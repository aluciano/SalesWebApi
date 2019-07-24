using System.Collections.Generic;

namespace SalesWebApi.Models.ServiceModels
{
    public class MyGrouping
    {
        public Department Key { get; set; }
        public IEnumerable<SalesRecord> Sales { get; set; }
    }
}
