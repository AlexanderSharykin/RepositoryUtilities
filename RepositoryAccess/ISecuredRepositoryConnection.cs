using System;
using RepositoryScanner.RepositoryConnection;

namespace RepositoryAccess
{
    public interface ISecuredRepositoryConnection : IRepositoryConnection
    {
        event EventHandler<AuthenticationNeededEventArgs> AuthenticationNeeded;
    }
}