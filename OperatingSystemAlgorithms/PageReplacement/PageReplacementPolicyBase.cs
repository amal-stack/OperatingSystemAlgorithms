namespace OperatingSystemAlgorithms.PageReplacement;

public abstract class PageReplacementPolicyBase : IPageReplacementPolicy
{
    public PageArrivalSequence Pages { get; }

    public int FrameCount { get; }

    public int Faults { get; protected set; }

    public int Hits { get; protected set; }

    protected Page?[] Memory { get; }

    protected int Current { get; set; }

    public PageReplacementPolicyBase(
        PageArrivalSequence pages,
        int frameCount)
    {
        Pages = pages;
        FrameCount = frameCount;
        Memory = new Page?[frameCount];
    }

    public IEnumerable<Page?> MemoryState => Array.AsReadOnly(Memory);

    public virtual PageReplacementResult Replace()
    {
        if (Current >= Pages.Count)
        {
            return new PageReplacementResult(
                true,
                Helpers.CopyMemory(Memory),
                null);
        }
        int nextIndex = Current++;
        Page nextPage = Pages[nextIndex];
        if (Memory.Contains(nextPage))
        {
            Hits++;
            return new PageReplacementResult(
                false,
                Helpers.CopyMemory(Memory),
                PageReplacementStatus.PageHit);
        }

        Faults++;
        Memory[GetReplacementFrameIndex(nextIndex)] = nextPage;

        return new PageReplacementResult(
            false,
            Helpers.CopyMemory(Memory),
            PageReplacementStatus.PageFault);
    }

    protected abstract int GetReplacementFrameIndex(int nextPageIndex);
}
