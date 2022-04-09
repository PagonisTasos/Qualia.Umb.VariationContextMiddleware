using Microsoft.AspNetCore.Http;
using System;

namespace Qualia.Umb.VariationContextMiddleware
{
    internal static partial class Extensions
    {
        /// <summary>
        ///     Returns null if the string is empty.
        /// </summary>
        /// <param name="this">The string to act upon.</param>
        /// <returns>Null if the string is empty, otherwise returns the string.</returns>
        public static string? NullIfEmpty(this string? @this)
        {
            return String.IsNullOrEmpty(@this) ? null : @this;
        }

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        internal static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers == null)
                return false;
            
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
