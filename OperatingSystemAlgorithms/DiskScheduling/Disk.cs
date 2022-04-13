namespace OperatingSystemAlgorithms.DiskScheduling;

public class Disk
{
    public int Tracks { get; }

    public int Position { get; private set; }

    public int End => Tracks - 1;

    public event EventHandler<DiskSeekEventArgs>? Seeked;

    public void Seek(int position)
    {
        int previous = Position;
        if (position < 0)
        {
            Position = 0;
        }
        else if (position > Tracks)
        {
            Position = Tracks - 1;
        }
        else
        {
            Position = position;
        }
        OnSeek(new DiskSeekEventArgs(previous, Position));
    }
    private void OnSeek(DiskSeekEventArgs args) => Seeked?.Invoke(this, args);

    public class DiskSeekEventArgs : EventArgs
    {
        public DiskSeekEventArgs(int previous, int current)
        {
            Previous = previous;
            Current = current;
        }

        public int Previous { get; }

        public int Current { get; }
    }
}

