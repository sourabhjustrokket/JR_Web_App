using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JR_Web_App.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
