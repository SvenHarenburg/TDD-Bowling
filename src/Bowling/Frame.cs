namespace Bowling;

public class Frame
{
    public int[] PinsRolled = Array.Empty<int>();
    public int Score { get; private set; }
    public bool IsStrike => PinsRolled.Length == 1 && PinsRolled[0] == 10;
    public bool IsSpare => PinsRolled.Length == 2 && PinsRolled.Sum() == 10;
    public bool IsComplete
    {
        get
        {
            // TODO: Refactor
            if (IsLastFrame)
            {
                switch (PinsRolled.Length)
                {
                    case 1:
                        return false;
                    case 2:
                        return !(IsSpare || (PinsRolled[0] == 10 && PinsRolled[1] == 10));
                    case 3:
                        return true;
                }
            }
            return IsStrike || PinsRolled.Length == 2;

        }
    }
    public bool IsLastFrame { get; set; }

    public void AddRoll(int pins)
    {
        PinsRolled = PinsRolled.Append(pins);
        Score += pins;
    }

    public void AddBonus(int pins)
    {
        Score += pins;
    }
}