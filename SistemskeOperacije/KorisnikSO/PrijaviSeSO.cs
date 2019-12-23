using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Broker;
using Domen;

namespace SistemskeOperacije.Korisnik
{
    public class PrijaviSeSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            
            Domen.Korisnik prijava = (Domen.Korisnik)objekat;

            List<Domen.Korisnik> korisnici = Broker.Broker.Instance.VratiSve(new Domen.Korisnik()).Cast<Domen.Korisnik>().ToList();


            foreach (Domen.Korisnik k in korisnici)
            {
                if (k.KorisnickoIme == prijava.KorisnickoIme && k.Lozinka == prijava.Lozinka)
                    return k;
            }
            throw new Exception("Korisnik ne postoji!");
            
        }
        /*
        public object IzvrsiSO(object objekat)
        {
            return base.IzvrsiSO(objekat);

        }
        */
    }
}
