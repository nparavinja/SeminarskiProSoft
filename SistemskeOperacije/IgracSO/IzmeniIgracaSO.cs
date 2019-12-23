using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class IzmeniIgracaSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {

            bool uspesno = Broker.Broker.Instance.Update((Domen.IDomenskiObjekat)objekat);
            return uspesno;
        }
    }
}
