﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije.Trener
{
    public class ZapamtiTreneraSO : SistemskaOperacijaBase
    {
        protected override object IzvrsiKonkretnuOperaciju(object objekat)
        {
            bool uspesno = Broker.Broker.Instance.Insert((Domen.IDomenskiObjekat)objekat);
            return uspesno;
        }
    }
}
