using System;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol.Core.Types;

namespace NuGet.Protocol
{
    public class MetadataResourceV2FeedProvider : ResourceProvider
    {
        public MetadataResourceV2FeedProvider()
            : base(typeof(MetadataResource), "MetadataResourceV2FeedProvider", "MetadataResourceV2Provider")
        {
        }

        public async override Task<Tuple<bool, INuGetResource>> TryCreate(SourceRepository source, CancellationToken token)
        {
            MetadataResource resource = null;

            if ((FeedTypeUtility.GetFeedType(source.PackageSource) & FeedType.HttpV2) != FeedType.None)
            {
                var httpSourceResource = await source.GetResourceAsync<HttpSourceResource>(token);
               
                resource = new MetadataResourceV2Feed(httpSourceResource, source.PackageSource);
            }

            return new Tuple<bool, INuGetResource>(resource != null, resource);
        }
    }
}
