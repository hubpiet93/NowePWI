using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NowePWI.Models
{
    public class MojaEncja
    {
        [Key]
        public int IDEncji { get; set; }

        public string Tytul { get; set; }
        public string Opis { get; set; }

    }
}