using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroGUI
{
    class Eurovizio
    {
        int ev;
        string eloado;
        string cim;
        int helyezes;
        int pontszam;

        public Eurovizio(int ev, string eloado, string cim, int helyezes, int pontszam)
        {
            this.ev = ev;
            this.eloado = eloado;
            this.cim = cim;
            this.helyezes = helyezes;
            this.pontszam = pontszam;
        }

        public int Ev { get => ev;  }
        public string Eloado { get => eloado;  }
        public string Cim { get => cim; }
        public int Helyezes { get => helyezes; }
        public int Pontszam { get => pontszam; }
    }
}
