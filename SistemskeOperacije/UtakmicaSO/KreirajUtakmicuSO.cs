using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije.UtakmicaSO
{
    public class KreirajUtakmicuSO : SistemskaOperacijaBase
    {
        //insert
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            if (!(objekat is Domen.Utakmica))
            {
                return false;
            }

            Domen.Utakmica utakmica = (Domen.Utakmica)objekat;
            utakmica.UtakmicaID = Broker.Broker.Instance.VratiSledeciID(new Domen.Utakmica());
            var igraci = new List<Domen.Igrac>();
            igraci = utakmica.Igraci;
            var treneri = new List<Domen.Trener>();
            treneri = utakmica.Treneri;

            bool uspeh = false;

            bool uspesnoUneoUtakmicu = Broker.Broker.Instance.InsertBezID(utakmica);
            bool uspesnoUneoIgranja = true;
            bool uspesnoUneoUcestvovanja = true;

            foreach (var i in igraci)
            {
                var igra = new Domen.Igra();
                igra.Igrac = i;
                igra.Utakmica = utakmica;
                bool uneoJednoIgranje = Broker.Broker.Instance.InsertBezID(igra);
                if (!uneoJednoIgranje)
                {
                    uspesnoUneoIgranja = false;
                    break;
                }

            }
            
            foreach (var t in treneri)
            {
                var ucestvuje = new Domen.Ucestvuje();
                ucestvuje.Trener = t;
                ucestvuje.Utakmica = utakmica;
                bool uneoJednoU = Broker.Broker.Instance.InsertBezID(ucestvuje);
                if (!uneoJednoU)
                {
                    uspesnoUneoUcestvovanja = false;
                    break;
                }

            }





    

            Debug.WriteLine($"UTAKMICA: {utakmica}");
            Debug.WriteLine("UNETI IGRACI: ");
            foreach (Domen.Igrac i in igraci)
            {
                Debug.WriteLine(i);
            }
            Debug.WriteLine("UNETI TRENERI: ");
            foreach (Domen.Trener t in treneri)
            {
                Debug.WriteLine(t);
            }



            
            if (uspesnoUneoUtakmicu && uspesnoUneoIgranja && uspesnoUneoUcestvovanja)
            {
                uspeh = true;
            }           
           
            return uspeh;
        }
    }
}
