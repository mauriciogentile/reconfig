namespace Reconfig.Domain.Model
{
    public class Id<T>
    {
        public Id(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public class StringId : Id<string>
    {
        public StringId(string value)
            : base(value)
        {
        }
    }
}
