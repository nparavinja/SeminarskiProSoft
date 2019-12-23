using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Trener
{
    public class VratiTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<Domen.Trener> treneri = Broker.Broker.Instance.VratiSaUslovom(objekat as Domen.Trener).Cast<Domen.Trener>().ToList();


            return treneri;
        }
    }
}
