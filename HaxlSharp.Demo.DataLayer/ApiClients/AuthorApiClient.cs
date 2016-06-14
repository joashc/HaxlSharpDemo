using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaxlSharp.Demo.Models;

namespace HaxlSharp.Demo.DataLayer
{
    public class FriendApiClient
    {
        public static List<string> Names = new List<string>
        {
            "Cherry Greenburg",
            "Alison Herald",
            "Michal Zakrzewski",
            "Chance Kehoe",
            "Delaine Crago",
            "Sabina Barrs",
            "Peg Delosh",
            "Johnie Wengerd",
            "Shayne Knauer",
            "Tyson Dave",
            "Shandra Hanlin",
            "Rey Pita",
            "Jacquelyn Bivona",
            "Cristal Hornak",
            "Julieta Kilbane",
            "Terry Cavin",
            "Peppa Pig",
            "Charity Gadsden",
            "Antione Domingo",
            "Corazon Benito",
            "Tianna Bratton",
        };

        public async Task<IEnumerable<int>> PostsByAuthor(int authorId)
        {
            await Task.Delay(10);
            return new ShowList<int>
            {
                (authorId * 11) % 19,
                (authorId * 3) % 19,
                (authorId * 7) % 19
            };
        }

        public async Task<IEnumerable<int>> GetRelatedAuthorIds(int authorId)
        {
            await Task.Delay(10);
            return new ShowList<int>
            {
                (authorId + 5)%20,
                (authorId + 7)%20,
                (authorId + 3)%20
            };
        }

        public async Task<Author> GetAuthor(int authorId)
        {
            await Task.Delay(10);
            var nameIndex = authorId % 20;
            return new Author
            {
                Name = Names.ElementAt(nameIndex),
                AuthorId = authorId
            };
        }
    }
}
