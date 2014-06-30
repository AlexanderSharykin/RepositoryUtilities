namespace RepositoryScanner.Activity
{
    public interface IActivityDistribution
    {
        double this[ActivityLevel level] { get; set; }
        ActivityLevel GetLevel(double measure);
    }
}