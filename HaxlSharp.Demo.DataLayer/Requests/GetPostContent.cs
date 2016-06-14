using HaxlSharp.Demo.Models;

namespace HaxlSharp.Demo.DataLayer.Requests
{
    public class GetPostContent : Returns<PostContent>
    {
        public readonly int PostId;
        public GetPostContent(int postId)
        {
            PostId = postId;
        }
    }
}
