using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class Trener : IDomenskiObjekat
    {
        public int TrenerID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Obrazovanje { get; set; }

        public string Kategorija { get; set; }

        public string KriterijumPretrage { get; set; }

        public int VratiID()
        {
            return this.TrenerID;
        }

        public string VratiIDNaziv()
        {
            return "TrenerID";
        }

        public override string ToString()
        {
            return $"{this.Ime} {this.Prezime}, {this.Kategorija}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Trener t)
            {
                return t.TrenerID == this.TrenerID;
            }
            return false;
        }


        public List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac)
        {
            List<Trener> treneri = new List<Trener>();
            while (citac.Read())
            {
                Trener t = new Trener
                {
                    TrenerID = (int)citac["TrenerID"],
                    Ime = citac["Ime"].ToString(),
                    Prezime = citac["Prezime"].ToString(),
                    Kategorija = citac["Kategorija"].ToString(),
                    Obrazovanje = citac["Obrazovanje"].ToString()
                };


                treneri.Add(t);
            }

            return treneri.ToList<IDomenskiObjekat>();
        }

        public string VratiNazivTabele()
        {
            return "Trener";
        }

       

        public int VratiSledeciID(SqlDataReader citac)
        {
            int slobodanID = 0;
            List<int> listaID = new List<int>();

            while (citac.Read())
            {
                int id = (int)citac["TrenerID"];
                listaID.Add(id);

            }

            if (listaID.Count == 0)
            {
                return 1;
            }

            for (int i = 0; i < listaID.Count; i++)
            {
                if ((i + 1) == listaID.Count)
                {
                    return listaID[i] + 1;
                }
                else if ((listaID[i] + 1) < listaID[i + 1])
                {
                    return listaID[i] + 1;
                }
            }
            return slobodanID;
        }

        public string VratiUslovZaJoin()
        {
            return "";
        }

        public string VratiUslovZaUpdate()
        {
            return $"TrenerID = {VratiID()}";

        }

        public string VratiVrednostiZaDelete()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaInsert()
        {
            return $"'{this.Ime}','{this.Obrazovanje}','{this.Kategorija}'," +
                $"'{this.Prezime}'";
        }

        public string VratiVrednostiZaUpdate()
        {
            return $"TrenerID = {this.TrenerID},Ime = '{this.Ime}', Prezime = '{this.Prezime}'," +
                $" Obrazovanje = '{this.Obrazovanje}', Kategorija = '{this.Kategorija}'";
        }

        public string VratiZaPretragu()
        {
            switch (this.KriterijumPretrage)
            {
                case "ID":
                    return $"TrenerID = {this.TrenerID}";
                case "IME":
                    return $"Ime LIKE '%{this.Ime}%'";
                case "PREZIME":
                    return $"Prezime LIKE '%{this.Prezime}%'";
                case "KATEGORIJA":
                    return $"Kategorija = '{this.Kategorija}'";
                

                default:
                    return "error";
            }
        }

        public override int GetHashCode()
        {
            return 1551148158 + TrenerID.GetHashCode();
        }

        public string VratiZaSelect()
        {
            return "*";
        }
    }

   

}
