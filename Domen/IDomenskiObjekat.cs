using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    
    public interface IDomenskiObjekat
    {
        
        string VratiNazivTabele();
        int VratiID();
        int VratiSledeciID(SqlDataReader citac);
        List<IDomenskiObjekat> VratiListuObjekata(SqlDataReader citac);
        string VratiUslovZaJoin();
        string VratiVrednostiZaUpdate();
        string VratiUslovZaUpdate();
        string VratiVrednostiZaInsert();
        string VratiVrednostiZaDelete();
        string VratiZaSelect();
        string VratiZaPretragu();
        string VratiIDNaziv();

    }
}
