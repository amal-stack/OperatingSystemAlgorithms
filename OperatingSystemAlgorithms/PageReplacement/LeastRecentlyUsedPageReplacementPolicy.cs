namespace OperatingSystemAlgorithms.PageReplacement;

public class LeastRecentlyUsedPageReplacementPolicy : PageReplacementPolicyBase
{
    public LeastRecentlyUsedPageReplacementPolicy(
        PageArrivalSequence pages,
        int frameCount) : base (pages, frameCount)
    {
    }

    protected override int GetReplacementFrameIndex(int nextPageIndex)
    { 
        int leastRecentlyUsedPageIndex = nextPageIndex - 1;
        int targetFrameIndex = 0;
        for (int i = 0; i < Memory.Length; i++)
        {
            // Return the current frame index because the frame is empty
            if (Memory[i] is null)
            {
                return i;
            }

            int j;
            for (j = nextPageIndex - 1; j > -1; j--)
            {
                if (Memory[i] == Pages[j])
                {
                    if (j < leastRecentlyUsedPageIndex)
                    {
                        leastRecentlyUsedPageIndex = j;
                        targetFrameIndex = i;
                    }
                    break;
                }
            }
            // This should never happen
            if (j == -1)
            {
                throw new InvalidOperationException("Page in memory was not found in the page arrival sequence.");
            }
        }
        return targetFrameIndex;
    }
}

