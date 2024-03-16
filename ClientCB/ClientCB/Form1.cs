using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientCB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Variabili Client
        private TcpClient client;
        private NetworkStream stream;
        private int elapsedTime, clicks;
        private int limSx, limDx, limUp, limDw;
        private Random random = new Random();
        private string nome;
        private byte[] responseData = new byte[1024];

        private void Form1_Load(object sender, EventArgs e)
        {
            elapsedTime = 0;
            clicks = 0;
            limSx = 0;
            limUp = 0;
            limDx = this.Width;
            limDw = this.Height;
        }       

        // Funzioni
        private void InviaMessaggioServer(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        private string RiceviMessaggioServer()
        {
            int bytesRead = stream.Read(responseData, 0, responseData.Length);
            string response = Encoding.ASCII.GetString(responseData, 0, bytesRead);
            return response;
        }

        // Richieste al Server
        private void Wait()
        {       
            labelAttesa.Visible = true;

            string message = nome + ";sfida";

            InviaMessaggioServer(message);
             
            string response;

            while (true)
            {
                response = RiceviMessaggioServer();
                if (response == "gioca")
                {
                    labelAttesa.Visible = false;
                    Gioca();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }
        private void Gioca()
        {
            InizioPartita();
            while (elapsedTime < 30)
            {
                
            }
            FinePartita();
        }
        private void InizioPartita()
        {
            button.Visible = true;
            buttonLogout.Visible = false;
            timerPartita.Start();
            timerSposta.Start();
            elapsedTime = 0;
            clicks = 0;
            labelNome.Visible = false;
            labelRecord.Visible = false;
            labelVittorie.Visible = false;
        }
        private void FinePartita()
        {            
            button.Visible = false;
            buttonLogout.Visible = true;
            labelNome.Visible = true;
            labelRecord.Visible = true;
            labelVittorie.Visible = true;
            timerSposta.Stop();
            timerPartita.Stop();
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
                    buttonPartita.Visible = true;
                    buttonCancella.Visible = false;
                    labelAttesa.Visible = false;
                }
            } catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void CancellaRichiesta()
        {
            string message = nome + ";cancella";

            InviaMessaggioServer(message);

            string response = RiceviMessaggioServer();

            if (response == "cancellato")
            {
                buttonPartita.Visible = true;
                buttonCancella.Visible = false;
                labelAttesa.Visible = false;
            }             
        }

        // Button Clicks
        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 10000);
                stream = client.GetStream();

                nome = textBoxNome.Text;

                // Invio delle credenziali al server
                string message = nome + ";login";

                InviaMessaggioServer(message);

                int bytesRead = stream.Read(responseData, 0, responseData.Length);
                string response = Encoding.ASCII.GetString(responseData, 0, bytesRead);
                string[] msg = response.Split(';');
                // Visualizzazione della risposta
                if (msg[0] == "loggato")
                {
                    buttonPartita.Visible = true;
                    textBoxNome.Visible = false;
                    buttonGioca.Visible = false;

                    labelNome.Visible = true;
                    labelNome.Text = "Nome: " + nome;
                    labelRecord.Visible = true;
                    labelRecord.Text = "Record: " + msg[1];
                    labelVittorie.Visible = true;
                    labelVittorie.Text = "Vittorie: " + msg[2];
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Logout(client, stream);
        }
        private void button_Click(object sender, EventArgs e)
        {
            clicks++;
        }
        private void buttonPartita_Click(object sender, EventArgs e)
        {
            Wait();
        }
        private void buttonCancella_Click(object sender, EventArgs e)
        {
            CancellaRichiesta();
        }
        private void buttonRivincita_Click(object sender, EventArgs e)
        {
            Wait();
        }

        // Timers
        private void timerSposta_Tick(object sender, EventArgs e)
        {
            int x, y;
            x = random.Next(limSx, limDx);
            y = random.Next(limUp, limDw);
            button.Location = new Point(x, y);
        }
        private void timerPartita_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
        }
    }
}
    

