using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using RepositoryScanner.RepositoryConnection;
using RepositoryScanner.Stats;
using SharpSvn;
using SharpSvn.Security;

namespace RepositoryAccess
{
    public sealed class SvnRepositoryConnection : ISecuredRepositoryConnection
    {
        /// <summary>
        /// Loads history from repository
        /// </summary>
        /// <param name="repo"></param>
        /// <returns></returns>
        public IList<RevisionInfo> LoadHistory(RepositoryInfo repo)
        {
            Collection<SvnLogEventArgs> logEntries =null;            
            SvnLogArgs logArgs = new SvnLogArgs();

            using (var client = new SvnClient())
            {
                //applies revision settings
                if (repo.MinRevision.HasValue)
                    logArgs.End = new SvnRevision(repo.MinRevision.Value);

                if (repo.MaxRevision.HasValue)
                    logArgs.Start = new SvnRevision(repo.MaxRevision.Value);

                try
                {
                    logEntries = GetLog(repo, logArgs, client);
                }
                catch (SvnAuthenticationException)
                {
                    // login + password are required
                    logEntries = Retry(repo, logArgs, client);
                }
            }

            if (logEntries == null)
                return Enumerable.Empty<RevisionInfo>().ToList();

            return logEntries.Select(entry => new RevisionInfo()
                {
                    Time = entry.Time,
                    Author = entry.Author ?? string.Empty,
                    Comment = entry.LogMessage,
                    Number = entry.Revision,
                    FileCount = entry.ChangedPaths != null ? entry.ChangedPaths.Count : 0,
                }).ToList();
        }

        /// <see cref="http://stackoverflow.com/questions/13887645/svn-repository-authentication-with-sharpsvn-credentials-not-working"/>
        /// <summary>
        /// Asks login and password and then tries to connect again
        /// </summary>
        private Collection<SvnLogEventArgs> Retry(RepositoryInfo repo, SvnLogArgs logArgs, SvnClient client)
        {
            Collection<SvnLogEventArgs> logEntries;
            var e = new AuthenticationNeededEventArgs(false);
            OnAuthenticationNeeded(e);
            if (e.Cancel)
                return null;

            client.Authentication.Clear();
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(e.Login, e.Password);
            client.Authentication.SslServerTrustHandlers += delegate(object sender, SvnSslServerTrustEventArgs args)
                {
                    args.AcceptedFailures = args.Failures;
                    //e.Save = true;
                };
            logEntries = GetLog(repo, logArgs, client);

            return logEntries;
        }

        /// <summary>
        /// Connects to repository and loads the history
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logArgs"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        private Collection<SvnLogEventArgs> GetLog(RepositoryInfo repo, SvnLogArgs logArgs, SvnClient client)
        {
            Collection<SvnLogEventArgs> logEntries;
            // applies location settings
            if (!string.IsNullOrEmpty(repo.Uri))
            {
                Uri repository = new Uri(repo.Uri + "/" + repo.ProjectName);
                client.GetLog(repository, logArgs, out logEntries);
            }
            else if (!string.IsNullOrEmpty(repo.Path))
            {
                client.GetLog(repo.Path + "/" + repo.ProjectName, logArgs, out logEntries);
            }
            else
                throw new FileNotFoundException("Repository is not specified");
            return logEntries;
        }

        public event EventHandler<AuthenticationNeededEventArgs> AuthenticationNeeded;

        void OnAuthenticationNeeded(AuthenticationNeededEventArgs args)
        {
            var ev = AuthenticationNeeded;
            if (ev != null)
                ev(this, args);            
        }
    }
}
