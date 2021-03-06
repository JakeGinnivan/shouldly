using System;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Shouldly;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace ShouldlyConvention.Tests
{
    [TestFixture]
    public class ShouldlyConventions
    {
        private readonly Types _shouldlyMethodClasses;

        public ShouldlyConventions()
        {
            _shouldlyMethodClasses = Types.InAssemblyOf<ShouldAssertException>(
                "Shouldly extension classes",
                t => t.HasAttribute("Shouldly.ShouldlyMethodsAttribute"));
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void ShouldHaveCustomMessageOverloads()
        {
            Convention.IsWithApprovedExeptions(new ShouldlyMethodsShouldHaveCustomMessageOverload(), _shouldlyMethodClasses);
        }

        [Test]
        public void VerifyItWorks()
        {
            var ex = Should.Throw<ConventionFailedException>(() =>
            {
                var convention = new ShouldlyMethodsShouldHaveCustomMessageOverload();
                var types = Types.InCollection(new[] { typeof(TestWithMissingOverloads) }, "Sample");
                Convention.Is(convention, types);
            });

            ex.Message.ShouldContain("ShouldAlsoFail");
        }

        [Test]
        public void ShouldThrowMethodsShouldHaveExtensions()
        {
            Convention.Is(new ShouldThrowMatchesExtensionsConvention(), _shouldlyMethodClasses);
        }
    }

    public static class TestWithMissingOverloads
    {
        public static void ShouldTest(this object foo) { }

        public static void ShouldAlsoFail(this object foo) { }
        public static void ShouldAlsoFail(this object foo, string customMessage) { }
        public static void ShouldAlsoFail(this object foo, Func<string> customMessage) { }
        public static void ShouldAlsoFail(this object foo, int param) { }
        public static void ShouldAlsoFail(this object foo, int param, Func<string> customMessage) { }
    }
}