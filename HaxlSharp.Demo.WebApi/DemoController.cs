using HaxlSharp.Demo.DataLayer;
using HaxlSharp.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HaxlSharp.Demo.WebApi.Controllers
{

    public class DemoController : ApiController
    {
        private readonly HaxlFetcher _fetcher;
        public DemoController(HaxlFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        [HttpGet]
        [Route("post/{postId}/details")]
        public async Task<PostDetails> PostDetails(int postId)
        {
            var getPostDetails = from info in DemoFetch.GetPostInfo(postId)
                                 from content in DemoFetch.GetPostContent(postId)
                                 select new PostDetails { PostInfo = info, PostContent = content };
            return await _fetcher.Fetch(getPostDetails);
        }

        [HttpGet]
        [Route("post/authorFriends/{postId}")]
        public Task<ShowList<PostDetails>> GetPostsByAuthor(int postId)
        {
            return _fetcher.Fetch(DemoFetch.PostsByAuthor(postId));
        }

        [HttpGet]
        [Route("post/authorFriendsViewed/{postId}")]
        public Task<ShowList<PostDetails>> GetPostsByRelatedAuthors(int postId)
        {
            return _fetcher.Fetch(DemoFetch.PostsByRelatedAuthors(postId));
        }

        [HttpGet]
        [Route("post/getAllAuthorFriendsViewed")]
        public Task<IEnumerable<PostDetails>> GetAllPostDetails()
        {
            return _fetcher.Fetch(
                from postIds in DemoFetch.GetAllPostIds()
                from viewed in postIds.SelectFetch(DemoFetch.GetPostDetails)
                select viewed
            );
        }
    }
}
