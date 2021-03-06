using System.Runtime.Serialization;

namespace Bowling;

[Serializable]
public class GameOverException : Exception
{
    public GameOverException()
    {
    }

    public GameOverException(string message) : base(message)
    {
    }

    public GameOverException(string message, Exception inner) : base(message, inner)
    {
    }

    protected GameOverException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}