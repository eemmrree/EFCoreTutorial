using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTutorial.WebApi
{
    public class StudentFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Number { get; set; }
    }
}
