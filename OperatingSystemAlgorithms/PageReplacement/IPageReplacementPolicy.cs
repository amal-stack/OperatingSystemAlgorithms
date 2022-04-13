using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OperatingSystemAlgorithms.PageReplacement;

internal class Helpers
{
    public static Page?[] CopyMemory(Page?[] memory)
    {
        Page?[] pages = new Page?[memory.Length];
        for (int i = 0; i < memory.Length; i++)
        {
            pages[i] = memory[i];
        }

        return pages;
    }
}

public interface IPageReplacementPolicy
{
    public PageReplacementResult Replace();

    public PageArrivalSequence Pages { get; }

    int Faults { get; }

    int Hits { get; }

    IEnumerable<Page?> MemoryState { get; }
}

public class PageReplacementResult
{
    public PageReplacementResult(
        bool completed, 
        Page?[] memory, 
        PageReplacementStatus? status)
    {
        Completed = completed;
        Memory = memory;
        Status = status;
    }

    public bool Completed { get; }

    public Page?[] Memory { get; }

    public PageReplacementStatus? Status { get; }
}

public enum PageReplacementStatus { PageHit, PageFault };

public readonly record struct Page(int Number);

