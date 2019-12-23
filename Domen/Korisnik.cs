using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class Korisnik : IDomenskiObjekat
    {
        public int KorisnikID { get; set; }
        public string ImeIPrezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public bool Ulogovan { get; set; }

        public int VratiID()
        {
            throw new NotImplementedException();
        }

        public List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac)
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            while (citac.Read())
            {
                Korisnik k = new Korisnik();
                k.ImeIPrezime = citac["ImeIPrezime"].ToString();
                k.KorisnickoIme = citac["KorisnickoIme"].ToString();
                k.KorisnikID = (int)citac["KorisnikID"];
                k.Lozinka = citac["Lozinka"].ToString();


                korisnici.Add(k);
            }

            return korisnici.ToList<IDomenskiObjekat>();
        }

        public string VratiNazivTabele()
        {
            return "Korisnik";
        }

        public int VratiSledeciID()
        {
            throw new NotImplementedException();
        }

        public string VratiUslovZaJoin()
        {
            throw new NotImplementedException();
        }

        public string VratiUslovZaUpdate()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaDelete()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaInsert()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaUpdate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{ImeIPrezime} --> {KorisnickoIme},{Lozinka}";
        }

        public string VratiZaPretragu()
        {
            throw new NotImplementedException();
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            throw new NotImplementedException();
        }

        public string VratiIDNaziv()
        {
            throw new NotImplementedException();
        }

        public string VratiZaSelect()
        {
            throw new NotImplementedException();
        }
    }
}
