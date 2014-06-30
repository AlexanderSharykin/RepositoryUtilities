using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using RepositoryScanner.RepositoryConnection;

namespace RepositoryVisualClient
{
    /// <summary>
    /// Class saves and loads repository configuration
    /// </summary>
    public class ConfigurationStorage
    {
        public const string Extension = ".cxml";
        public const string DefaultConfig = "Default.cxml";

        /// <summary>
        /// Gets last error
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Loads configuraton from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RepositoryInfo GetConfiguration(string path)
        {
            Error = null;
            if (string.IsNullOrWhiteSpace(path))
            {
                Error = new ArgumentNullException("path");
                return null;
            }
            if (false == File.Exists(path))
            {
                Error = new FileNotFoundException(path);
                return null;
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                var xDoc = XDocument.Load(fs);
                if (xDoc.Root == null || xDoc.Root.Name != "cfg")
                    return null;
                var repo = new RepositoryInfo();
                var node = xDoc.Root.Descendants("uri").FirstOrDefault();
                if (node != null)
                    repo.Uri = node.Value;
                node = xDoc.Root.Descendants("path").FirstOrDefault();
                if (node != null)
                    repo.Path = node.Value;
                node = xDoc.Root.Descendants("project").FirstOrDefault();
                if (node != null)
                    repo.ProjectName = node.Value;
                node = xDoc.Root.Descendants("revision").FirstOrDefault();
                if (node != null)
                {
                    int revision;
                    var a = node.Attribute("from");
                    if (a != null)
                        if (int.TryParse(a.Value, out revision) && revision > 0)
                            repo.MinRevision = revision;

                    a = node.Attribute("to");
                    if (a != null)
                        if (int.TryParse(a.Value, out revision) && revision > 0)
                            repo.MaxRevision = revision;
                }
                return repo;

            }
            catch (Exception ex)
            {
                Error = ex;
                return null;
            }
            finally
            {
                if (fs!=null)
                fs.Dispose();
            }
        }

        /// <summary>
        /// Saves configuration into file
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SetConfiguration(RepositoryInfo repository, string path)
        {
            Error = null;
            if (repository == null)
            {
                Error = new ArgumentNullException("repository");
                return false;
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                Error = new ArgumentNullException("path");
                return false;
            }
            var xRevision = new XElement("revision");
            if (repository.MinRevision.HasValue)
                xRevision.Add(new XAttribute("from", repository.MinRevision));
            if (repository.MaxRevision.HasValue)
                xRevision.Add(new XAttribute("to", repository.MaxRevision));
            var xDoc = new XDocument(new XElement("cfg",
                                                  new XElement("uri", repository.Uri),
                                                  new XElement("path", repository.Path),
                                                  new XElement("project", repository.ProjectName),
                                                  xRevision
                                         ));
            try
            {
                xDoc.Save(path);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public static bool CreateDefaultConfig()
        {
            return new ConfigurationStorage().SetConfiguration(new RepositoryInfo(), DefaultConfig);
        }
    }
}
