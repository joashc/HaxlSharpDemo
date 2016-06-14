using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaxlSharp.Demo.Models
{
    /// <summary>
    /// Simple pretty printing list
    /// </summary>
    public class ShowList<A> : List<A>
    {
        public ShowList(IEnumerable<A> list)
        {
            AddRange(list);
        }

        public ShowList()
        {

        }

        public override string ToString()
        {
            if (!this.Any()) return "[]";

            var builder = new StringBuilder();
            builder.Append("[ ");
            var first = this.First();
            var rest = this.Skip(1);
            builder.Append(first);
            foreach (var item in rest)
            {
                builder.Append($", {item}");
            }
            builder.Append(" ]");
            return builder.ToString();
        }

    }
}
