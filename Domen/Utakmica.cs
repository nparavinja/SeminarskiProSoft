using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class Utakmica : IDomenskiObjekat
    {
        public int UtakmicaID { get; set; }
        public List<Igrac> Igraci { get; }
        public List<Trener> Treneri { get; }
        public DateTime DatumIVremeOdigravanja { get; set; }
        public string Protivnik { get; set; }
        public int BrojGledalaca { get; set; }
        public string KriterijumPretrage { get; set; }

        public List<StavkaStatistike> StavkeStatistike { get; set; }
        public string Rezultat { get; set; }

        public Utakmica()
        {
            Igraci = new List<Igrac>();
            Treneri = new List<Trener>();
            StavkeStatistike = new List<StavkaStatistike>();
        }

        public override string ToString()
        {
            return $"{this.UtakmicaID} -> {this.DatumIVremeOdigravanja} VS {this.Protivnik}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Utakmica u)
            {
                return u.UtakmicaID == this.UtakmicaID;
            }
            return false;

        }

        public override int GetHashCode()
        {
            return 0;
        }

        public int VratiID()
        {
            return this.UtakmicaID;
        }

        public string VratiIDNaziv()
        {
            return "UtakmicaID";
        }

        public List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac)
        {
            List<Utakmica> utakmice = new List<Utakmica>();
            List<StavkaStatistike> stavke = new List<StavkaStatistike>();
            while (citac.Read())
            {
                Utakmica u = new Utakmica
                {
                    UtakmicaID = (int)citac["UtakmicaID"],
                    BrojGledalaca = (int)citac["BrojGledalaca"],
                    DatumIVremeOdigravanja = DateTime.Parse(citac["DatumIVremeOdigravanja"].ToString()),
                    Protivnik = citac["Protivnik"].ToString(),
                   // Rezultat = citac["Rezultat"].ToString()
                    
                };



                // u bazi postoje i utakmice koje nemaju unesene stavke
                // pa koristimo left join da bi vratili sve utakmice
                // onda su te kolone statistike sve null
                if (citac["RedniBroj"] != DBNull.Value)
                {
                    StavkaStatistike s = new StavkaStatistike
                    {
                        Utakmica = u,
                        BrojAseva = (int)citac["BrojAseva"],
                        BrojBlokova = (int)citac["BrojBlokova"],
                        BrojGresaka = (int)citac["BrojGresaka"],
                        BrojGresakaUNapadu = (int)citac["BrojGresakaUNapadu"],
                        BrojGresakaUPrijemu = (int)citac["BrojGresakaUPrijemu"],
                        BrojNegativnihPrijema = (int)citac["BrojNegativnihPrijema"],
                        BrojPozitivnihPrijema = (int)citac["BrojPozitivnihPrijema"],
                        BrojRealizovanihNapada = (int)citac["BrojRealizovanihNapada"],
                        RedniBroj = (int)citac["RedniBroj"],
                        UkupanBrojGresaka = (int)citac["UkupanBrojGresaka"],
                        UkupanBrojNapada = (int)citac["UkupanBrojNapada"],
                        UkupanBrojPoena = (int)citac["UkupanBrojPoena"],
                        UkupanBrojPrijema = (int)citac["UkupanBrojPrijema"],
                        Igrac = new Igrac
                        {
                            IgracID = (int)citac["IgracID"],
                            BrojNaDresu = (int)citac["BrojNaDresu"],
                            DatumRodjenja = DateTime.Parse(citac["DatumRodjenja"].ToString()),
                            Ime = citac["Ime"].ToString(),
                            Kategorija = citac["Kategorija"].ToString(),
                            Pozicija = citac["Pozicija"].ToString(),
                            Prezime = citac["Prezime"].ToString()
                        }

                    };

                    stavke.Add(s);


                }
                


               




                if (!(utakmice.Contains(u)))
                {
                    utakmice.Add(u);
                }
            }
               


            foreach (var utakmica in utakmice)
            {
                foreach (var stavka in stavke)
                {
                    if(utakmica.UtakmicaID == stavka.Utakmica.UtakmicaID)
                    {
                        utakmica.StavkeStatistike.Add(stavka);
                    }

                }

            }




            return utakmice.ToList<IDomenskiObjekat>();
        }

        public string VratiNazivTabele()
        {
            return "Utakmica";
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            int slobodanID = 0;
            List<int> listaID = new List<int>();

            while (citac.Read())
            {
                int id = (int)citac["UtakmicaID"];
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
            return "u LEFT JOIN StavkaStatistike s ON u.UtakmicaID = s.UtakmicaID LEFT JOIN Igrac i ON s.IgracID = i.IgracID";
        }

        public string VratiUslovZaUpdate()
        {
            return $"UtakmicaID = {this.UtakmicaID}";
        }

        public string VratiVrednostiZaDelete()
        {
            throw new NotImplementedException();
        }

        public string VratiVrednostiZaInsert()
        {
            return $"{this.UtakmicaID},'{this.DatumIVremeOdigravanja}','{this.Protivnik}','{this.BrojGledalaca}'"; 
        }

        public string VratiVrednostiZaUpdate()
        {
            return $"UtakmicaID = {this.UtakmicaID}, BrojGledalaca = {this.BrojGledalaca}, DatumIVremeOdigravanja = '{this.DatumIVremeOdigravanja}'," +
                $" Protivnik = '{this.Protivnik}', Rezultat = '{this.Rezultat}'";
        }

        public string VratiZaPretragu()
        {
            throw new NotImplementedException();
        }

        public string VratiZaSelect()
        {
            return "u.UtakmicaID, u.BrojGledalaca, u.DatumIVremeOdigravanja, u.Protivnik, u.Rezultat, s.RedniBroj, s.BrojRealizovanihNapada," +
                "s.BrojGresakaUNapadu, s.UkupanBrojNapada, s.BrojPozitivnihPrijema, s.BrojNegativnihPrijema, s.BrojGresakaUPrijemu, s.UkupanBrojPrijema," +
                "s.BrojGresaka, s.BrojAseva, s.BrojBlokova, s.UkupanBrojGresaka, s.UkupanBrojPoena, i.IgracID, i.Ime, i.Prezime, i.DatumRodjenja, i.Kategorija," +
                "i.Pozicija,i.BrojNaDresu";
        }
    }
}
