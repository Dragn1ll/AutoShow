using System.Collections;

namespace AutoShow;

public delegate bool CarFilterDelegate(Car car);

public class CarDealer : IEnumerable<Car>
{
    private readonly List<Car> _cars;

    public CarDealer(List<Car> cars) => _cars = cars;

    public Car this[int index] => _cars[index];

    public int Count => _cars.Count;

    public void AddCar(Car car) => _cars.Add(car);

    public IEnumerator<Car> GetEnumerator()
    {
        return new CarIterator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public IEnumerable<Car> FilterCars(CarFilterDelegate filter)
    {
        foreach (var car in _cars)
        {
            if (filter(car))
            {
                yield return car;
            }
        }
    }
}