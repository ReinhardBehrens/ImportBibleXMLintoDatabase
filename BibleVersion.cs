using System;
using System.Collections.Generic;

namespace ConsoleApp3.Models;

public partial class BibleVersion
{
    public int Id { get; set; }

    public string Version { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Edition { get; set; } = null!;

    public string HistorySummary { get; set; } = null!;

    public virtual ICollection<BibleChapter> BibleChapters { get; } = new List<BibleChapter>();

    public virtual ICollection<BibleVerse> BibleVerses { get; } = new List<BibleVerse>();
}
