using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class KreirajIgracaSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            return new Domen.Igrac();
        }
    }
}
