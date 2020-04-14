using System;

public static class Utils
{
    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        int length = source.Length;

        T[] dest = new T[length - 1];

        if (index > 0)
            Array.Copy(source, dest, index);

        if (index < length - 1)
            Array.Copy(source, index + 1, dest, index, length - index - 1);

        return dest;
    }

    public static T[] Add<T>(this T[] source, T newItem)
    {
        int length = source.Length;

        T[] dest = new T[length + 1];
        Array.Copy(source, dest, length);
        dest[length] = newItem;

        return dest;
    }
}
