using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Utakmica
{
    public class VratiUtakmicuSO : SistemskaOperacijaBase
    {
        // select sa id
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            // NIJE TESTIRANO
            List<Domen.Utakmica> utakmice = Broker.Broker.Instance.VratiSaUslovom(objekat as Domen.Utakmica).Cast<Domen.Utakmica>().ToList();

            // dodati igrace i trenere
            // koristi VratiSaUslovom select * from igra JOIN utakmica where utakmicaid = uid

            foreach (var utakmica in utakmice)
            {
                Igra i = new Igra
                {
                    Utakmica = utakmica,
                    Igrac = new Domen.Igrac()
                };

                List<Igra> igranja = Broker.Broker.Instance.VratiSaUslovom(i).Cast<Igra>().ToList();
                foreach (var igranje in igranja)
                {
                    utakmica.Igraci.Add(igranje.Igrac);

                }

                Ucestvuje u = new Ucestvuje
                {
                    Utakmica = utakmica,
                    Trener = new Domen.Trener()
                };

                List<Ucestvuje> ucestvovanja = Broker.Broker.Instance.VratiSaUslovom(u).Cast<Ucestvuje>().ToList();
                foreach (var ucestvovanje in ucestvovanja)
                {
                    utakmica.Treneri.Add(ucestvovanje.Trener);

                }



            }






            return utakmice;
        }
    }
}
