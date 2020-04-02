/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/tidyui/coreweb
 *
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Piranha.AttributeBuilder;
using Piranha.Extend;

namespace Piranha.Tests.AttributeBuilder
{
    [Collection("Integration tests")]
    public class TypeBuilderTests : BaseTestsAsync
    {

        [PageType(Id = "Simple", Title = "Simple Page Type")]
        public class SimplePageType
        {
            [Region]
            public Extend.Fields.TextField Body { get; set; }
        }

        [PageType(Id = "Complex", Title = "Complex Page Type")]
        [PageTypeRoute(Title = "Default", Route = "/complex")]
        [PageTypeEditor(Title = "Custom Editor", Component = "custom-editor", Icon = "fas fa-fish")]
        public class ComplexPageType
        {
            public class BodyRegion
            {
                [Field]
                public Extend.Fields.TextField Title { get; set; }
                [Field]
                public Extend.Fields.TextField Body { get; set; }
            }

            [Region(Title = "Intro")]
            public IList<Extend.Fields.TextField> Slider { get; set; }

            [Region(Title = "Main content")]
            public BodyRegion Content { get; set; }
        }

        [PostType(Id = "Simple", Title = "Simple Post Type")]
        public class SimplePostType
        {
            [Region]
            public Extend.Fields.TextField Body { get; set; }
        }

        [PostType(Id = "Complex", Title = "Complex Post Type")]
        [PostTypeRoute(Title = "Default", Route = "/complex")]
        [PostTypeEditor(Title = "Custom Editor", Component = "custom-editor", Icon = "fas fa-fish")]
        public class ComplexPostType
        {
            public class BodyRegion
            {
                [Field]
                public Extend.Fields.TextField Title { get; set; }
                [Field]
                public Extend.Fields.TextField Body { get; set; }
            }

            [Region(Title = "Intro")]
            public IList<Extend.Fields.TextField> Slider { get; set; }

            [Region(Title = "Main content")]
            public BodyRegion Content { get; set; }
        }

        [SiteType(Id = "Simple", Title = "Simple Page Type")]
        public class SimpleSiteType
        {
            [Region]
            public Extend.Fields.TextField Body { get; set; }
        }

        [SiteType(Id = "Complex", Title = "Complex Page Type")]
        public class ComplexSiteType
        {
            public class BodyRegion
            {
                [Field]
                public Extend.Fields.TextField Title { get; set; }
                [Field]
                public Extend.Fields.TextField Body { get; set; }
            }

            [Region(Title = "Intro")]
            public IList<Extend.Fields.TextField> Slider { get; set; }

            [Region(Title = "Main content")]
            public BodyRegion Content { get; set; }
        }

        public override Task InitializeAsync()
        {
            return Task.Run(() =>
            {
                using (var api = CreateApi())
                {
                    Piranha.App.Init(api);
                }
            });
        }

        public override async Task DisposeAsync()
        {
            using (var api = CreateApi())
            {
                var types = await api.PageTypes.GetAllAsync();
                foreach (var t in types)
                {
                    await api.PageTypes.DeleteAsync(t);
                }

                var siteTypes = await api.SiteTypes.GetAllAsync();
                foreach (var t in siteTypes)
                {
                    await api.SiteTypes.DeleteAsync(t);
                }
            }
        }

        [Fact]
        public async Task AddSimplePageType()
        {
            using (var api = CreateApi())
            {
                var builder = new PageTypeBuilder(api)
                    .AddType(typeof(SimplePageType));
                builder.Build();

                var type = await api.PageTypes.GetByIdAsync("Simple");

                Assert.NotNull(type);
                Assert.Equal(1, type.Regions.Count);
                Assert.Equal("Body", type.Regions[0].Id);
                Assert.Equal(1, type.Regions[0].Fields.Count);
            }
        }

