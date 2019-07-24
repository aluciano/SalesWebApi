using System;
using System.Collections.Generic;

namespace SalesWebApi.Models.ServiceModels
{
    public class SalesRecordByDateIncluding
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public List<String> IncludeList { get; set; }
        public bool GroupBySellerDepartment { get; set; }
    }
}
