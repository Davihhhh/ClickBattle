using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCB
{
    public class Partita
    {
        private string _nome1, _nome2;
        private int _punteggio1, _punteggio2;
        private DateTime _data;

        public string Nome1
        {
            get { return _nome1; }
            private set { _nome1 = value; }
        }
        public string Nome2
        {
            get { return _nome2; }
            private set { _nome2 = value; }
        }
        public int Punteggio1
        {
            get { return _punteggio1; }
            private set { _punteggio1 = value; }
        }
        public int Punteggio2
        {
            get { return _punteggio2; }
            private set { _punteggio2 = value; }
        }
        public DateTime Data
        {
            get { return _data; }
            private set { _data = value; }
        }

        public Partita(string nome1, string nome2, int punteggio1, int punteggio2)
        {
            Nome1 = nome1;
            Nome2 = nome2;
            Punteggio1 = punteggio1;
            Punteggio2 = punteggio2;
            _data = DateTime.Now;
        }

    }
}
