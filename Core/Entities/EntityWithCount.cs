using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EntityWithCount<T>
    {
        public IEnumerable<T> Rows { get; set; }

        public int Count { get; set; }
    }
}
