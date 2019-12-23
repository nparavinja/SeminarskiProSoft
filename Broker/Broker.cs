using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace Broker
{
    public class Broker
    {
       
            private static Broker _instance;
            private SqlConnection konekcija;
            private SqlTransaction transakcija;

            private Broker()
            {
                string konekcioniString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seminarski;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                konekcija = new SqlConnection(konekcioniString);
            }

            public static Broker Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new Broker();
                    }
                    return _instance;
                }
            }

            public void OtvoriKonekciju()
            {
                konekcija.Open();
            }

            public void ZatvoriKonekciju()
            {
                konekcija.Close();
            }

            public void PocniTransakciju()
            {
                transakcija = konekcija.BeginTransaction();
            }

            public void PotvrdiTransakciju()
            {
                transakcija.Commit();
            }

            public void PonistiTransakciju()
            {
                transakcija.Rollback();
            }

            public List<IDomenskiObjekat> VratiSve(IDomenskiObjekat ido)
            {
                string upit = $"SELECT {ido.VratiZaSelect()} FROM {ido.VratiNazivTabele()} {ido.VratiUslovZaJoin()}";
                Debug.WriteLine("SELECT UPIT: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                SqlDataReader citac = komanda.ExecuteReader();
                List<IDomenskiObjekat> rezultat = ido.VratiListuObjekata(citac);
                citac.Close();
                return rezultat;
            }

           

            public bool Update(IDomenskiObjekat ido)
            {
                string upit = $"UPDATE {ido.VratiNazivTabele()} SET {ido.VratiVrednostiZaUpdate()}  WHERE  {ido.VratiUslovZaUpdate()}";
                Debug.WriteLine("UPDATE UPIT: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                int rezultat = komanda.ExecuteNonQuery();
                return rezultat == 1;
            }

            public bool Insert(IDomenskiObjekat ido)
            {
                string upit = $"INSERT INTO {ido.VratiNazivTabele()} VALUES ({VratiSledeciID(ido)},{ido.VratiVrednostiZaInsert()})";
                Debug.WriteLine("INSERT UPIT: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                int rezultat = komanda.ExecuteNonQuery();
                return rezultat == 1;
            }

            public bool InsertBezID(IDomenskiObjekat ido)
            {
                string upit = $"INSERT INTO {ido.VratiNazivTabele()} VALUES ({ido.VratiVrednostiZaInsert()})";
                Debug.WriteLine("INSERT UPIT: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                int rezultat = komanda.ExecuteNonQuery();
                return rezultat == 1;
            }


        public bool Delete(IDomenskiObjekat ido)
            {
                string upit = $"DELETE FROM {ido.VratiNazivTabele()} WHERE {ido.VratiVrednostiZaDelete()}";
                Debug.WriteLine("DELETE UPIT: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                int rezultat = komanda.ExecuteNonQuery();
                return rezultat == 1;
            }

            
             public List<IDomenskiObjekat> VratiSaUslovom(IDomenskiObjekat ido)
             {
                string upit = $"SELECT {ido.VratiZaSelect()} FROM {ido.VratiNazivTabele()} {ido.VratiUslovZaJoin()} WHERE {ido.VratiZaPretragu()}";
                Debug.WriteLine("SELECT UPIT SA USLOVOM: " + upit);
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                SqlDataReader citac = komanda.ExecuteReader();
                List<IDomenskiObjekat> rezultat = ido.VratiListuObjekata(citac);
                citac.Close();
                return rezultat;

             }

             public int VratiSledeciID(IDomenskiObjekat ido)
             {
                string upit = $"SELECT * FROM {ido.VratiNazivTabele()} ORDER BY {ido.VratiIDNaziv()} ASC";
                SqlCommand komanda = new SqlCommand(upit, konekcija, transakcija);
                SqlDataReader citac = komanda.ExecuteReader();
                int id = ido.VratiSledeciID(citac);
                citac.Close();
                
            



                return id;
             }      

    }
    
    }
