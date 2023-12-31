﻿using Microsoft.Extensions.DependencyInjection;
using Udemy_Umbraco_course.Mappings;
using Udemy_Umbraco_course.Routing;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using UmbracoTutorial.Core.Repository;
using UmbracoTutorial.Core.Services;


namespace UmbracoTutorial.Composers
{
    public class ProductComposer : IComposer
    {
        //example.com/product/1
        public void Compose(IUmbracoBuilder builder)
        {
            builder.UrlSegmentProviders().Insert<ProductPageUrlSegmentProvider>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.Configure<UmbracoPipelineOptions>(options =>
            {
                options.AddFilter(new UmbracoPipelineFilter(
                "Product integration",
                ApplicationBuilder => { },
                ApplicationBuilder => { },
                ApplicationBuilder => {
                    ApplicationBuilder.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllerRoute(
                                                   "product custom route",
                                                   "product/{id}",
                                                   new
                                                   {
                                                       controller = "Product",
                                                       action = "Details"
                                                   }
												   );
					});
				}
				));

            });
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
				.Add<ProductMapping>();
        }
    }
}

