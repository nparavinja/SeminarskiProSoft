using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemskeOperacije.Trener
{
    public class VratiListuTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            List<Domen.Trener> treneri = Broker.Broker.Instance.VratiSve(new Domen.Trener()).Cast<Domen.Trener>().ToList();


            return treneri;
        }
    }
}
