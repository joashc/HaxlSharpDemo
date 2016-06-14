# HaxlSharp Demo
This projects demonstrates how to integrate your data sources into HaxlSharp.

## Existing data sources
Your existing data sources can be a remote API call, a database call, a file read, etc. The project `HaxlSharp.Demo.DataLayer` has a folder called `ApiClients` that contain mock API clients that return some data after a simulated 10ms delay. Here's an example method:

```cs
public async Task<IEnumerable<int>> GetAllPostIds()
{
    await Task.Delay(10);
    return Enumerable.Range(0, 20);
}
```

We'll use these mock API clients as our existing data sources.

## Requests
For each API method we want to integrate, we create a request class, which is just a POCO annotated with its return type:

```cs
public class GetAllPostIds : Returns<IEnumerable<int>> { }
```

Requests that need parameters should include them in their constructor:

```cs
public class GetAuthor : Returns<Author>
{
    public readonly int AuthorId;
    public GetAuthor(int authorId)
    {
        AuthorId = authorId;
    }
}
```

You can find the request objects in the `Requests` folder of the `HaxlSharp.Demo.DataLayer` project.

## Fetch functions
We don't use the request objects directly; instead, we create fetch functions out of them. The file `DemoFetch.cs` in the project `HaxlSharp.Demo.DataLayer` contains some fetch functions constructed from request objects:

```cs
public static Fetch<IEnumerable<int>> GetAllPostIds() => new GetAllPostIds().ToFetch();
public static Fetch<Author> GetAuthor(int authorId) => new GetAuthor(authorId).ToFetch();
public static Fetch<PostContent> GetPostContent(int postId) => new GetPostContent(postId).ToFetch();
public static Fetch<PostInfo> GetPostInfo(int postId) => new GetPostInfo(postId).ToFetch();
```

Once these fetch functions have been created, they can be combined using query syntax. For instance:

```cs
public static Fetch<PostDetails>
GetPostDetails(int postId) =>
    from info in GetPostInfo(postId)
    from content in GetPostContent(postId)
    select new PostDetails(info, content);
```

This function returns a `Fetch<PostDetails>` by combining two primitive requests. We can combine this request with others as if it was a primitive request:

```cs
public static Fetch<IEnumerable<PostDetails>>
PostsByAuthor(int authorId) =>
    from postIds in GetPostsByAuthor(authorId)
    from details in postIds.SelectFetch(GetPostDetails)
    select details;
```

## Connecting the data sources
Of course, if we actually want to fetch any data, we must link the request objects- the ones we annotated with `Returns<>`- with the actual data fetch functions.

We register these handlers in the `RegisterHandlers.cs` in the `HaxlSharp.Demo.DataLayer` project by using the `FetcherBuilder` class:

```cs
FetcherBuilder.New()
    .FetchRequest<GetAllPostIds, IEnumerable<int>>(_ => _postApi.GetAllPostIds())
    .FetchRequest<GetPostContent, PostContent>(req => _postApi.GetPostContent(req.PostId))
    .FetchRequest<GetPostInfo, PostInfo>(req => _postApi.GetPostInfo(req.PostId))
    .FetchRequest<GetAuthor, Author>(req => _friendApi.GetAuthor(req.AuthorId))
    .FetchRequest<GetPostsByAuthor, IEnumerable<int>>(req => _friendApi.PostsByAuthor(req.AuthorId))
    .FetchRequest<GetRelatedAuthorIds, IEnumerable<int>>(req => _friendApi.GetRelatedAuthorIds(req.AuthorId))
    .LogWith(log => Debug.WriteLine(log.ToDefaultString()))
    .Create();
```

This builder links request objects with functions that take that request object and return the requested data. It also optionally registers a function that handles log output from HaxlSharp. Calling `Create()` on the builder will return a `HaxlFetch` object, that can be injected anywhere we want to resolve `Fetch<>` objects.

## Using the Fetcher
The `HaxlSharp.Demo.Test` demonstrates fetching. We create a `HaxlFetcher` object, and then use it to resolve our `Fetch<>` objects:

```cs
private readonly HaxlFetcher _fetcher;
public DemoTests()
{
    _fetcher = new RegisterHandlers(new FriendApiClient(), new PostApiClient()).GetFetcher();
}

public async Task PostsByAuthor3()
{
    var postsByAuthor3 = DemoFetch.PostsByAuthor(3);
    var result = await _fetcher.Fetch(postsByAuthor3);
}
```

It's also possible to construct `Fetch<>` objects inline:

```cs
var allPostDetails = await _fetcher.Fetch(
    from postIds in DemoFetch.GetAllPostIds()
    from viewed in postIds.SelectFetch(DemoFetch.GetPostDetails)
    select viewed
);
```

## Injecting the fetcher
The project `HaxlSharp.Demo.WebApi` demonstrates how we can inject the fetcher into our controllers.
