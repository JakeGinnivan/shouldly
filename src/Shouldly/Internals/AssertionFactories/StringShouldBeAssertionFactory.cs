﻿using Shouldly.Internals.Assertions;
using System;
using System.Collections.Generic;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.AssertionFactories
{
    internal static class StringShouldBeAssertionFactory
    {
        public static IAssertion Create(string expected, string actual, StringCompareShould options)
        {
            List<string> optionsList = new List<string>();
            if ((options & StringCompareShould.IgnoreLineEndings) != 0)
            {
                expected = expected.NormalizeLineEndings();
                actual = actual.NormalizeLineEndings();
                optionsList.Add("Ignoring line endings");
            }

            Case sensitivity;
            Func<string, string, bool> stringComparer;
            if ((options & StringCompareShould.IgnoreCase) == 0)
            {
                sensitivity = Case.Sensitive;
                stringComparer = StringComparer.InvariantCulture.Equals;
                optionsList.Add("Ignoring case");
            }
            else
            {
                sensitivity = Case.Insensitive;
                stringComparer = StringComparer.InvariantCultureIgnoreCase.Equals;
            }

            return new StringShouldBeAssertion(
                        expected, actual,
                        stringComparer,
                        new ActualCodeTextGetter(),
                        new StringDifferenceHighlighter(sensitivity),
                        string.Join(", ", optionsList));
        }
    }
}
