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
}