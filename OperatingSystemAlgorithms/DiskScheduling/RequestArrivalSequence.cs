
using System.Collections;

namespace OperatingSystemAlgorithms.DiskScheduling;

public class RequestArrivalSequence : IReadOnlyList<DiskRequest>
{
    private IList<DiskRequest> _requests;

    public RequestArrivalSequence(IEnumerable<DiskRequest> requests)
    {
        _requests = new List<DiskRequest>(requests);
    }

    public DiskRequest this[int index] => _requests[index];

    public int Count => _requests.Count;

    public IEnumerator<DiskRequest> GetEnumerator() => _requests.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

