﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrijsOfferteApp
{
    internal class Bedrijf
    {
        public int BedrijfId { get; set; }
        public string BedrijfNaam { get; set; }
        public int NrTva { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public int NrTel { get; set; }

       
    }
}
