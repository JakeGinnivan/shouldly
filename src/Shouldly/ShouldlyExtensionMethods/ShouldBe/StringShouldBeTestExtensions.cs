using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Perform a case sensitive string comparison
        /// </summary>
        public static void ShouldBe(this string actual, string expected)
        {
            ShouldBe(actual, expected, () => null);
        }

        /// <summary>
        /// Perform a case sensitive string comparison
        /// </summary>
        public static void ShouldBe(this string actual, string expected, string customMessage)
        {
            ShouldBe(actual, expected, () => customMessage);
        }

        /// <summary>
        /// Perform a case sensitive string comparison
        /// </summary>
        public static void ShouldBe(this string actual, string expected, [InstantHandle] Func<string> customMessage)
        {
            ShouldBe(actual, expected, Case.Sensitive, customMessage);
        }

        /// <summary>
        /// Perform a string comparison, specifying the desired case sensitivity
        /// </summary>
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity)
        {
            ShouldBe(actual, expected, caseSensitivity, () => null);
        }

        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, string customMessage)
        {
            ShouldBe(actual, expected, caseSensitivity, () => customMessage);
        }

        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, [InstantHandle] Func<string> customMessage)
        {
            actual.AssertAwesomelyWithCaseSensitivity(
                v => Is.StringEqualWithCaseSensitivity(v, expected, caseSensitivity), 
                actual, expected, caseSensitivity, customMessage);
        }
    }
}