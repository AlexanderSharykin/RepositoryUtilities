using System;

namespace RepositoryScanner.Activity
{
    public class UniformActivityDistribution : ActivityDistributionBase
    {
        public UniformActivityDistribution():this(1)
        {            
        }

        public UniformActivityDistribution(double maxActivity = 1.0)
        {
            if (maxActivity <= 0)
                throw new ArgumentOutOfRangeException("maxActivity");
            this[ActivityLevel.Great] = maxActivity;
            this[ActivityLevel.High] = maxActivity*0.75;
            this[ActivityLevel.Average] = maxActivity*0.5;
            this[ActivityLevel.Low] = maxActivity*0.25;
        }
    }
}
