using System;
using System.Collections.Generic;

namespace ConsoleApp3.Models;

public partial class BibleChapter
{
    public int Id { get; set; }

    public string? ChapterName { get; set; }

    public string? ChapterDescription { get; set; }

    public string? ChapterTitle { get; set; }

    public int ChapterNumber { get; set; }

    public int? BibleVersionId { get; set; }

    public int BibleBookId { get; set; }

    public virtual BibleBook BibleBook { get; set; } = null!;

    public virtual ICollection<BibleVerse> BibleVerses { get; } = new List<BibleVerse>();

    public virtual BibleVersion? BibleVersion { get; set; }
}