        [Fact]
        public async Task AddComplexPageType()
        {
            using (var api = CreateApi())
            {
                var builder = new PageTypeBuilder(api)
                    .AddType(typeof(ComplexPageType));
                builder.Build();

                var type = await api.PageTypes.GetByIdAsync("Complex");

                Assert.NotNull(type);
                Assert.Equal(2, type.Regions.Count);

                Assert.Equal("Slider", type.Regions[0].Id);
                Assert.Equal("Intro", type.Regions[0].Title);
                Assert.True(type.Regions[0].Collection);
                Assert.Equal(1, type.Regions[0].Fields.Count);

                Assert.Equal("Content", type.Regions[1].Id);
                Assert.Equal("Main content", type.Regions[1].Title);
                Assert.False(type.Regions[1].Collection);
                Assert.Equal(2, type.Regions[1].Fields.Count);

                Assert.Equal(1, type.Routes.Count);
                Assert.Equal("/complex", type.Routes[0]);

                Assert.Equal(1, type.CustomEditors.Count);
                Assert.Equal("Custom Editor", type.CustomEditors[0].Title);
                Assert.Equal("custom-editor", type.CustomEditors[0].Component);
                Assert.Equal("fas fa-fish", type.CustomEditors[0].Icon);
            }
        }

        [Fact]
        public async Task AddSimplePostType()
        {
            using (var api = CreateApi())
            {
                var builder = new PostTypeBuilder(api)
                    .AddType(typeof(SimplePostType));
                builder.Build();

                var type = await api.PostTypes.GetByIdAsync("Simple");

                Assert.NotNull(type);
                Assert.Equal(1, type.Regions.Count);
                Assert.Equal("Body", type.Regions[0].Id);
                Assert.Equal(1, type.Regions[0].Fields.Count);
            }
        }

        [Fact]
        public async Task AddComplexPostType()
        {
            using (var api = CreateApi())
            {
                var builder = new PostTypeBuilder(api)
                    .AddType(typeof(ComplexPostType));
                builder.Build();

                var type = await api.PostTypes.GetByIdAsync("Complex");

                Assert.NotNull(type);
                Assert.Equal(2, type.Regions.Count);

                Assert.Equal("Slider", type.Regions[0].Id);
                Assert.Equal("Intro", type.Regions[0].Title);
                Assert.True(type.Regions[0].Collection);
                Assert.Equal(1, type.Regions[0].Fields.Count);

                Assert.Equal("Content", type.Regions[1].Id);
                Assert.Equal("Main content", type.Regions[1].Title);
                Assert.False(type.Regions[1].Collection);
                Assert.Equal(2, type.Regions[1].Fields.Count);

                Assert.Equal(1, type.Routes.Count);
                Assert.Equal("/complex", type.Routes[0]);

                Assert.Equal(1, type.CustomEditors.Count);
                Assert.Equal("Custom Editor", type.CustomEditors[0].Title);
                Assert.Equal("custom-editor", type.CustomEditors[0].Component);
                Assert.Equal("fas fa-fish", type.CustomEditors[0].Icon);
            }
        }

        [Fact]
        public async Task DeleteOrphans()
        {
            using (var api = CreateApi())
            {
                var builder = new PageTypeBuilder(api)
                    .AddType(typeof(SimplePageType))
                    .AddType(typeof(ComplexPageType));
                builder.Build();

                Assert.Equal(2, (await api.PageTypes.GetAllAsync()).Count());

                builder = new PageTypeBuilder(api)
                    .AddType(typeof(SimplePageType));
                builder.DeleteOrphans();

                Assert.Single(await api.PageTypes.GetAllAsync());
            }
        }

        [Fact]
        public async Task AddSimpleSiteType()
        {
            using (var api = CreateApi())
            {
                var builder = new SiteTypeBuilder(api)
                    .AddType(typeof(SimpleSiteType));
                builder.Build();

                var type = await api.SiteTypes.GetByIdAsync("Simple");

                Assert.NotNull(type);
                Assert.Equal(1, type.Regions.Count);
                Assert.Equal("Body", type.Regions[0].Id);
                Assert.Equal(1, type.Regions[0].Fields.Count);
            }
        }

        [Fact]
        public async Task AddComplexSiteType()
        {
            using (var api = CreateApi())
            {
                var builder = new SiteTypeBuilder(api)
                    .AddType(typeof(ComplexSiteType));
                builder.Build();

                var type = await api.SiteTypes.GetByIdAsync("Complex");

                Assert.NotNull(type);
                Assert.Equal(2, type.Regions.Count);

                Assert.Equal("Slider", type.Regions[0].Id);
                Assert.Equal("Intro", type.Regions[0].Title);
                Assert.True(type.Regions[0].Collection);
                Assert.Equal(1, type.Regions[0].Fields.Count);

                Assert.Equal("Content", type.Regions[1].Id);
                Assert.Equal("Main content", type.Regions[1].Title);
                Assert.False(type.Regions[1].Collection);
                Assert.Equal(2, type.Regions[1].Fields.Count);
            }
        }
    }
}
