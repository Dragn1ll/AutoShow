namespace AutoShow;

public interface IProduct
{
    bool IsSold { get; }
    bool Sell();
}