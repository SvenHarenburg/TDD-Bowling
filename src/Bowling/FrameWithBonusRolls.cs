namespace Bowling;

public class FrameWithBonusRolls
{
    public Frame Frame { get; }
    public int BonusRolls { get; set; }

    public FrameWithBonusRolls(Frame frame, int bonusRolls)
    {
        Frame = frame;
        BonusRolls = bonusRolls;
    }
}