using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class StavkaStatistike : IDomenskiObjekat
    {

        public Utakmica Utakmica { get; set; }
        public Igrac Igrac { get; set; }

        public int RedniBroj { get; set; }

        public int BrojRealizovanihNapada { get; set; }
        public int BrojGresakaUNapadu { get; set; }
        public int UkupanBrojNapada { get; set; }
        public int BrojPozitivnihPrijema { get; set; }
        public int BrojNegativnihPrijema { get; set; }
        public int BrojGresakaUPrijemu { get; set; }
        public int UkupanBrojPrijema { get; set; }
        public int BrojAseva { get; set; }
        public int BrojGresaka { get; set; }
        public int BrojBlokova { get; set; }
        public int UkupanBrojGresaka { get; set; }
        public int UkupanBrojPoena { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if(obj is StavkaStatistike s)
            {
                return (s.Utakmica.UtakmicaID == this.Utakmica.UtakmicaID) && (s.RedniBroj == this.RedniBroj);
            }


            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

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
            throw new NotImplementedException();
        }

        public string VratiNazivTabele()
        {
            return "StavkaStatistike";
        }

        public int VratiSledeciID()
        {
            throw new NotImplementedException();
        }

        public int VratiSledeciID(SqlDataReader citac)
        {
            throw new NotImplementedException();
        }

        public string VratiUslovZaJoin()
        {
            throw new NotImplementedException();
        }

        public string VratiUslovZaUpdate()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND RedniBroj = {this.RedniBroj}";
        }

        public string VratiVrednostiZaDelete()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID} AND RedniBroj = {this.RedniBroj}";

        }

        public string VratiVrednostiZaInsert()
        {
            return $"{this.Utakmica.UtakmicaID},{this.RedniBroj},{this.BrojRealizovanihNapada},{this.BrojGresakaUNapadu},{this.UkupanBrojNapada}," +
                $"{this.BrojPozitivnihPrijema}, {this.BrojNegativnihPrijema}, {this.BrojGresakaUPrijemu}, {this.UkupanBrojPrijema} , {this.BrojAseva}," +
                $"{this.BrojGresaka}, {this.BrojBlokova} ,{this.UkupanBrojGresaka}, {this.UkupanBrojPoena}, {this.Igrac.IgracID}";
        }

        public string VratiVrednostiZaUpdate()
        {
            return $"UtakmicaID = {this.Utakmica.UtakmicaID}, RedniBroj = {this.RedniBroj}, BrojRealizovanihNapada = {this.BrojRealizovanihNapada}" +
                $", BrojGresakaUNapadu = {this.BrojGresakaUNapadu}, UkupanBrojNapada = {this.UkupanBrojNapada}, BrojPozitivnihPrijema = {this.BrojPozitivnihPrijema}," +
                $" BrojNegativnihPrijema = {this.BrojNegativnihPrijema}, BrojGresakaUPrijemu = {this.BrojGresakaUPrijemu}, UkupanBrojPrijema = {this.UkupanBrojPrijema}," +
                $"BrojAseva = {this.BrojAseva}, BrojGresaka = {this.BrojGresaka}, BrojBlokova = {this.BrojBlokova}, UkupanBrojGresaka = {this.UkupanBrojGresaka}," +
                $"UkupanBrojPoena = {this.UkupanBrojPoena}, IgracID = {this.Igrac.IgracID}";
        }

        public string VratiZaPretragu()
        {
            throw new NotImplementedException();
        }

        public string VratiZaSelect()
        {
            throw new NotImplementedException();
        }
    }
}
