using System.Collections;

namespace AutoShow;

public class CarIterator : IEnumerator<Car>
{
    private CarDealer _aggregator;
    private int _current;

    public delegate void AllCarsSoldDelegate(string message);
    public event AllCarsSoldDelegate? AllCarsSold;

    public CarIterator(CarDealer aggregate)
    {
        _aggregator = aggregate;
    }

    public void Reset()
    {
        _current = 0;
    }

    object IEnumerator.Current => Current;

    public Car Current
    {
        get
        {
            return _aggregator[_current];
        }
    }

    public bool IsDone
    {
        get
        {
            return (_current >= _aggregator.Count);
        }
    }
    public bool MoveNext()
    {
        _current++;
        while (Current.IsSold)
        {
            if (IsDone)
            {
                AllCarsSold?.Invoke("Всё продано!");
                return false;
            }

            _current++;
        }

        return true;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool All(Func<Car, bool> predicate) 
    {
        Reset();
        while (!IsDone)
        {
            if (!predicate(Current))
                return false;
            MoveNext();
        }

        return false;
    }

    public bool Any(Func<Car, bool> predicate)
    {
        Reset();
        while (!IsDone)
        {
            if (predicate(Current))
                return true;
            MoveNext();
        }

        return false;
    }

    public IEnumerable<Car> Take(int count, Func<Car, bool> predicate)
    {
        if (count > _aggregator.Count)
            throw new ArgumentException("Недостаточно элементов в коллекции!");

        int startIndex = _current;
        MoveNext();
        while (count > 0 && _current != startIndex)
        {
            if (IsDone) Reset();

            if (predicate(Current))
            {
                yield return Current;
                count--;
            }

            MoveNext();
        }

        if (predicate(Current) && count > 0)
        {
            yield return Current;
            MoveNext();
        }
    }

    public void Skip(int count)
    {
        var b = Take(count, p => true);
    }

    public Dictionary<TKey, List<Car>> GroupBy<TKey>(Func<Car, TKey> keySelector)
    {
        Dictionary<TKey, List<Car>> groups = new Dictionary<TKey, List<Car>>();

        Reset();

        while (!IsDone)
        {
            Car currentCar = Current;
            TKey key = keySelector(currentCar);

            if (!groups.ContainsKey(key))
            {
                groups[key] = new List<Car>();
            }

            groups[key].Add(currentCar);
            MoveNext();
        }

        return groups;
    }
}