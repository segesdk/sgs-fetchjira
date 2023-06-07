using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchJiraLogic.dto
{
    public class Child
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Customfield10034
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
        public Child child { get; set; }
    }
}
