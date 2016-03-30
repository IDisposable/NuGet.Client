using System;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol.Core.Types;

namespace NuGet.Protocol
{
    public class DownloadResourceV2FeedProvider : ResourceProvider
    {
        public DownloadResourceV2FeedProvider()
            : base(typeof(DownloadResource), "DownloadResourceV2FeedProvider", "DownloadResourceV2Provider")
        {
        }

        public async override Task<Tuple<bool, INuGetResource>> TryCreate(SourceRepository source, CancellationToken token)
        {
            DownloadResource resource = null;

            if ((FeedTypeUtility.GetFeedType(source.PackageSource) & FeedType.HttpV2) != FeedType.None)
            {
                var httpSourceResource = await source.GetResourceAsync<HttpSourceResource>(token);

                resource = new DownloadResourceV2Feed(httpSourceResource, source.PackageSource);
            }

            return new Tuple<bool, INuGetResource>(resource != null, resource);
        }
    }
}
