﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Options
{
    public class StripeOption
    {
        public string Publishablekey {  get; set; }
        public string Secretkey { get; set; }

        public string WebHookSecret { get; set; }
    }

}
