﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Extensions.Data.Tests
{
    using Models;
    using Stores;

    public class TestStoreInitializer : StoreInitializer<TestStoreIdentifier>
    {
        private IList<Category> _categories;


        public TestStoreInitializer(IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(identifier, loggerFactory)
        {
        }


        protected override void InitializeCore<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId>
            (StoreHub<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId> stores)
        {
            base.InitializeCore(stores);

            if (stores.Accessor is TestDbContextAccessor dbContextAccessor)
            {
                InitializeCategories(dbContextAccessor);

                InitializeArticles(dbContextAccessor);
            }
        }

        private void InitializeCategories(TestDbContextAccessor accessor)
        {
            if (!accessor.Categories.Any())
            {
                _categories = new List<Category>
                {
                    new Category
                    {
                        Name = $"First {nameof(Category)}",
                        CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, isUtc: true).ConfigureAndResult(),
                        CreatedBy = GetType().GetDisplayName()
                    },
                    new Category
                    {
                        Name = $"Last {nameof(Category)}",
                        CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, isUtc: true).ConfigureAndResult(),
                        CreatedBy = GetType().GetDisplayName()
                    }
                };

                _categories.ForEach(category =>
                {
                    category.CreatedTimeTicks = category.CreatedTime.Ticks;
                });

                accessor.Categories.AddRange(_categories);
                RequiredSaveChanges = true;
            }
            else
            {
                _categories = accessor.Categories.ToList();
            }
        }

        private void InitializeArticles(TestDbContextAccessor stores)
        {
            if (!stores.Categories.Any())
            {
                var articles = new List<Article>();

                for (int i = 0; i < 100; i++)
                {
                    var article = new Article
                    {
                        Id = Identifier.GetArticleIdAsync().ConfigureAndResult(),
                        Title = $"{nameof(Article)} {i.FormatString(3)}",
                        Descr = $"{nameof(Article.Descr)} {i.FormatString(3)}",
                        Category = (i < 50) ? _categories.First() : _categories.Last(),
                        CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, isUtc: true).ConfigureAndResult(),
                        CreatedBy = GetType().GetDisplayName()
                    };

                    article.CreatedTimeTicks = article.CreatedTime.Ticks;

                    articles.Add(article);
                }

                stores.Articles.AddRange(articles);
                RequiredSaveChanges = true;
            }
        }

    }
}
