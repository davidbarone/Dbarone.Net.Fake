public interface IFakerStrategy
{

    IRandom Random { get; set; }

    object Next(int i, object? last = null);
}