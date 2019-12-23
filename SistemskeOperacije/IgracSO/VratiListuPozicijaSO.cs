using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class VratiListuPozicijaSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<string> pozicije = new List<string>();
            pozicije.Add("Tehnicar");
            pozicije.Add("Korektor");
            pozicije.Add("Primac");
            pozicije.Add("Libero");
            pozicije.Add("Srednji bloker");
            return pozicije;
        }
    }
}
