using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entites
{
    public class HotDeal
    {
        public int Id { set; get; }
        public DateTime HotDealActionStartTime { set; get; }
        public DateTime HotDealActionEndTime { set; get; }
        public DateTime HotDealTime { set; get; }
        public string HotDealToProducts { set; get; }
        public bool IsHotDeal { set; get; }
    }
}
