namespace OperatingSystemAlgorithms;
public class Producer
{
    public void Produce()
    {

    }
}

public class Consumer
{
    public void Consume()
    {

    }
}


public class Buffer<T>
{
    private readonly T[] _buffer;
    public int Capacity { get; }

    public Buffer(int capacity)
    {
        Capacity = capacity;
        _buffer = new T[capacity];
        Size = 0;
    }

    public int Size { get; private set; }

    public void Add(byte @byte)
    {

    }

}