using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Broker;

namespace SistemskeOperacije
{
    public abstract class SistemskaOperacijaBase
    {
        private object _rezultat;
        

        public object IzvrsiSO(object objekat)
        {
            try
            {
                Broker.Broker.Instance.OtvoriKonekciju();
                Broker.Broker.Instance.PocniTransakciju();
                _rezultat = IzvrsiKonkretnuOperaciju(objekat);
                Broker.Broker.Instance.PotvrdiTransakciju();
                return _rezultat;
            }
            catch (Exception ex)
            {
                Broker.Broker.Instance.PonistiTransakciju();
                Debug.WriteLine("GRESKA U IZVRSENJU SO: " + ex.Message);
                return ex.Message;
            }
            finally
            {
                Broker.Broker.Instance.ZatvoriKonekciju();
            }


        }

        protected abstract object IzvrsiKonkretnuOperaciju(object objekat);
    }
}
