using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class VratiListuKategorijaIgracaSO : SistemskaOperacijaBase
    {

        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<string> kategorije = new List<string>();
            kategorije.Add("Pionir");
            kategorije.Add("Kadet");
            kategorije.Add("Junior");
            kategorije.Add("Profesionalac");
            return kategorije;
        }
    }
}
