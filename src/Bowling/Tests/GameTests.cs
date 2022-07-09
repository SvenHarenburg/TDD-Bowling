using NUnit.Framework;

namespace Bowling.Tests;

public class GameTests
{
    [Test]
    public void TotalScore_is_1_after_rolling_1()
    {
        var game = new Game();

        game.AddRoll(1);

        Assert.That(game.TotalScore, Is.EqualTo(1));
    }

    [Test]
    public void Over_is_false_after_starting_Game_and_rolling_1()
    {
        var game = new Game();

        game.AddRoll(1);

        Assert.That(game.Over, Is.False);
    }

    [Test]
    public void TotalScore_is_5_after_rolling_1_and_4()
    {
        var game = new Game();

        game.AddRoll(1);
        game.AddRoll(4);

        Assert.That(game.TotalScore, Is.EqualTo(5));
    }


    [Test]
    public void Adds_second_Frame_when_first_already_has_two_rolls()
    {
        var game = new Game();

        // Frame1
        game.AddRoll(1);
        game.AddRoll(4);

        // Frame2
        game.AddRoll(4);

        Assert.That(game.Frames.Length, Is.EqualTo(2));
    }

    [Test]
    public void Sets_PinsRolled_on_both_Frames_when_two_Frames_are_completed()
    {
        var game = new Game();

        // Frame1
        game.AddRoll(1);
        game.AddRoll(4);

        // Frame2
        game.AddRoll(4);
        game.AddRoll(5);

        var firstFrame = game.Frames[0];
        Assert.That(firstFrame.PinsRolled[0], Is.EqualTo(1));
        Assert.That(firstFrame.PinsRolled[1], Is.EqualTo(4));

        var secondFrame = game.Frames[1];
        Assert.That(secondFrame.PinsRolled[0], Is.EqualTo(4));
        Assert.That(secondFrame.PinsRolled[1], Is.EqualTo(5));
    }

    [Test]
    public void Adds_next_Roll_as_Bonus_to_Spare_Frame()
    {
        var game = new Game();

        game.AddRoll(4);
        game.AddRoll(6);
        game.AddRoll(1);

        var spareFrame = game.Frames[0];
        Assert.That(spareFrame.Score, Is.EqualTo(11));
    }

    [Test]
    public void Stops_adding_bonus_to_Spare_Frame_after_one_roll()
    {
        var game = new Game();

        game.AddRoll(4);
        game.AddRoll(6);
        game.AddRoll(2);
        game.AddRoll(4);

        var firstFrame = game.Frames[0];
        Assert.That(firstFrame.Score, Is.EqualTo(12));
    }

    [Test]
    public void Ends_Frame_after_rolling_Strike()
    {
        var game = new Game();

        game.AddRoll(10);
        game.AddRoll(1);

        Assert.That(game.Frames.Length, Is.EqualTo(2));
    }

    [Test]
    public void Adds_next_two_Rolls_as_Bonus_to_Strike_Frame()
    {
        var game = new Game();

        game.AddRoll(10);
        game.AddRoll(1);
        game.AddRoll(2);

        var spareFrame = game.Frames[0];
        Assert.That(spareFrame.Score, Is.EqualTo(13));
    }
    
    [Test]
    public void Stops_adding_bonus_to_Strike_Frame_after_two_rolls()
    {
        var game = new Game();

        game.AddRoll(10);
        game.AddRoll(1);
        game.AddRoll(2);
        game.AddRoll(4);

        var firstFrame = game.Frames[0];
        Assert.That(firstFrame.Score, Is.EqualTo(13));
    }

    [Test]
    public void Does_not_add_3rd_Roll_of_10th_Frame_as_Bonus_to_10th_Frame_if_first_two_Rolls_are_Spare()
    {
        var game = new Game();
        // Roll 9 Frames:
        for (var i = 0; i < 9; i++)
        {
            game.AddRoll(1);
            game.AddRoll(1);
        }

        game.AddRoll(1);
        game.AddRoll(9);
        game.AddRoll(1);

        var tenthFrame = game.Frames[9];
        Assert.That(tenthFrame.Score, Is.EqualTo(11));
    }

    [Test]
    public void Does_not_add_2nd_or_3rd_Roll_of_10th_Frame_as_Bonus_to_10th_Frame_if_first_two_Rolls_are_Strike()
    {
        var game = new Game();
        // Roll 9 Frames:
        for (var i = 0; i < 9; i++)
        {
            game.AddRoll(1);
            game.AddRoll(1);
        }

        game.AddRoll(10);
        game.AddRoll(10);
        game.AddRoll(1);

        var tenthFrame = game.Frames[9];
        Assert.That(tenthFrame.Score, Is.EqualTo(21));
    }

    [Test]
    public void Can_roll_3_times_on_10th_Frame_if_first_two_rolls_are_Spare()
    {
        var game = new Game();
        // Roll 9 Frames:
        for (var i = 0; i < 9; i++)
        {
            game.AddRoll(1);
            game.AddRoll(1);
        }

        game.AddRoll(4);
        game.AddRoll(6);
        game.AddRoll(1);

        Assert.That(game.Frames.Length, Is.EqualTo(10));
        Assert.That(game.Frames.Last().PinsRolled.Length, Is.EqualTo(3));
    }

    [Test]
    public void Over_returns_true_after_10th_Frame_is_completed()
    {
        var game = new Game();
        // Roll 10 Frames:
        for (var i = 0; i < 10; i++)
        {
            game.AddRoll(1);
            game.AddRoll(1);
        }

        Assert.That(game.Over, Is.True);
    }

    [Test]
    public void Throws_exception_when_rolling_after_Game_is_Over()
    {
        var game = new Game();
        // Roll 10 Frames:
        for (var i = 0; i < 10; i++)
        {
            game.AddRoll(1);
            game.AddRoll(1);
        }

        Assert.Throws<GameOverException>(() => game.AddRoll(1));
    }
}