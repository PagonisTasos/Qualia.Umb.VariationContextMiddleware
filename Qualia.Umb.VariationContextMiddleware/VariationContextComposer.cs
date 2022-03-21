using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace Qualia.Umb.VariationContextMiddleware
{
    public class VariationContextComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.Configure<UmbracoPipelineOptions>(options =>
            {
                options.AddFilter(new UmbracoPipelineFilter(
                 name: nameof(VariationContextMiddleware),
                 prePipeline: applicationBuilder => { },
                 postPipeline: applicationBuilder => { applicationBuilder.UseMiddleware<VariationContextMiddleware>(); },
                 endpointCallback: applicationBuilder => { }));
            });
        }
    }

}
