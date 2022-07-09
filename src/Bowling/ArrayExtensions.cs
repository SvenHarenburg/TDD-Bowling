namespace Bowling;

public static class ArrayExtensions
{
    public static T[] Append<T>(this T[] array, T newItem)
    {
        var newArray = new T[array.Length + 1];
        array.CopyTo(newArray, 0);
        newArray[^1] = newItem;
        return newArray;
    }
}