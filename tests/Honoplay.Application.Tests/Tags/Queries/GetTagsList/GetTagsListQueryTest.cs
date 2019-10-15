﻿using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Tags.Queries.GetTagsList;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTagsListQueryHandler _getTagsListQueryHandler;
        private readonly Guid _tenantId;

        public GetTagsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getTagsListQueryHandler = new GetTagsListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId)
        {
            var context = GetDbContext();
            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Id = 1,
                Email = "test@omegabigdata.com",
                Password = "pass".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTimeOffset.Now.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);


            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUser.Id
            };
            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var question = new Question
            {
                Duration = 3,
                Text = "testQuestion",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            };
            context.Questions.Add(question);

            var tag = new Tag
            {
                CreatedBy = adminUser.Id,
                Name = "testTag",
                ToQuestion = true
            };
            context.Tags.Add(tag);

            var questionTag = new QuestionTag
            {
                QuestionId = question.Id,
                TagId = tag.Id
            };
            context.QuestionTags.Add(questionTag);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getTagsListQuery = new GetTagsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var tagModel = await _getTagsListQueryHandler.Handle(getTagsListQuery, CancellationToken.None);

            Assert.Null(tagModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getTagsListQuery = new GetTagsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var tagModel = await _getTagsListQueryHandler.Handle(getTagsListQuery, CancellationToken.None);

            Assert.Single(tagModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}