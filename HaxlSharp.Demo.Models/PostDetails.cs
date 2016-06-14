namespace HaxlSharp.Demo.Models
{
    public class PostDetails
    {
        public PostDetails(PostInfo info, PostContent content)
        {
            PostInfo = info;
            PostContent = content;
        }

        public PostDetails()
        {

        }

        public PostInfo PostInfo { get; set; }
        public PostContent PostContent { get; set; }
    }
}
