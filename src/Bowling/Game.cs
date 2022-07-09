namespace Bowling;

public class Game
{
    private const int BonusRollsForStrike = 2;
    private const int BonusRollsForSpare = 1;
    private const int MaximumNumberOfFrames = 10;

    public Frame[] Frames { get; private set; } = Array.Empty<Frame>();
    public int TotalScore => Frames.Sum(frame => frame.Score);
    public bool Over => Frames.Length == 10 && Frames.Last().IsComplete;

    private readonly List<FrameWithBonusRolls> framesWithBonusRolls = new();

    public void AddRoll(int pins)
    {
        if (Over) throw new GameOverException();
        var currentFrame = GetCurrentFrame();
        currentFrame.AddRoll(pins);

        HandleBonusesForCurrentRoll(currentFrame, pins);
    }

    private Frame GetCurrentFrame()
    {
        var lastFrame = Frames.LastOrDefault();
        if (lastFrame == null || lastFrame.IsComplete)
        {
            return StartNewFrame();
        }

        return lastFrame;
    }

    private Frame StartNewFrame()
    {
        var newFrame = new Frame()
        {
            IsLastFrame = NextFrameWillBeLastFrame()
        };
        Frames = Frames.Append(newFrame);
        return newFrame;
    }

    private bool NextFrameWillBeLastFrame()
    {
        return Frames.Length + 1 == MaximumNumberOfFrames;
    }

    private void HandleBonusesForCurrentRoll(Frame currentFrame, int pins)
    {
        AddBonusToPreviousFrames(pins);
        RemoveFramesWithNoMoreBonusRollsFromBonusList();
        AddFrameToBonusListIfQualified(currentFrame);
    }

    private void AddBonusToPreviousFrames(int pins)
    {
        foreach (var frame in framesWithBonusRolls)
        {
            frame.Frame.AddBonus(pins);
            frame.BonusRolls--;
        }
    }

    private void RemoveFramesWithNoMoreBonusRollsFromBonusList()
    {
        framesWithBonusRolls
            .Where(frame => frame.BonusRolls <= 0)
            .ToList()
            .ForEach(frame => framesWithBonusRolls.Remove(frame));
    }

    private void AddFrameToBonusListIfQualified(Frame frame)
    {
        if (frame.IsLastFrame) return;
        if (frame.IsStrike)
        {
            framesWithBonusRolls.Add(
                new FrameWithBonusRolls(frame, BonusRollsForStrike));

        }
        else if (frame.IsSpare)
        {
            framesWithBonusRolls.Add(
                new FrameWithBonusRolls(frame, BonusRollsForSpare));
        }
    }
}