using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaxlSharp.Demo.Models
{
    public class PostInfo
    {
        public int PostId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime LastViewed { get; set; }
        public IEnumerable<int> RelatedPostIds { get; set; }
        public int AuthorId { get; set; }

        public PostInfo()
        {

        }

        public PostInfo(int postId, DateTime postDate, IEnumerable<int> relatedPostIds, int authorId, DateTime updated)
        {
            PostId = postId;
            PostDate = postDate;
            RelatedPostIds = new ShowList<int>(relatedPostIds);
            AuthorId = authorId;
            LastViewed = updated;
        }

        public override string ToString()
        {
            return $"PostInfo {{ PostId: {PostId}, PostDate: {PostDate}, LastViewed: {LastViewed}, RelatedPosts: {RelatedPostIds}, AuthorId: {AuthorId} }}";
        }
    }
}