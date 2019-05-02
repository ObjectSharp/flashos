using System.Collections.Generic;

namespace Flash.Core.Models
{
    public class Batch<T> where T : class, new()
    {
        public IEnumerable<T> Data { get; set; }
    }
}
