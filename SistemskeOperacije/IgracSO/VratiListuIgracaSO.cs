using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Igrac
{
    public class VratiListuIgracaSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {

                   // VratiSve(IDomenskiObjekat) vraca listu IDomenskihObjekata, pa ih kastujemo u listu igraca
            List<Domen.Igrac> igraci = Broker.Broker.Instance.VratiSve(new Domen.Igrac()).Cast<Domen.Igrac>().ToList();


            return igraci;
        }
    }
}
