using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace Visual_Novel_Engine
{
    public class ResourcesManager
    {
        private List<XDocument> resourcesFiles;
        string resourcesDirectory;
        private ResourcesManager()
        {
            resourcesFiles = new List<XDocument>();
        }

        private static ResourcesManager instance;
        public static ResourcesManager Create()
        {
            if (instance == null)
                instance = new ResourcesManager();

            return instance;
        }
        public void Initialize(string resourceDirectory)
        {
            if (this.resourcesDirectory == null)
                this.resourcesDirectory = resourceDirectory;
            foreach (string filePath in Directory.GetFiles(this.resourcesDirectory))
            {
                resourcesFiles.Add(XDocument.Load(filePath));
            }
        }

        public string Get(string resourceName, string name, string state = null)
        {
            XDocument resrouceFile = resourcesFiles.Where((XDocument xdoc) => xdoc.Root.Attribute("name").Value == resourceName).FirstOrDefault();

            if (resrouceFile == null)
                return string.Empty;

            string basepath = resrouceFile.Root.Attribute("basepath").Value;

            if (basepath.Contains(@"\") && !basepath.EndsWith("\\"))
                basepath += "\\";
            else if (basepath.Contains("/") && !basepath.EndsWith("/"))
                basepath += "/";


            XElement resourceNode = resrouceFile.Root.Elements("string").Where((XElement node) => node.Attribute("name").Value == name && ((node.Attribute("state") != null) ? node.Attribute("state").Value == state : true)).FirstOrDefault();


            if (resourceNode == null)
                return string.Empty;
            return basepath + resourceNode.Value;
        }
    }
}
