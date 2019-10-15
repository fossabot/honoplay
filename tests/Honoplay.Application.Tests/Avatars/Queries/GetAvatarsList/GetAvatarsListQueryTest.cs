using Honoplay.Application.Avatars.Queries.GetAvatarsList;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Avatars.Queries.GetAvatarsList
{
    public class GetAvatarsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetAvatarsListQueryHandler _getAvatarsListQueryHandler;

        public GetAvatarsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext();
            _getAvatarsListQueryHandler = new GetAvatarsListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            var avatar = new Avatar
            {
                Name = "Avatar1",
                ImageBytes = new byte[] { 0x20 }
            };
            context.Avatars.Add(avatar);
            context.SaveChanges();
            return context;
        }
        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getAvatarsListQuery = new GetAvatarsListQuery(skip: 0, take: 10);

            var avatarModel = await _getAvatarsListQueryHandler.Handle(getAvatarsListQuery, CancellationToken.None);

            Assert.Null(avatarModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getAvatarsListQuery = new GetAvatarsListQuery(skip: 0, take: 1);

            var avatarModel = await _getAvatarsListQueryHandler.Handle(getAvatarsListQuery, CancellationToken.None);

            Assert.Single(avatarModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
