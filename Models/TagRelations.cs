using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JR_Web_App.Models
{
    public class TagRelations
    {
        public int Id { get; set; }
        public Tag FamilyTag { get; set; }
        public List<Tag> FamilyTagMembers { get; set; }
    }
}
