using SpacedRep.Models.Remote;

namespace SpacedRep.Extensions;

public static class BinaryConvertExtensions
{
    public static Byte[] ToByteArray(this SendRepetitionDto repDto)
    {
        var idBytes = BitConverter.GetBytes(repDto.Id);
        long dtLongValue;
        if (repDto.BlockedUntil == null)
            dtLongValue = 0;
        else
            dtLongValue = ((DateTime)repDto.BlockedUntil).Ticks;
        var dtBytes = BitConverter.GetBytes(dtLongValue);

        var array = new Byte[idBytes.Length + dtBytes.Length];
        for (int i = 0; i < array.Length; i++)
        {
            if (i < sizeof(long))
                array[i] = idBytes[i];
            else
                array[i] = dtBytes[i - sizeof(long)];
        }

        return array;
    }

    public static long[] ToLongArray(this Byte[] array)
    {
        int longAmount = array.Length / sizeof(long);
        long[] longArray = new long[longAmount];
        for (int i = 0; i < longArray.Length; i++)
        {
            longArray[i] = BitConverter.ToInt64(array, i * sizeof(long));
        }
        return longArray;
    }
}