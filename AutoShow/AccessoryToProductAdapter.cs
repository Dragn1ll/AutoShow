namespace AutoShow;

public class AccessoryToProductAdapter : IProduct
{
    public bool IsSold { get; }
    private IAccessory _accessory;

    public AccessoryToProductAdapter(IAccessory accessory)
    {
        _accessory = accessory;
    }

    public bool Sell()
    {
        return _accessory.Give();
    }
}