using FlashCards.Models.Dtos.Remote;

namespace FlashCards.Extensions;

public static class BinaryConvertExtensions
{
    public static RecieveRepetitionDto ToRecieveRepetitionDto(this Byte[] array)
    {
        var id = BitConverter.ToInt64(array);
        var dtTicks = BitConverter.ToInt64(array, sizeof(long));
        return new RecieveRepetitionDto()
        {
            Id = id,
            BlockedUntil = dtTicks != 0 ? DateTime.FromBinary(dtTicks) : null
        };
    }
}