using System.Collections.Generic;

namespace HaxlSharp.Demo.DataLayer.Requests
{
    public class GetPostsByAuthor : Returns<IEnumerable<int>>
    {
        public readonly int AuthorId;
        public GetPostsByAuthor(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
