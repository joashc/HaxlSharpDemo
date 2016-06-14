using HaxlSharp.Demo.Models;

namespace HaxlSharp.Demo.DataLayer.Requests
{
    public class GetAuthor : Returns<Author>
    {
        public readonly int AuthorId;
        public GetAuthor(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
