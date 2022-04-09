using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace Qualia.Umb.VariationContextMiddleware
{

    internal class VariationContextMiddleware
    {
        private readonly RequestDelegate _next;

        public VariationContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IVariationContextAccessor variationContextAccessor, ILocalizationService localizationService)
        {
            if (context.Request.IsAjaxRequest())
            {
                var localeStr =
                    GetLocale_fromQuery(context).NullIfEmpty()
                    ?? GetLocale_fromCookie(context).NullIfEmpty()
                    ?? GetLocale_fromHeaders(context).NullIfEmpty()
                    ;
            
                if (!LocaleExistsInCms(localizationService, localeStr))
                {
                    localeStr = localizationService.GetDefaultLanguageIsoCode();
                }

                if (!string.IsNullOrWhiteSpace(localeStr))
                {
                    var culture = new CultureInfo(localeStr);

                    //for dictionary
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                    //for content
                    variationContextAccessor.VariationContext = new VariationContext(localeStr);
                }
            }

            await _next(context);
        }

        private string? GetLocale_fromQuery(HttpContext context)
            => context.Request.Query["culture"].ToString();
        private string? GetLocale_fromCookie(HttpContext context)
            => context.Request.Cookies["culture"];
        private string? GetLocale_fromHeaders(HttpContext context)
            => context.Request.Headers["culture"].ToString();
        private bool LocaleExistsInCms(ILocalizationService localizationService, string? locale)
            => localizationService.GetAllLanguages().Any(x => x.CultureInfo.Name.Equals(locale, StringComparison.OrdinalIgnoreCase));

    }

}
