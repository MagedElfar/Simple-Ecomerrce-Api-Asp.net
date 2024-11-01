﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Pagning
{
    public class PagningListDto<T> where T : class
    {
        public int Count {  get; set; }
        public List<T> Items { get; set; }
    }
}
