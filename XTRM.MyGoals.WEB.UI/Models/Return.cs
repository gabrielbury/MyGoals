using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTRM.MyGoals.WEB.UI.Models
{
    public class Return
    {
        public Guid ID { get; set; }
        public int Code { get; set; }
        public String Message { get; set; }
        public String Date { get; set; }
        public String Path { get; set; }
    }
}