namespace RepositoryScanner.RepositoryConnection
{
    public class RepositoryInfo
    {
        /// <summary>
        /// Repository address
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// Path to working copy folder
        /// </summary>
        public string Path { get; set; }
        public string ProjectName { get; set; }

        public int? MinRevision { get; set; }
        public int? MaxRevision { get; set; }
    }
}
