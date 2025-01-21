namespace Blog.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) =>
        obj is not null
        && obj.GetType() == GetType()
        && GetEqualityComponents().SequenceEqual(((ValueObject)obj).GetEqualityComponents());

    public bool Equals(ValueObject? other) => other is not null && Equals((object)other);

    public static bool operator ==(ValueObject? left, ValueObject? right) =>
        !(left is null ^ right is null) && (left is null || left.Equals(right));

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    public override int GetHashCode() =>
        GetEqualityComponents().Aggregate(default(int), HashCode.Combine);
}
