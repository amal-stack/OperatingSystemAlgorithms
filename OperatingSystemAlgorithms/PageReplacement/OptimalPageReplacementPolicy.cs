namespace OperatingSystemAlgorithms.PageReplacement;

public class OptimalPageReplacementPolicy : PageReplacementPolicyBase
{
    public OptimalPageReplacementPolicy(
        PageArrivalSequence pages,
        int frameCount) : base(pages, frameCount)
    {}

    protected override int GetReplacementFrameIndex(int nextPageIndex)
    {
        int farthestPageIndex = nextPageIndex;
        int targetFrameIndex = 0;
        for(int i = 0; i < Memory.Length; i++)
        {
            // Return the current frame index if the frame is empty
            if (Memory[i] is null)
            {
                return i;
            }
            int j;
            for (j = nextPageIndex; j < Pages.Count; j++)
            {
                if (Memory[i] == Pages[j])
                {
                    if (j > farthestPageIndex)
                    {
                        farthestPageIndex = j;
                        targetFrameIndex = i;
                    }
                    break;
                }
            }

            // If page not found, replacing that frame is optimal
            if (j == Pages.Count)
            {
                return i;
            }
        }
        return targetFrameIndex;
    }
}

