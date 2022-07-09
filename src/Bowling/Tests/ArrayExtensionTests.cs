using NUnit.Framework;

namespace Bowling.Tests;

public class ArrayExtensionTests
{
    [Test]
    public void Append_resizes_empty_array_to_1_when_adding_item()
    {
        var array = Array.Empty<int>();

        var newArray = array.Append(1);

        Assert.That(newArray.Length, Is.EqualTo(1));
    }

    [Test]
    public void Append_adds_new_item_to_empty_array()
    {
        var array = Array.Empty<int>();

        var newArray = array.Append(1);

        Assert.That(newArray[0], Is.EqualTo(1));
    }
}

