using System.ComponentModel;

namespace RepositoryScanner.RepositoryConnection
{
    public class AuthenticationNeededEventArgs:CancelEventArgs
    {
         public AuthenticationNeededEventArgs(bool cancel = false):base(cancel)
         {
             
         }

         public string Login { get; set; }
         public string Password { get; set; }
    }
}