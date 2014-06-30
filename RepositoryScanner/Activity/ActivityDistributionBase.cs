using System;
using System.Linq;

namespace RepositoryScanner.Activity
{
    public class ActivityDistributionBase: IActivityDistribution
    {
        private readonly double[] _ranges;

        public ActivityDistributionBase()
        {
            var maxLevel = Enum.GetValues(typeof(ActivityLevel)).Cast<int>().Max();
            _ranges = new double[maxLevel];
        }

        public double this[ActivityLevel level]
        {
            get { return _ranges[(int)level]; }
            set
            {
                int lvl = (int)level;
                if (lvl >= _ranges.Length)
                    return;
                if (lvl > 0 && _ranges[lvl - 1] >= value || lvl < _ranges.Length - 1 && _ranges[lvl + 1] <= value)
                    return;
                _ranges[lvl] = value;
            }
        }

        public ActivityLevel GetLevel(double measure)
        {                            
            for (int lvl = 0; lvl < _ranges.Length; lvl++)
                if (_ranges[lvl] >= measure)
                    return (ActivityLevel) lvl;
             return ActivityLevel.Great;
        }
    }
}
