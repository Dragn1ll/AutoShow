namespace AutoShow;

public interface IAccessory
{
    string Name { get; }
    Car Car { get; }
    decimal Price { get; }
    bool Give();
}