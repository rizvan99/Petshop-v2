using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Filter
{
    public class FilteredList<T>
    {
        public FilterSearch FilterUsed { get; set; }

        public int TotalCount { get; set; }

        public int TotalFound { get; set; }

        public List<T> List { get; set; }
    }
}
