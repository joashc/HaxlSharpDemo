using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaxlSharp.Demo.Models
{
    public class Author
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }

        public override string ToString()
        {
            return $"Author {{ Name: {Name} }}";
        }
    }
}
