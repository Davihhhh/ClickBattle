using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerCB
{
    public class GiocatoreAttivo : Giocatore
    {
        private NetworkStream _connessione;
        public NetworkStream Connessione
        {
            get { return _connessione; }
            private set { _connessione = value; }
        }

        public GiocatoreAttivo()
        {

        }
        public GiocatoreAttivo(string nome, int punteggiomassimo, int vittorie, NetworkStream connessione) : base(nome, punteggiomassimo, vittorie)
        {
            Connessione = connessione; 
        }
        public GiocatoreAttivo(Giocatore giocatore, NetworkStream connessione) : base(giocatore.Nome, giocatore.PunteggioMassimo, giocatore.Vittorie)
        {
            Connessione = connessione;
        }
    }
}
