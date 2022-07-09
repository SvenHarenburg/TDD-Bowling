using NUnit.Framework;

namespace Bowling.Tests;

public class FrameTests
{
    [Test]
    public void AddRoll_adds_first_roll_to_PinsRolled()
    {
        var frame = new Frame();

        frame.AddRoll(1);

        Assert.That(frame.PinsRolled[0], Is.EqualTo(1));
    }

    [Test]
    public void AddRoll_adds_second_roll_to_PinsRolled()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(2);

        Assert.That(frame.PinsRolled[1], Is.EqualTo(2));
    }

    [Test]
    public void Score_is_1_after_adding_roll_of_1()
    {
        var frame = new Frame();

        frame.AddRoll(1);

        Assert.That(frame.Score, Is.EqualTo(1));
    }

    [Test]
    public void Score_is_5_after_adding_roll_of_1_and_roll_of_4()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(4);

        Assert.That(frame.Score, Is.EqualTo(5));
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public void AddBonusToScore_Adds_bonus_to_Score(int bonus)
    {
        var frame = new Frame();

        frame.AddBonus(bonus);

        Assert.That(frame.Score, Is.EqualTo(bonus));
    }

    [Test]
    public void Score_SumsUp_PinsRolled_and_Bonus()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(2);
        frame.AddBonus(3);

        Assert.That(frame.Score, Is.EqualTo(6));
    }

    [Test]
    public void IsSpare_does_not_throw_on_empty_Frame()
    {
        var frame = new Frame();

        Assert.DoesNotThrow(() => { var _ = frame.IsSpare; });
    }

    [Test]
    public void IsSpare_is_true_after_rolling_Spare()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(9);

        Assert.That(frame.IsSpare, Is.True);
    }

    [Test]
    public void IsSpare_is_false_after_not_rolling_spare()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(2);

        Assert.That(frame.IsSpare, Is.False);
    }

    [Test]
    public void IsSpare_is_false_after_rolling_Strike()
    {
        var frame = new Frame();

        frame.AddRoll(10);

        Assert.That(frame.IsSpare, Is.False);
    }


    [Test]
    public void IsStrike_does_not_throw_on_empty_Frame()
    {
        var frame = new Frame();

        Assert.DoesNotThrow(() => { var _ = frame.IsStrike; });
    }

    [Test]
    public void IsStrike_is_true_after_rolling_strike()
    {
        var frame = new Frame();

        frame.AddRoll(10);

        Assert.That(frame.IsStrike, Is.True);
    }

    [Test]
    public void IsStrike_is_false_after_not_rolling_Strike()
    {
        var frame = new Frame();

        frame.AddRoll(1);

        Assert.That(frame.IsStrike, Is.False);
    }

    [Test]
    public void IsStrike_is_false_after_not_rolling_Spare()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(9);

        Assert.That(frame.IsStrike, Is.False);
    }

    [Test]
    public void IsComplete_is_true_after_rolling_twice()
    {
        var frame = new Frame();

        frame.AddRoll(1);
        frame.AddRoll(2);

        Assert.That(frame.IsComplete, Is.True);
    }

    [Test]
    public void IsComplete_is_true_after_rolling_Strike()
    {
        var frame = new Frame();

        frame.AddRoll(10);

        Assert.That(frame.IsComplete, Is.True);
    }

    [Test]
    public void IsComplete_is_false_after_one_roll_with_less_than_10_pins()
    {
        var frame = new Frame();

        frame.AddRoll(9);

        Assert.That(frame.IsComplete, Is.False);
    }

    [Test]
    public void IsComplete_is_false_if_Frame_is_last_Frame_and_first_two_Rolls_are_Spare()
    {
        var frame = new Frame()
        {
            IsLastFrame = true
        };

        frame.AddRoll(1);
        frame.AddRoll(9);

        Assert.That(frame.IsComplete, Is.False);
    }

    [Test]
    public void IsComplete_is_false_if_Frame_is_last_Frame_and_first_Roll_is_Strike()
    {
        var frame = new Frame()
        {
            IsLastFrame = true
        };

        frame.AddRoll(10);

        Assert.That(frame.IsComplete, Is.False);
    }

    [Test]
    public void IsComplete_is_false_if_Frame_is_last_Frame_and_first_two_Rolls_are_Strike()
    {
        var frame = new Frame()
        {
            IsLastFrame = true
        };

        frame.AddRoll(10);
        frame.AddRoll(10);

        Assert.That(frame.IsComplete, Is.False);
    }
}

