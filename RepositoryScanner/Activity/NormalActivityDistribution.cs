using System;
using System.ComponentModel;

namespace RepositoryScanner.Activity
{
    [Description("Standard Normal Activity Distribution")]
    public class NormalActivityDistribution : ActivityDistributionBase
    {
        public NormalActivityDistribution():this(1)
        {            
        }

        public NormalActivityDistribution(double maxActivity = 1.0)
        {
            if (maxActivity <= 0)
                throw new ArgumentOutOfRangeException("maxActivity");
            this[ActivityLevel.Great] = maxActivity;
            this[ActivityLevel.High] = maxActivity*0.833;
            this[ActivityLevel.Average] = maxActivity*0.667;            
            this[ActivityLevel.Low] = maxActivity*0.333;
        }
    }
}
