using System;
using System.Collections.Generic;
using RepositoryScanner.Stats;

namespace RepositoryScanner.RepositoryConnection
{
    public interface IRepositoryConnection
    {        
        IList<RevisionInfo> LoadHistory(RepositoryInfo repo);

        event EventHandler<AuthenticationNeededEventArgs> AuthenticationNeeded;
    }
}