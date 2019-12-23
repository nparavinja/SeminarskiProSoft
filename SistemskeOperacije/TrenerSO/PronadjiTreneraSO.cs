using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Trener
{
    public class PronadjiTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            // select - pretraga
            List<Domen.Trener> treneri = Broker.Broker.Instance.VratiSaUslovom(objekat as Domen.Trener).Cast<Domen.Trener>().ToList();


            return treneri;
        }
    }
}
