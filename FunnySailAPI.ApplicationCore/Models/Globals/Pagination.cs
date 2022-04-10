using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Utils
{
    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Page { get; set; }

        public Pagination()
        {
            Limit = 20;
            Offset = 0;
            Page = 0;
        }
    }
}
