using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class VratiIgracaSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<Domen.Igrac> igraci = Broker.Broker.Instance.VratiSaUslovom(objekat as Domen.Igrac).Cast<Domen.Igrac>().ToList();


            return igraci;

        }
    }
}
