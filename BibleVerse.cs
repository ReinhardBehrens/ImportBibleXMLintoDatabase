using System;
using System.Collections.Generic;

namespace ConsoleApp3.Models;

public partial class BibleVerse
{
    public int Id { get; set; }

    public string VerseNr { get; set; } = null!;

    public string VerseContent { get; set; } = null!;

    public int? BibleVersionId { get; set; }

    public int BibleChapterId { get; set; }

    public virtual BibleChapter BibleChapter { get; set; } = null!;

    public virtual BibleVersion? BibleVersion { get; set; }
}
