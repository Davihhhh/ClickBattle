using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.Net.NetworkInformation;

namespace ServerCB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            giocatore1 = new Giocatore();
            giocatore2 = new Giocatore();
            CaricaGiocatori();
        }       

        // Variabili Server
        private List<Giocatore> giocatori;
        private TcpListener server;
        private TcpClient client, client1, client2;
        private Giocatore giocatore1, giocatore2;
        private const string fileName = @".\giocatori.xml";
        private const string fileNamePartite = @".\partite.xml";
        private const int portaDefault = 10000;
        private IPAddress ipAddress;
        private bool stop;

        // Funzioni Server

        // Funzione per caricare in memoria il file dei giocatori
        private void CaricaGiocatori()
        {
            giocatori = new List<Giocatore>();
            // Controlla se il file esiste
            if (File.Exists(fileName))
            {
                XmlDocument lista = new XmlDocument();
                lista.Load(fileName);

                XmlNode giocatorii = lista.SelectSingleNode("giocatori");
                XmlNode nome, punteggio, vittorie;
                Giocatore x;
                foreach (XmlNode giocatore in giocatorii)
                {
                    nome = giocatore.SelectSingleNode("nome");
                    punteggio = giocatore.SelectSingleNode("punteggiomassimo");
                    vittorie = giocatore.SelectSingleNode("vittorie");

                    x = new Giocatore(nome.InnerText, Convert.ToInt32(punteggio.InnerText), Convert.ToInt32(vittorie.InnerText));

                    giocatori.Add(x);
                }
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmlDeclaration);

                XmlElement root = xmlDoc.CreateElement("giocatori");
                xmlDoc.AppendChild(root);

                xmlDoc.Save(fileName);
            }
        }
        
        // Gestione connessioni
        private void AttendiClient()
        {
            // Ip macchina locale
            ipAddress = IPAddress.Parse("127.0.0.1");

            // Istanzia un oggetto "ascoltatore" (un Listener, appunto) in grado
            // di accettare connessioni su qualunque interfaccia ip, sulla porta definita dalla variabile porta          
            server = new TcpListener(ipAddress, portaDefault);

            // Avvia il server di ascolto
            server.Start();

            // Ascolto
            while (true)
            {
                try
                {
                    // Accetta la richiesta di connessione di un client
                    client = server.AcceptTcpClient();
                    
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                }
                catch (Exception eccezione)
                {
                    MessageBox.Show(eccezione.ToString());
                }
                if(!giocatore1.Equals(giocatore2) && !giocatore2.PunteggioMassimo.Equals(-1) && !giocatore2.PunteggioMassimo.Equals(-1))
                {                 
                    Partita();
                }
                if(stop)
                {
                    server.Stop();
                    break;
                }
            }
        }
        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            try
            {
                // Ottiene la porta locale
                IPEndPoint localEndPoint = (IPEndPoint)client.Client.LocalEndPoint;

                int localPort = localEndPoint.Port;

                // Genera una nuova porta per la connessione
                int newPort = GetNextAvailablePort();
                string p = "porta;" + newPort;

                // Associa il nuovo TcpListener alla nuova porta
                TcpListener newListener = new TcpListener(IPAddress.Any, newPort);
                newListener.Start();

                // Notifica al client la nuova porta
                byte[] data = Encoding.UTF8.GetBytes(p);
                client.GetStream().Write(data, 0, data.Length);

                // Accetta una nuova connessione sulla nuova porta
                client = newListener.AcceptTcpClient();

                newListener.Stop();

                // Variabili del client
                NetworkStream stream;
                byte[] buffer = new byte[1024];
                int bytesRead;
                string dataReceived = "";
                string[] msg;
                string nome, comando;
                string response = "";
                byte[] responseData;
                bool logged = false;
                Giocatore giocatore = new Giocatore("tmp", 0, 0);

                while (true)
                {
                    // Leggi i dati inviati dal client
                    stream = client.GetStream();
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Formato del messaggio nome;comando
                    MessageBox.Show(dataReceived);

                    msg = dataReceived.Split(';');
                    nome = msg[0];
                    comando = msg[1];

                    switch (comando)
                    {
                        case "login":
                            if (Login(nome))
                            {
                                logged = true;
                                giocatore = GetGiocatore(nome);
                                response = $"loggato;{giocatore.PunteggioMassimo};{giocatore.Vittorie}";
                            }
                            else if (SalvaUtente(nome))
                            {
                                response = $"loggato;0;0";
                            }
                            break;
                        case "sfida":
                            if(logged)
                            {
                                if (AggiungiGiocatoreAttesa(giocatore, client) == 1)
                                {
                                    response = "attendi";
                                }
                                else if (AggiungiGiocatoreAttesa(giocatore, client) == 2)
                                {
                                    response = "gioca";
                                }
                                else if (AggiungiGiocatoreAttesa(giocatore, client) > 2)
                                {
                                    response = "pieno";
                                }
                            }
                            break;
                        case "cancella":
                            if (logged)
                            {
                                if(RimuoviGiocatoreAttesa(giocatore))
                                {
                                    response = "cancellato";
                                }
                            }
                            break;
                        case "disconnetto":
                            if (logged)
                            {
                                logged = false;
                                giocatore = null;
                                response = "disconnesso";                            
                            }
                            break;                 
                        default:
                            response = "comando invalido";
                            break;
                    }
                    responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                    if (response == "disconnetto")
                    {
                        client.Close();
                        newListener.Stop();
                        return;
                    }
                }
            }
            catch (Exception eccezione)
            {
                MessageBox.Show(eccezione.ToString());
            }
            finally
            {
                client.Close();
            }
        }
        private int GetNextAvailablePort()
        {
            // Crea un nuovo TcpListener con porta 0 per ottenere automaticamente una porta disponibile
            TcpListener listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
        private bool Login(string nome)
        {
            foreach (Giocatore g in giocatori)
            {
                if (g.Nome.Equals(nome))
                    return true;
            }
            return false;
        }
        private Giocatore GetGiocatore(string nome)
        {
            foreach (Giocatore g in giocatori)
            {
                if (g.Nome.Equals(nome))
                    return g;
            }
            return null;
        }             

        // Gestione partite
        private int AggiungiGiocatoreAttesa(Giocatore giocatore, TcpClient client)
        {
            if (giocatore1.Equals(null))
            {
                giocatore1 = giocatore;
                client1 = client;
                return 1;
            }
            else if (giocatore2.Equals(null))
            {
                giocatore2 = giocatore;
                client2 = client;
                return 2;
            }
            else return 3;
        }
        private bool RimuoviGiocatoreAttesa(Giocatore giocatore)
        {
            if (giocatore1.Equals(giocatore))
            {
                giocatore1 = null;
                return true;
            }
            else if (giocatore2.Equals(giocatore))
            {
                giocatore2 = null;
                return true;
            }
            else return false;
        }
        private void Partita()
        {
            try
            {
                string nome1 = giocatore1.Nome, nome2 = giocatore2.Nome, response;
                int punteggio1 = 0, punteggio2 = 0;
                byte[] responseData;               

                byte[] buffer = new byte[1024];
                int bytesRead;
                string dataReceived1 = "", dataReceived2 = "";
                string[] msg;

                TcpListener ascolto1 = new TcpListener(IPAddress.Any, ((IPEndPoint)client1.Client.LocalEndPoint).Port);
                TcpListener ascolto2 = new TcpListener(IPAddress.Any, ((IPEndPoint)client2.Client.LocalEndPoint).Port);

                // Variabili partita
                NetworkStream stream1, stream2;

                stream1 = client1.GetStream();
                stream2 = client2.GetStream();

                response = "gioca";

                responseData = Encoding.UTF8.GetBytes(response);
                stream1.Write(responseData, 0, responseData.Length);
                stream2.Write(responseData, 0, responseData.Length);

                bytesRead = stream1.Read(buffer, 0, buffer.Length);
                dataReceived1 = Encoding.UTF8.GetString(buffer, 0, bytesRead);             

                msg = dataReceived1.Split(';');

                if (nome1 == msg[0])
                {
                    punteggio1 = Convert.ToInt32(msg[1]);
                }

                bytesRead = stream2.Read(buffer, 0, buffer.Length);
                dataReceived2 = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                msg = dataReceived1.Split(';');

                if (nome2 == msg[0])
                {
                    punteggio2 = Convert.ToInt32(msg[1]);
                }

                response = "fine";
                
                responseData = Encoding.UTF8.GetBytes(response);
                stream1.Write(responseData, 0, responseData.Length);
                stream2.Write(responseData, 0, responseData.Length);

                Partita nuova = new Partita(nome1, nome2, punteggio1, punteggio2);
                SalvaPartita(nuova);
            }
            catch (Exception eccezione) { MessageBox.Show(eccezione.Message); }
        }

        // Controllo del server
        private void buttonStop_Click(object sender, EventArgs e)
        {
            stop = true;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            AttendiClient();
            stop = false;
        }

        // Gestione dei file XML
        private void SalvaPartita(Partita partita)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileNamePartite);

            XmlElement root = xmlDoc.DocumentElement;

            XmlElement nuovaPartita = xmlDoc.CreateElement("partita");

            // Crea il nodo "nome" e assegna il valore ricevuto come parametro
            XmlElement nome1Element = xmlDoc.CreateElement("nome1");
            XmlElement nome2Element = xmlDoc.CreateElement("nome2");
            XmlElement punteggio1Element = xmlDoc.CreateElement("punteggio1");
            XmlElement punteggio2Element = xmlDoc.CreateElement("punteggio2");

            nome1Element.InnerText = partita.Nome1;
            nome2Element.InnerText = partita.Nome2;
            punteggio1Element.InnerText = partita.Punteggio1.ToString();
            punteggio2Element.InnerText = partita.Punteggio2.ToString();

            nuovaPartita.AppendChild(nome1Element);
            nuovaPartita.AppendChild(nome2Element);
            nuovaPartita.AppendChild(punteggio1Element);
            nuovaPartita.AppendChild(punteggio2Element);

            root.AppendChild(nuovaPartita);

            xmlDoc.Save(fileNamePartite);
        }
        private bool SalvaUtente(string nome)
        {
            foreach (Giocatore giocatore in giocatori)
            {
                if (giocatore.Nome.Equals(nome))
                    return false;
            }
            Giocatore g = new Giocatore(nome, 0, 0);
            giocatori.Add(g);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlElement root = xmlDoc.DocumentElement;

            XmlElement nuovoGiocatore = xmlDoc.CreateElement("giocatore");

            XmlElement nomeElement = xmlDoc.CreateElement("nome");
            XmlElement punteggioElement = xmlDoc.CreateElement("punteggiomassimo");
            XmlElement vittorieElement = xmlDoc.CreateElement("vittorie");

            nomeElement.InnerText = nome;
            punteggioElement.InnerText = "0";
            vittorieElement.InnerText = "0";

            nuovoGiocatore.AppendChild(nomeElement);
            nuovoGiocatore.AppendChild(punteggioElement);
            nuovoGiocatore.AppendChild(vittorieElement);

            root.AppendChild(nuovoGiocatore);

            xmlDoc.Save(fileName);

            return true;
        }
    }
}
// DEBUGGING (LOL, HO PAURA)
