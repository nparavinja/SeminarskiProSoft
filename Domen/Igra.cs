using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public class Igra : IDomenskiObjekat
    {
        public Igrac Igrac { get; set; }
        public Utakmica Utakmica { get; set; }

        public int VratiID()
        {
            throw new NotImplementedException();
        }

        public string VratiIDNaziv()
        {
            throw new NotImplementedException();
        }

        public List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac)
        {
            List<IDomenskiObjekat> igranja = new List<IDomenskiObjekat>();
            while (citac.Read())
            {

                Utakmica u = new Utakmica
                {
                    UtakmicaID = (int)citac["UtakmicaID"],
                    BrojGledalaca = (int)citac["BrojGledalaca"],
                    DatumIVremeOdigravanja = DateTime.Parse(citac["DatumIVremeOdigravanja"].ToString()),
                    Protivnik = citac["Protivnik"].ToString(),
                    Rezultat = citac["Rezultat"].ToString()

                };

                Igrac i = new Igrac
                {
                    
                    Ime = citac["Ime"].ToString(),
                    Prezime = citac["Prezime"].ToString(),
                    DatumRodjenja = (DateTime)citac["DatumRodjenja"],
                    IgracID = (int)citac["IgracID"],
                    Kategorija = citac["Kategorija"].ToString(),
                    Pozicija = citac["Pozicija"].ToString(),
                    
                };

                Igra igranje = new Igra
                {
                    Utakmica = u,
                    Igrac = i
                };


                igranja.Add(igranje);
            }

            return igranja;
        }

        public string VratiNazivTabele()
        {
            return "Igra";
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            // return;
            return 0;
        }

        public string VratiUslovZaJoin()
        {
            // iz vrati naziv tabele: Igra
            //   igrac --> igra <-- utakmica
            
            return "i JOIN Utakmica u ON u.UtakmicaID = i.UtakmicaID JOIN Igrac igrac ON i.IgracID = igrac.IgracID";
        }

        public string VratiUslovZaUpdate()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND IgracID = {this.Igrac.IgracID}";
        }

        public string VratiVrednostiZaDelete()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND IgracID = {this.Igrac.IgracID}";
        }

        public string VratiVrednostiZaInsert()
        {
            return $"{this.Igrac.IgracID},{this.Utakmica.UtakmicaID}";

        }

        public string VratiVrednostiZaUpdate()
        {
            return $"IgracID = {this.Igrac.IgracID}, UtakmicaID = {this.Utakmica.UtakmicaID}";
        }

        public string VratiZaPretragu()
        {
            if(this.Igrac.IgracID == 0 && this.Utakmica.UtakmicaID != 0)
            {
                return $"i.UtakmicaID = {this.Utakmica.UtakmicaID}";
            }

            if (this.Igrac.IgracID != 0 && this.Utakmica.UtakmicaID == 0)
            {
                return $"i.IgracID = {this.Igrac.IgracID}";
            }


            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND IgracID = {this.Igrac.IgracID}";
        }

        public string VratiZaSelect()
        {
            return "u.UtakmicaID, u.DatumIVremeOdigravanja, u.Protivnik, u.BrojGledalaca, u.Rezultat, igrac.IgracID ," +
                "igrac.Ime, igrac.Prezime, igrac.DatumRodjenja, igrac.Kategorija, igrac.Pozicija";
        }
    }
}
