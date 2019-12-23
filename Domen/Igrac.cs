using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public class Igrac : IDomenskiObjekat
    {
        public int IgracID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Kategorija { get; set; }
        public string Pozicija { get; set; }
        public string KriterijumPretrage { get; set; }
        public int BrojNaDresu { get; set; }


        public override string ToString()
        {
            return $"{this.Ime} {this.Prezime}, {this.Kategorija}";
        }

        public int VratiID()
        {
            return this.IgracID;
        }

        public string VratiIDNaziv()
        {
            return "IgracID";
        }

        public List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac)
        {
            List<Igrac> igraci = new List<Igrac>();
            while (citac.Read())
            {
                Igrac i = new Igrac
                {
                    Ime = citac["Ime"].ToString(),
                    Prezime = citac["Prezime"].ToString(),
                    DatumRodjenja = (DateTime)citac["DatumRodjenja"],
                    IgracID = (int)citac["IgracID"],
                    Kategorija = citac["Kategorija"].ToString(),
                    Pozicija = citac["Pozicija"].ToString(),
                    BrojNaDresu = (int)citac["BrojNaDresu"]
                };
             


                igraci.Add(i);
            }

            return igraci.ToList<IDomenskiObjekat>();
        }

        public string VratiNazivTabele()
        {
            return "Igrac";
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            int slobodanID = 0;
            List<int> listaID = new List<int>();

            while (citac.Read())
            {
                int id = (int)citac["IgracID"];
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
            return $"IgracID = {VratiID()}";
        }

        public string VratiVrednostiZaDelete()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaInsert()
        {
            return $"'{this.Ime}','{this.DatumRodjenja}','{this.Kategorija}'," +
                $"'{this.Pozicija}', {this.BrojNaDresu}, '{this.Prezime}'";
        }

        public string VratiVrednostiZaUpdate()
        {
            return $"IgracID = {this.IgracID},Ime = '{this.Ime}', Prezime = '{this.Prezime}', " +
                $" DatumRodjenja = '{this.DatumRodjenja}', Kategorija = '{this.Kategorija}', Pozicija = '{this.Pozicija}', BrojNaDresu = {this.BrojNaDresu} ";
        }

        public string VratiZaPretragu()
        {
            switch (this.KriterijumPretrage)
            {
                case "ID":
                    return $"IgracID = {this.IgracID}";
                case "IME":
                    return $"Ime LIKE '%{this.Ime}%'";
                case "PREZIME":
                    return $"Prezime LIKE '%{this.Prezime}%'";
                case "KATEGORIJA":
                    return $"Kategorija = '{this.Kategorija}'";
                case "POZICIJA":
                    return $"Pozicija = '{this.Pozicija}'";

                default:
                    return "error";
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Igrac i)
            {
                return i.IgracID == this.IgracID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string VratiZaSelect()
        {
            return "*";
        }
    }
     
 }

        

       
    

    

