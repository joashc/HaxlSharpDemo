using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaxlSharp.Demo.Models;

namespace HaxlSharp.Demo.DataLayer
{
    public class PostApiClient
    {
        public async Task<IEnumerable<int>> GetAllPostIds()
        {
            await Task.Delay(10);
            var ids = new ShowList<int>();
            ids.AddRange(Enumerable.Range(0, 20));
            return ids;
        }

        public async Task<PostContent> GetPostContent(int postId)
        {
            await Task.Delay(10);
            return new PostContent
            {
                Content = $"This is Post {postId}, and the content is just some lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                PostId = postId,
                Title = $"Post {postId}"
            };
        }

        public async Task<PostInfo> GetPostInfo(int postId)
        {
            await Task.Delay(10);
            var postDate = new DateTime(2016, 06, 01).Subtract(TimeSpan.FromDays(postId));
            return new PostInfo(postId, postDate, new List<int> { postId * 3 % 20, postId * 7 % 20}, postId + 100, DateTime.Now);
        }
    }
}
