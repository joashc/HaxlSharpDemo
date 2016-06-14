using HaxlSharp.Demo.DataLayer.Requests;
using HaxlSharp.Demo.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace HaxlSharp.Demo.DataLayer
{
    public class RegisterHandlers
    {
        private readonly FriendApiClient _friendApi;
        private readonly PostApiClient _postApi;
        public RegisterHandlers(FriendApiClient friendApi, PostApiClient postApi)
        {
            _friendApi = friendApi;
            _postApi = postApi;
        }

        public HaxlFetcher GetFetcher()
        {
            return FetcherBuilder.New()
                .FetchRequest<GetAllPostIds, IEnumerable<int>>(_ => _postApi.GetAllPostIds())
                .FetchRequest<GetPostContent, PostContent>(req => _postApi.GetPostContent(req.PostId))
                .FetchRequest<GetPostInfo, PostInfo>(req => _postApi.GetPostInfo(req.PostId))
                .FetchRequest<GetAuthor, Author>(req => _friendApi.GetAuthor(req.AuthorId))
                .FetchRequest<GetPostsByAuthor, IEnumerable<int>>(req => _friendApi.PostsByAuthor(req.AuthorId))
                .FetchRequest<GetRelatedAuthorIds, IEnumerable<int>>(req => _friendApi.GetRelatedAuthorIds(req.AuthorId))
                .LogWith(log => Debug.WriteLine(log.ToDefaultString()))
                .Create();
        }

    }
}
