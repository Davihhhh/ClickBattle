﻿using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientCB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // Variabili Client
        private TcpClient client;
        private NetworkStream stream;
        private int clicks;
        private int limSx, limDx, limUp, limDw;
        private Random random = new Random();
        private string nome;
        private byte[] responseData = new byte[1024];
        private const string ipServer = "127.0.0.1";
        private const int portaDefault = 10000;

        private void Form1_Load(object sender, EventArgs e)
        {
            clicks = 0;
            limSx = 0;
            limUp = 0;
            limDx = this.Width - 60;
            limDw = this.Height - 60;      
        }

        // Funzioni
        private void InviaMessaggioServer(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            } catch(Exception e) { MessageBox.Show(e.Message); }
        }
        private string RiceviMessaggioServer()
        {
            try
            { 
                int bytesRead = stream.Read(responseData, 0, responseData.Length);
                string response = Encoding.ASCII.GetString(responseData, 0, bytesRead);
                return response;
            }
            catch (TimeoutException) { return "timeout"; }
            catch (Exception ex) { MessageBox.Show(ex.Message); return "nodata"; }
        }

        // Richieste al Server
        private void Login()
        {
            try
            {
                if (StabilisciConnessione())
                {
                    stream = client.GetStream();

                    nome = textBoxNome.Text;

                    // Invio delle credenziali al server
                    string message = nome + ";login";

                    InviaMessaggioServer(message);

                    string response = RiceviMessaggioServer();

                    string[] msg = response.Split(';');

                    // Visualizzazione della risposta
                    if (msg[0] == "loggato")
                    {
                        textBoxNome.Visible = false;
                        buttonGioca.Visible = false;

                        labelNome.Visible = true;
                        labelNome.Text = "Nome: " + nome;
                        labelRecord.Visible = true;
                        labelRecord.Text = "Record: " + msg[1];
                        labelVittorie.Visible = true;
                        labelVittorie.Text = "Vittorie: " + msg[2];
                        buttonLogout.Visible = true;
                        buttonRicarica.Visible = true;
                        labelAttesa.Visible = true;
                        comboBoxAvversari.Visible = true;
                        buttonSfida.Visible = true;

                        Ricarica();                       
                    }                    
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
        private bool StabilisciConnessione()
        {
            client = new TcpClient();
            client.Connect(ipServer, portaDefault);

            stream = client.GetStream();
            stream.ReadTimeout = 3000;
            string message = RiceviMessaggioServer();

            //MessageBox.Show(message);

            if (message.StartsWith("porta"))
            {
                int newPort = int.Parse(message.Split(';')[1]);

                client.Close();

                // Riconnettiti al server sulla nuova porta
                client = new TcpClient();
                client.Connect(ipServer, newPort);
                return true;
            }
            return false;
        }
        private void SfidaAvversario()
        {
            try
            {
                string valoreSelezionato;
                if (comboBoxAvversari.Items.Count == 0)
                    valoreSelezionato = "-1";
                else 
                    valoreSelezionato = comboBoxAvversari.SelectedItem.ToString();
                string message = nome + ";sfidaavversario;" + valoreSelezionato;
                MessageBox.Show(message);

                InviaMessaggioServer(message);

                string response = RiceviMessaggioServer();
                MessageBox.Show(response);

                if (response == "insufficienti")
                {
                    MessageBox.Show("Giocatori Insufficienti");
                }
                else if (response == "gioca")
                {
                    InizioPartita();
                    return;
                } 
                else if (response == "nodata")
                {
                    MessageBox.Show("In attesa di risposta dal server");
                }

            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void InizioPartita()
        {
            labelPunteggio.Text = "Punteggio: 0";
            textBoxNome.Visible = false;
            button.Visible = true;
            buttonLogout.Visible = false;
            buttonRivincita.Visible = false;
            buttonGioca.Visible = false;
            buttonRicarica.Visible = false;
            comboBoxAvversari.Visible = false;
            timerPartita.Start();
            clicks = 0;
            labelNome.Visible = false;
            labelRecord.Visible = false;
            labelVittorie.Visible = false;
            labelPunteggio.Visible = true;
        }
        private void FinePartita()
        {
            button.Visible = false;
            buttonLogout.Visible = true;
            labelNome.Visible = true;
            labelRecord.Visible = true;
            labelVittorie.Visible = true;
            buttonRivincita.Visible = true;
            buttonRicarica.Visible = true;           
            InviaPunteggio();
        }
        private void Logout(TcpClient client, NetworkStream stream)
        {
            try
            {
                string message = nome + ";disconnetto";
                InviaMessaggioServer(message);

                stream.Close();
                client.Close();

                textBoxNome.Visible = true;
                buttonGioca.Visible = true;
                button.Visible = false;
                buttonLogout.Visible = false;
                buttonRivincita.Visible = false;                
                buttonRicarica.Visible = false;
                buttonSfida.Visible = false;
                comboBoxAvversari.Visible = false;
                labelNome.Visible = false;
                labelRecord.Visible = false;
                labelVittorie.Visible = false;
                labelAttesa.Visible = false;
                labelPunteggio.Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void InviaPunteggio()
        {
            try
            {
                string message = nome + ';' + clicks;

                InviaMessaggioServer(message);

                string response = RiceviMessaggioServer();

                if (response == "fine")
                {
                    labelAttesa.Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void GeneraAvversari(string[] opzioniComboBox)
        {
            comboBoxAvversari.Items.Clear();
            comboBoxAvversari.DropDownStyle = ComboBoxStyle.DropDownList;
            if (opzioniComboBox[opzioniComboBox.Length - 1].EndsWith(""))
            {    
                Array.Resize(ref opzioniComboBox, opzioniComboBox.Length - 1);
            }
            if (opzioniComboBox.Contains(nome))
            {
                opzioniComboBox = opzioniComboBox.Where(valore => valore != nome).ToArray();
            }
            comboBoxAvversari.Items.AddRange(opzioniComboBox);
        }
        private void Ricarica()
        {
            string message = nome + ";avversari";
            InviaMessaggioServer(message);
            string response = RiceviMessaggioServer();
            //MessageBox.Show(response);
            string[] msg = response.Split(';');
            if (msg[0].StartsWith("gioca"))
                InizioPartita();
            else
                GeneraAvversari(msg);
        }

        // Button Clicks
        private void Login_Click(object sender, EventArgs e)
        {
            InizioPartita();
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Logout(client, stream);
        }
        private void button_Click(object sender, EventArgs e)
        {
            int x, y;
            clicks++;
            labelPunteggio.Text = "Punteggio: " + clicks;
            x = random.Next(limSx, limDx);
            y = random.Next(limUp, limDw);
            button.Location = new Point(x, y);
            System.Media.SystemSounds.Beep.Play();
        }
        private void buttonRicarica_Click(object sender, EventArgs e)
        {
            Ricarica();
        }
        private void buttonSfida_Click(object sender, EventArgs e)
        {
            SfidaAvversario();
        }
        private void buttonRivincita_Click(object sender, EventArgs e)
        {
            SfidaAvversario();
        }

        // Timers
        private void timerPartita_Tick_1(object sender, EventArgs e)
        {
            FinePartita();
            timerPartita.Stop();
        }
    }
}


