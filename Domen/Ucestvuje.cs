using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public class Ucestvuje : IDomenskiObjekat
    {
        public Trener Trener { get; set; }
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
            List<IDomenskiObjekat> ucestvovanja = new List<IDomenskiObjekat>();
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

                Trener t = new Trener
                {
                    TrenerID = (int)citac["TrenerID"],
                    Ime = citac["Ime"].ToString(),
                    Prezime = citac["Prezime"].ToString(),
                    Kategorija = citac["Kategorija"].ToString(),
                    Obrazovanje = citac["Obrazovanje"].ToString()
                };

                Ucestvuje uc = new Ucestvuje
                {
                    Utakmica = u,
                    Trener = t
                };


                ucestvovanja.Add(uc);
            }

            return ucestvovanja;
        }

        public string VratiNazivTabele()
        {
            return "Ucestvuje";
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            throw new NotImplementedException();
        }

        public string VratiUslovZaJoin()
        {
            return "uc JOIN Utakmica u ON uc.UtakmicaID = u.UtakmicaID JOIN Trener trener ON uc.TrenerID = trener.TrenerID";
        }

        public string VratiUslovZaUpdate()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND TrenerID = {this.Trener.TrenerID}";
        }

        public string VratiVrednostiZaDelete()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND TrenerID = {this.Trener.TrenerID}";
        }

        public string VratiVrednostiZaInsert()
        {
            return $"{this.Trener.TrenerID},{this.Utakmica.UtakmicaID}";
        }

        public string VratiVrednostiZaUpdate()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} , TrenerID = {this.Trener.TrenerID}";

        }

        public string VratiZaPretragu()
        {
            if (this.Trener.TrenerID == 0 && this.Utakmica.UtakmicaID != 0)
            {
                return $"uc.UtakmicaID = {this.Utakmica.UtakmicaID}";
            }

            if (this.Trener.TrenerID != 0 && this.Utakmica.UtakmicaID == 0)
            {
                return $"uc.TrenerID = {this.Trener.TrenerID}";
            }


            return $"uc.UtakmicaID = {this.Utakmica.UtakmicaID} AND TrenerID = {this.Trener.TrenerID}";
        }

        public string VratiZaSelect()
        {
            return "u.UtakmicaID, u.DatumIVremeOdigravanja, u.Protivnik, u.BrojGledalaca, u.Rezultat, trener.TrenerID ," +
                "trener.Ime, trener.Prezime, trener.Obrazovanje, trener.Kategorija";
        }
    }
}
