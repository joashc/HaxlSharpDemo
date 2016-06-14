using HaxlSharp.Demo.DataLayer.Requests;
using HaxlSharp.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaxlSharp.Demo.DataLayer
{
    public static class DemoFetch
    {
        // ShowList constructor
        public static ShowList<A> ShowList<A>(IEnumerable<A> list)
        {
            return new ShowList<A>(list);
        }

        //  Primitive requests
        public static Fetch<IEnumerable<int>> GetAllPostIds() => new GetAllPostIds().ToFetch();
        public static Fetch<Author> GetAuthor(int authorId) => new GetAuthor(authorId).ToFetch();
        public static Fetch<PostContent> GetPostContent(int postId) => new GetPostContent(postId).ToFetch();
        public static Fetch<IEnumerable<int>> GetRelatedAuthors(int authorId) => new GetRelatedAuthorIds(authorId).ToFetch();
        public static Fetch<PostInfo> GetPostInfo(int postId) => new GetPostInfo(postId).ToFetch();
        public static Fetch<IEnumerable<int>> GetPostsByAuthor(int authorId) => new GetPostsByAuthor(authorId).ToFetch();

        // Composed requests
        public static Fetch<PostDetails>
        GetPostDetails(int postId) =>
            from info in GetPostInfo(postId)
            from content in GetPostContent(postId)
            select new PostDetails(info, content);

        public static Fetch<ShowList<PostDetails>>
        PostsByAuthor(int authorId) =>
            from postIds in GetPostsByAuthor(authorId)
            from details in postIds.SelectFetch(GetPostDetails)
            select ShowList(details);

        public static Fetch<ShowList<PostDetails>>
        PostsByRelatedAuthors(int authorId) =>
            from relatedAuthors in GetRelatedAuthors(authorId)
            from details in relatedAuthors.SelectFetch(PostsByAuthor)
            let flattened = details.SelectMany(d => d)
            select ShowList(flattened);

        public static Fetch<ShowList<PostDetails>> RelatedPosts(int postId) =>
            from info in GetPostInfo(postId)
            from authorRelatedPosts in PostsByRelatedAuthors(info.AuthorId)
            from relatedPosts in info.RelatedPostIds.SelectFetch(GetPostDetails)
            let allRelated = relatedPosts.Concat(authorRelatedPosts)
            select ShowList(allRelated);
    }
}
