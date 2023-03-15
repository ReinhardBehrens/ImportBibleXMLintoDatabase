using ConsoleApp3.Models;
using ImportBibleXMLintoDatabase;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
// For JESUS CHRIST the Son of God
//string xmlDocument = "C:\\Users\\Reinhard Behrens`\\Downloads\\SF_2009-01-23_ENG_KJV_(KING JAMES VERSION).xml";
string xmlDocument = "C:\\Users\\Reinhard Behrens`\\Downloads\\Bible_English_ESV\\Bible_English_ESV.xml";

XDocument xdoc = XDocument.Load(xmlDocument);

bool inBiblebook = false;
bool inBibleChapter = false;
bool countingChapters = false;
bool countingVerses = false;
bool inChapter = false;
int noOfChapters = 0;
int noOfVerses = 0;
int startCounting = 0;
bool lastChapter = false;
bool countedAllVerses = false;
bool waitforLastVersesToIterate = false;
string BibleTranslation = "KJV";

var db = new BibleDbContext();

foreach (XElement element in xdoc.Descendants("XMLBIBLE"))
{

    BibleVersion bibleversion = db.BibleVersions.FirstOrDefault(x => x.Name == element.Attribute("biblename").Value);
    if (bibleversion == null || bibleversion.Name == null)
    {
        bibleversion = new BibleVersion();
        bibleversion.Name = element.LastAttribute.Value;
        db.BibleVersions.Add(bibleversion);
        db.SaveChanges();
        bibleversion = db.BibleVersions.FirstOrDefault(x => x.Name == element.Attribute("biblename").Value);
    }

    Console.WriteLine(element.Name);

    foreach (XElement biblebook in element.Descendants("BIBLEBOOK"))
    {
        Console.WriteLine("-->"+biblebook.Attribute("bname").Value);
        BibleBook book = db.BibleBooks.FirstOrDefault(x=>x.BookNumber == int.Parse(biblebook.Attribute("bnumber").Value));
        Console.WriteLine("Book Number : " + int.Parse(biblebook.Attribute("bnumber").Value));
        noOfChapters = 0;

        // GET BIBLEBOOK ID FROM DB
        foreach (XElement bibleChapter in biblebook.Descendants("CHAPTER"))
        {
            if (bibleChapter.Name=="CHAPTER")
            {
                Console.WriteLine("++-->" + bibleChapter.Attribute("cnumber").Value);
                BibleChapter biblechapter = new BibleChapter();
                biblechapter.ChapterNumber = int.Parse(bibleChapter.Attribute("cnumber").Value);
                biblechapter.BibleVersion = bibleversion;
                biblechapter.BibleBook = book;
                noOfChapters++;
                noOfVerses = 0;
                db.BibleChapters.Add(biblechapter);
                db.SaveChanges();
                foreach (XElement bibleVers in bibleChapter.Descendants("VERS"))
                {
                    BibleVerse bibleVerse = new BibleVerse();
                    bibleVerse.BibleVersion = bibleversion;
                    bibleVerse.VerseNr = bibleVers.Attribute("vnumber").Value;
                    bibleVerse.VerseContent = bibleVers.Value;
                    bibleVerse.BibleChapter = biblechapter;
                    Console.WriteLine("+++--->" + bibleVers.Attribute("vnumber").Value);
                    Console.WriteLine("++=========>" + bibleVers.Value);
                    db.BibleVerses.Add(bibleVerse);
                    db.SaveChanges();
                    noOfVerses++;
                }
            
                Console.WriteLine(noOfVerses);
           }
        }
        //Console.WriteLine(noOfChapters);
    }
}
