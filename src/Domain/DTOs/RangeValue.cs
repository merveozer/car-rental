namespace Domain.DTOs
{
    public sealed class RangeValue<T> //sealed olduğu için classın kalıtımını başka hiçbir class alamaz
    {
        public T Start { get; set; }
        public T End { get; set; }

    }
}