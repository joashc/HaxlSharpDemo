using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaxlSharp.Demo.Models
{
    public class PostContent
    {
        public string Title { get; set; }
        public string Content {get;set;}
        public int PostId { get; set; }

        public override string ToString()
        {
            return $"Post {{ Title: {Title}, Content: {Content}, PostId: {PostId} }}";
        }
    }
}
