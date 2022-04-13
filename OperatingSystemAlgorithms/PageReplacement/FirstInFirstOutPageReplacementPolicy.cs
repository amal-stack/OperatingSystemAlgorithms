namespace OperatingSystemAlgorithms.PageReplacement;

public class FirstInFirstOutPageReplacementPolicy : PageReplacementPolicyBase
{
    private int _nextReplaceIndex = 0;

    public FirstInFirstOutPageReplacementPolicy(
        PageArrivalSequence pages,
        int frameCount) : base(pages, frameCount) 
    {}

    public override PageReplacementResult Replace()
    {
        PageReplacementResult result = base.Replace();

        if (result.Status is PageReplacementStatus.PageFault)
        {
            IncrementNextReplaceIndex();
        }
        return result;
    }

    protected override int GetReplacementFrameIndex(int nextPageIndex) => _nextReplaceIndex;

    private void IncrementNextReplaceIndex() 
        => _nextReplaceIndex = (_nextReplaceIndex + 1) % Memory.Length;
}
