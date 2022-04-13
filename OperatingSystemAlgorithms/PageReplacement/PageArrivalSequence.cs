using System.Collections;
namespace OperatingSystemAlgorithms.PageReplacement;

public class PageArrivalSequence : IReadOnlyList<Page>
{
    private IList<Page> _pages;

    public PageArrivalSequence(IEnumerable<Page> pages)
    {
        _pages = new List<Page>(pages);
    }

    public Page this[int index] => _pages[index];

    public int Count => _pages.Count;

    public IEnumerator<Page> GetEnumerator() => _pages.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

