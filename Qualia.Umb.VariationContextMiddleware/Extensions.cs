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
    }

}
