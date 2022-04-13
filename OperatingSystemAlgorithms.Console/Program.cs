using OperatingSystemAlgorithms.PageReplacement;
using TextTableCreator;
using static System.Console;



// Display title
ForegroundColor = ConsoleColor.Green;
WriteLine("Page Replacement Algorithms".ToUpper());
ResetColor();

WriteLine(new string('-', 100));
WriteLine();
WriteLine();

// Input scheduling algorithm
WriteLine("Please enter the preferred scheduling algorithm");
ForegroundColor = ConsoleColor.DarkCyan;
WriteLine("1. First In First Out (FIFO)");
WriteLine("2. Optimal");
WriteLine("3. Least Recently Used");
ResetColor();
Write("> ");
int algo = InputHelpers.ReadInteger();

Write("Enter number of frames > ");
int frames = InputHelpers.ReadInteger();


WriteLine("Enter reference string > ");

int[] pageNumbers = InputHelpers.ReadIntegers();
Page[] pages = pageNumbers.Select(p => new Page(p)).ToArray();
PageArrivalSequence pageReference = new(pages);

IPageReplacementPolicy policy = algo switch
{
    3 => new LeastRecentlyUsedPageReplacementPolicy(pageReference, frames),
    2 => new OptimalPageReplacementPolicy(pageReference, frames),
    _ => new FirstInFirstOutPageReplacementPolicy(pageReference, frames)
};

List<PageReplacementResult> results = new();
var result = policy.Replace();

while (!result.Completed)
{
    results.Add(result);
    result = policy.Replace();
}


var builder = TableBuilder.For(results);
for (int i = 0; i < result.Memory.Length; i++)
{
    int localI = i;
    builder.AddColumn(localI.ToString(), result => result.Memory[localI]?.Number.ToString() ?? " ");
    
}
builder.AddColumn(
    "Status", 
    result => result.Status switch
    {
        PageReplacementStatus.PageFault => "*",
        PageReplacementStatus.PageHit => "Hit",
        _ => null
    });

var x = builder.Build();
x.WriteToConsole();

WriteLine($"Page Faults: {policy.Faults}");
WriteLine($"Page Hits: {policy.Hits}");