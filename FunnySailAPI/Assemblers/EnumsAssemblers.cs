using FunnySailAPI.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunnySailAPI.Assemblers
{
    public static class EnumsAssemblers
    {
        public static IList<EnumsOutputDTO> Convert<T>() where T : Enum
        {
            var result = new List<EnumsOutputDTO>();
            var values = Enum.GetValues(typeof(T));

            foreach (int value in values)
                result.Add(new EnumsOutputDTO
                {
                    Key = Enum.GetName(typeof(T), value),
                    Value = value
                });

            return result;
        }
    }
}
