using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Trener
{
    public class VratiListuKategorijaTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<string> kategorije = new List<string>();
            kategorije.Add("Selektor");
            kategorije.Add("Pomocni trener");
            kategorije.Add("Statisticar");
            kategorije.Add("Fizioterapeut");
            return kategorije;
        }
    }
}
