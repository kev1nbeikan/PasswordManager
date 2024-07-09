using NUnit.Framework;
using PasswordsSaver.Core;

namespace PasswordSaver.Tests.Extensions;

public static class AssertExtensions
{
    public static void AssertIsEqual(this SavedPassword actual, SavedPassword expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Source, Is.EqualTo(expected.Source));
            Assert.That(actual.SourceType, Is.EqualTo(expected.SourceType));
            Assert.That(actual.Password, Is.EqualTo(expected.Password));
        });
    }
}