namespace OperatingSystemAlgorithms.DiskScheduling;

public interface IDiskSchedulingPolicy
{
    public DiskResult Serve(
        Disk disk,
        LinkedList<DiskRequest> requests);
}

public abstract class DiskContext
{
    public DiskContext(Disk disk,
        RequestArrivalSequence requests,
        IDiskSchedulingPolicy policy)
    {
        Requests = requests;
        Disk = disk;
        ServedRequests = new HashSet<DiskRequest>(Requests.Count);
        PendingRequests = new LinkedList<DiskRequest>(Requests);
        SchedulingPolicy = policy;
    }

    public RequestArrivalSequence Requests { get; }

    public IDiskSchedulingPolicy SchedulingPolicy { get; }

    public Disk Disk { get; }

    public bool Completed => ServedRequests.Count == Requests.Count;

    protected ISet<DiskRequest> ServedRequests { get; }

    protected LinkedList<DiskRequest> PendingRequests { get; }

    public DiskResult ServeNext()
    {
        SchedulingPolicy.Serve(Disk, PendingRequests);
    }
}

public class FirstComeFirstServedPolicy : IDiskSchedulingPolicy
{
    public DiskResult Serve(
        Disk disk,
        LinkedList<DiskRequest> requests)
    {
        DiskRequest request = requests.First?.Value
            ?? throw new ArgumentException("No disk requests to select", nameof(requests));

        int previous = disk.Position;
        disk.Seek(request.Number);

        return new DiskResult(request, previous, disk.Position);
    }
}

public class LookPolicy : IDiskSchedulingPolicy
{
    public DiskResult Serve(Disk disk, LinkedList<DiskRequest> requests)
    {
        requests.Ord
    }
}

public class ShortestSeekTimeFirstPolicy : IDiskSchedulingPolicy
{
    public DiskResult Serve(Disk disk, LinkedList<DiskRequest> requests)
    {
        if (requests is null or { Count: 0 })
        {
            throw new ArgumentException("No disk requests to select", nameof(requests));
        }

        var request = requests.MinBy(dr => Math.Abs(dr.Number - disk.Position));
        int previous = disk.Position;
        disk.Seek(request.Number);
        return new DiskResult(request, previous, disk.Position);
    }
}

public enum DiskMovementDirection { Left, Right };

public class ScanPolicy : IDiskSchedulingPolicy
{
    public ScanPolicy(DiskMovementDirection direction)
    {
        Direction = direction;
    }

    public DiskMovementDirection Direction { get; private set; }


    public virtual DiskResult Serve(Disk disk, LinkedList<DiskRequest> requests)
    {
        int initial = disk.Position;

        if (requests is null or { Count: 0 })
        {
            throw new InvalidOperationException("No requests to select");
        }

        var sortedRequests = requests.OrderBy(req => req);

        if (Direction is DiskMovementDirection.Left)
        {
            return ServeLeft(disk, sortedRequests, initial);
        }
        // Current direction is right
        return ServeRight(disk, sortedRequests, initial);
    }

    protected virtual DiskResult ServeLeft(Disk disk, IOrderedEnumerable<DiskRequest> sortedRequests, int initialPosition)
    {
        int position = disk.Position;

        var requestsInPath = sortedRequests
            .TakeWhile(r => r.Number >= disk.Position)
            .ToArray();

        if (requestsInPath.Length > 0)
        {
            // There exists at least one request on the path until the disk moves to the start
            DiskRequest request = requestsInPath[0];
            int previous = disk.Position;
            disk.Seek(request.Number);
            return new DiskResult(request, initialPosition, disk.Position);
        }

        // No more requests on the path, move to the start
        SeekToEnd(disk);
        
        return ServeRight(disk, sortedRequests, initialPosition);
    }

    protected virtual DiskResult ServeRight(Disk disk, IOrderedEnumerable<DiskRequest> sortedRequests, int initialPosition)
    {
        int position = disk.Position;

        var requestsInPath = sortedRequests
            .SkipWhile(r => r.Number < position)
            .ToArray();

        if (requestsInPath.Length > 0)
        {
            // There exists at least one request on the path until the disk moves to the end
            DiskRequest request = requestsInPath[0];
            disk.Seek(request.Number);
            return new DiskResult(request, initialPosition, disk.Position);
        }
        // No more requests on the path, move to the end
        SeekToEnd(disk);

        return ServeLeft(disk, sortedRequests, initialPosition);
    }

    protected virtual void SeekToEnd(Disk disk)
    {
        if (Direction is DiskMovementDirection.Left)
        {
            disk.Seek(0);
            Direction = DiskMovementDirection.Right;
        }
        else
        {
            disk.Seek(disk.End);
            Direction = DiskMovementDirection.Left;
        }
    }
}

public class CircularScanPolicy : ScanPolicy
{
    public CircularScanPolicy() 
        : base(DiskMovementDirection.Right)
    {
    }

    protected override void SeekToEnd(Disk disk)
    {
        disk.Seek(disk.End);
        disk.Seek(0);
    }
}

public record struct DiskResult(
        DiskRequest? ServedRequest,
        int Start,
        int End);

public record struct DiskRequest(int Number);

