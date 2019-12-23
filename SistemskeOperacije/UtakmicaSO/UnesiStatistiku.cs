using Domen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.UtakmicaSO
{
    public class UnesiStatistiku : SistemskaOperacijaBase
    {
        // update
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {

            // primamo dva argumenta, utakmicu koja treba da se izmeni, i utakmicu sa novim vrednostima
            // u utakmici treba da se apdejtuje:
            // obicna polja klase utakmica
            // lista igraca - poveca ili smanji broj igraca koji igraju utakmicu - Igra(IgracID,UtakmicaID)
            // isto tako lista trenera
            // lista stavki - dodaju se nove ili menjaju postojece - u tabeli StavkaStatistike (UtakmicaID,RedniBroj....)


            List<Domen.Utakmica> utakmice = objekat as List<Domen.Utakmica>;

            Domen.Utakmica staraUtakmica = utakmice[0];
            Domen.Utakmica novaUtakmica = utakmice[1];

            ObradiIgrace(staraUtakmica, novaUtakmica);
            ObradiTrenere(staraUtakmica, novaUtakmica);
            ObradiStavke(staraUtakmica, novaUtakmica);

             //apdejtujemo Utakmica(DatumIVremeOdigravanja,Protivnik,BrojGledalaca,Rezultat) po novoj utakmici
             bool uspesnoUpdateUtakmicu = Broker.Broker.Instance.Update(novaUtakmica);
            
            return uspesnoUpdateUtakmicu;
        }

        private void ObradiStavke(Domen.Utakmica staraUtakmica, Domen.Utakmica novaUtakmica)
        {
            // sada proveravamo da li ima vise stavki u staroj utakmici nego u novoj
            // ako ima vise u staroj to znaci da treba da izbrisemo redove iz utakmice - tabele StavkaStatistike (UtakmicaID,RedniBroj..)
            // ako ima vise u novoj onda radimo insert za te redove viska
           
            if (staraUtakmica.StavkeStatistike.Count > novaUtakmica.StavkeStatistike.Count)
            {
                var stavkeZaDelete = new List<StavkaStatistike>();
                //var stavkeZaUpdate = new List<StavkaStatistike>();

                foreach (var stavka in staraUtakmica.StavkeStatistike)
                {
                    if (!novaUtakmica.StavkeStatistike.Contains(stavka))
                    {
                        stavkeZaDelete.Add(stavka);
                    }
                    /*else
                    {
                        stavkeZaUpdate.Add(stavka);
                    }
                    */
                }

                foreach (var s in stavkeZaDelete)
                {
                   
                    Broker.Broker.Instance.Delete(s);

                }

                foreach (var s in novaUtakmica.StavkeStatistike)
                {
                    Broker.Broker.Instance.Update(s);

                }



            }
            else if (staraUtakmica.StavkeStatistike.Count < novaUtakmica.StavkeStatistike.Count)
            {
                var stavkeZaInsert = new List<StavkaStatistike>();
                var stavkeZaUpdate = new List<StavkaStatistike>();

                foreach (var stavka in novaUtakmica.StavkeStatistike)
                {
                    if (!staraUtakmica.StavkeStatistike.Contains(stavka))
                    {
                        stavkeZaInsert.Add(stavka);
                    }else
                    {
                        stavkeZaUpdate.Add(stavka);
                    }

                }

                foreach (var s in stavkeZaInsert)
                {
                    
                    Broker.Broker.Instance.InsertBezID(s);

                }

                foreach (var s in stavkeZaUpdate)
                {
                    Broker.Broker.Instance.Update(s);
                }
            }


            
        }

        private void ObradiTrenere(Domen.Utakmica staraUtakmica, Domen.Utakmica novaUtakmica)
        {
            // sada proveravamo da li ima vise trenera u staroj utakmici nego u novoj
            // ako ima vise u staroj to znaci da treba da izbrisemo redove iz utakmice - tabele Ucestvuje (TrenerID,UtakmicaID)
            // ako ima vise u novoj onda radimo insert za te redove viska
           
            if (staraUtakmica.Treneri.Count > novaUtakmica.Treneri.Count)
            {
                // samo ako je strogo veci broj igraca u staroj utakmici onda radimo delete, kad je jednako nema sta da se brise, samo update
                var treneriZaDelete = new List<Domen.Trener>();

                foreach (var trener in staraUtakmica.Treneri)
                {
                    if (!novaUtakmica.Treneri.Contains(trener))
                    {
                        treneriZaDelete.Add(trener);
                    }

                }

                foreach (var t in treneriZaDelete)
                {
                    Ucestvuje u = new Ucestvuje
                    {
                        Trener = t,
                        Utakmica = novaUtakmica
                    };

                    Broker.Broker.Instance.Delete(u);

                }



            }
            else if (staraUtakmica.Treneri.Count < novaUtakmica.Treneri.Count)
            {
                var treneriZaInsert = new List<Domen.Trener>();

                foreach (var trener in novaUtakmica.Treneri)
                {
                    if (!staraUtakmica.Treneri.Contains(trener))
                    {
                        treneriZaInsert.Add(trener);
                    }

                }

                foreach (var t in treneriZaInsert)
                {
                    Ucestvuje u = new Ucestvuje
                    {
                        Trener = t,
                        Utakmica = novaUtakmica
                    };


                    Broker.Broker.Instance.InsertBezID(u);

                }
            }

        }

        private void ObradiIgrace(Domen.Utakmica staraUtakmica, Domen.Utakmica novaUtakmica)
        {
            // sada proveravamo da li ima vise igraca u staroj utakmici nego u novoj
            // ako ima vise u staroj to znaci da treba da izbrisemo redove iz utakmice - tabele Igra (IgracID,UtakmicaID)
            // ako ima vise u novoj onda radimo insert za te redove viska
            // svakako radimo update za redove koji su isti u obe utakmice, mozda je nesto dodato
            if (staraUtakmica.Igraci.Count > novaUtakmica.Igraci.Count)
            {
                // samo ako je strogo veci broj igraca u staroj utakmici onda radimo delete, kad je jednako nema sta da se brise, samo update
                var igraciZaDelete = new List<Domen.Igrac>();

                foreach (var igrac in staraUtakmica.Igraci)
                {
                    if (!novaUtakmica.Igraci.Contains(igrac))
                    {
                        igraciZaDelete.Add(igrac);
                    }

                }

                foreach (var igrac in igraciZaDelete)
                {
                    Igra igra = new Igra
                    {
                        Igrac = igrac,
                        Utakmica = novaUtakmica
                    };

                    Broker.Broker.Instance.Delete(igra);

                }



            }
            else if (staraUtakmica.Igraci.Count < novaUtakmica.Igraci.Count)
            {
                var igraciZaInsert = new List<Domen.Igrac>();

                foreach (var igrac in novaUtakmica.Igraci)
                {
                    if (!staraUtakmica.Igraci.Contains(igrac))
                    {
                        igraciZaInsert.Add(igrac);
                    }

                }

                foreach (var igrac in igraciZaInsert)
                {
                    Igra igra = new Igra
                    {
                        Igrac = igrac,
                        Utakmica = novaUtakmica
                    };

                    Broker.Broker.Instance.InsertBezID(igra);

                }
            }





        }
    }
}
