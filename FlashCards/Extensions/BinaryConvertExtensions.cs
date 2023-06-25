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

    public static RecieveRepetitionDto[] ToRecieveRepetitionDtoArray(this Byte[] array)
    {
        int singleDtoSize = sizeof(long) + sizeof(long);
        int numberOfDtos = (array.Length + 1) / singleDtoSize;
        RecieveRepetitionDto[] dtoArray
            = new RecieveRepetitionDto[numberOfDtos];

        for (int i = 0; i < numberOfDtos; i++)
        {
            var singleDtoArray = array
                .Skip(singleDtoSize * i)
                .Take(singleDtoSize);
            dtoArray[i] = ToRecieveRepetitionDto(singleDtoArray.ToArray());
        }
        return dtoArray;
    }

    public static Byte[] ToByteArray(this long[] longArray)
    {
        byte[] byteArray = new byte[longArray.Length * sizeof(long)];
        int index = 0;
        foreach (var longValue in longArray)
        {
            var longBytes = BitConverter.GetBytes(longValue);
            foreach (var longByte in longBytes)
            {
                byteArray[index] = longByte;
                index++;
            }
        }

        return byteArray;
    }
}