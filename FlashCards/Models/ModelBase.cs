namespace FlashCards.Models
{
    public abstract class ModelBase<T>
    {
        public abstract void ShallowCopy(T objToCopy);
    }
}