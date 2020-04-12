using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaFirema
{
    public class Persona
    {
        int n;
        string nome;
        string cognome;

        public Persona(int n, string nome, string cognome)
        {
            this.n = n;
            this.nome = nome;
            this.cognome = cognome;
        }

        public int getId()
        {
            return n;
        }

        public string getName()
        {
            return nome;
        }

        public string getSurName()
        {
            return cognome;
        }

    }
}
