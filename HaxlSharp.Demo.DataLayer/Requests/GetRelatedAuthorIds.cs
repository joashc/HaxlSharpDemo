using System.Collections.Generic;

namespace HaxlSharp.Demo.DataLayer.Requests
{
    public class GetRelatedAuthorIds : Returns<IEnumerable<int>>
    {
        public readonly int AuthorId;
        public GetRelatedAuthorIds(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
