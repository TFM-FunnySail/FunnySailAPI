using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output
{
    public class GenericResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }

        public GenericResponseDTO(IEnumerable<T> items,int limit, int offset, int total)
        {
            Items = items;
            Limit = limit;
            Offset = offset;
            Total = total;
        }
    }
}
