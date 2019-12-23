using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Trener
{
    public class KreirajTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            return new Domen.Trener();
        }
    }
}
