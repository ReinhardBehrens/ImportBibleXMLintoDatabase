using System;
using System.Collections.Generic;

namespace ConsoleApp3.Models;

public partial class BibleBook
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BookNumber { get; set; }

    public virtual ICollection<BibleChapter> BibleChapters { get; } = new List<BibleChapter>();
}
