using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kangaroo.Models
{
    public class ProjectHoursByUserModel
    {
        public string Project { get; set; }
        public string UserName { get; set; }
        public decimal Hours { get; set; }
    }
}
