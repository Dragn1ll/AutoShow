using System.Text.Json;
using System;

namespace AutoShow;

[Serializable]
public class Car : IProduct
{
    public Brand Brand { get; }
    public string Model { get; }
    public BodyType BodyType { get; }
    public decimal Price { get; }
    public bool IsSold { get; private set; }

    public Car(Brand brand, string model, BodyType bodyType, decimal price)
    {
        Brand = brand;
        Model = model;
        BodyType = bodyType;
        Price = price;
        IsSold = false;
    }

    public bool Sell()
    {
        if (!IsSold)
        {
            IsSold = true;
            return true;
        }

        return false;
    }
}