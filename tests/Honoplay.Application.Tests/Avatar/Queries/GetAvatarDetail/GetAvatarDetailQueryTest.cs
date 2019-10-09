using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Avatars.Queries.GetAvatarDetail
{
    public class GetAvatarDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetAvatarDetailQueryHandler _getAvatarDetailQueryHandler;
        private readonly int _avatarId;

        public GetAvatarDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _avatarId);
            _getAvatarDetailQueryHandler = new GetAvatarDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out int avatarId)
        {
            var context = GetDbContext();
            var avatar = new Avatar
            {
                Name = "Avatar1",
                ImageBytes = new byte[] { 0x20 }
            };
            context.Avatars.Add(avatar);
            context.SaveChanges();
            avatarId = avatar.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var contentFileDetailQuery = new GetAvatarDetailQuery(_avatarId);

            var contentFileDetailResponseModel = await _getAvatarDetailQueryHandler.Handle(contentFileDetailQuery, CancellationToken.None);

            Assert.Null(contentFileDetailResponseModel.Errors);
            Assert.Equal(expected: _context.Avatars.First().Name, actual: contentFileDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var contentFileDetailQuery = new GetAvatarDetailQuery(1 + _avatarId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getAvatarDetailQueryHandler.Handle(contentFileDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
