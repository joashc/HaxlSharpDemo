using HaxlSharp.Demo.Models;

namespace HaxlSharp.Demo.DataLayer.Requests
{
    public class GetPostInfo : Returns<PostInfo>
    {
        public readonly int PostId;
        public GetPostInfo(int postId)
        {
            PostId = postId;
        }
    }
}
