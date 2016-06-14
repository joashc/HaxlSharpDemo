using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HaxlSharp.Demo.DataLayer;
using System.Threading.Tasks;
using HaxlSharp.Internal;
using System.Diagnostics;

namespace HaxlSharp.Demo.Test
{
    [TestClass]
    public class DemoTests
    {
        private readonly HaxlFetcher _fetcher;
        public DemoTests()
        {
            _fetcher = new RegisterHandlers(new FriendApiClient(), new PostApiClient()).GetFetcher();
        }

        [TestMethod]
        public async Task PostsByAuthor()
        {
            var fetch = DemoFetch.PostsByAuthor(3);
            var result = await _fetcher.Fetch(fetch);
        }

        [TestMethod]
        public async Task PostsByRelatedAuthors()
        {
            var fetch = DemoFetch.PostsByRelatedAuthors(18);
            var result = await _fetcher.Fetch(fetch);
        }

        [TestMethod]
        public async Task RelatedPosts()
        {
            var fetch = DemoFetch.RelatedPosts(3);
            var result = await _fetcher.Fetch(fetch);
        }

        [TestMethod]
        public async Task Author()
        {
            var fetch = DemoFetch.GetAuthor(3);
            var result = await _fetcher.Fetch(fetch);
        }

        [TestMethod]
        public async Task Details()
        {
            var fetch = DemoFetch.GetPostDetails(3);
            var result = await _fetcher.Fetch(fetch);
        }

    }
}
