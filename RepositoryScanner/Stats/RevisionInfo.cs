using System;

namespace RepositoryScanner.Stats
{
    public class RevisionInfo
    {
        public DateTime Time { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public long Number { get; set; }
        public int FileCount { get; set; }
    }
}
