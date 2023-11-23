using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using UmbracoTutorial.Core;


namespace UmbracoTutorial.Composers
{
    public class ProductComposer : IComposer
    {
        //example.com/product/1
        public void Compose(IUmbracoBuilder builder)
        {
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
        }
    }
}

