using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{
    public class Line:Script
    {
        private string imageResourceId;
        public static Line Parse(System.Xml.Linq.XElement node)
        {
            Line line = new Line();
            line.Text = node.Element("Text").Value;
            line.Id = node.Attribute("id").Value;

            if (node.Attribute("imageId") != null)
            {
                line.imageResourceId = node.Attribute("imageId").Value;
            }

            if (node.Element("Characters") != null)
            {
                foreach (System.Xml.Linq.XElement characterNode in node.Element("Characters").Elements("Character"))
                {
                    line.Characters.Add(Character.Parse(characterNode));
                }
            }

            System.Xml.Linq.XAttribute nextAttribute = node.Attribute("next");
            if (nextAttribute != null && !string.IsNullOrEmpty(nextAttribute.Value))
                line.Next = nextAttribute.Value;

            return line;
        }
        public string Image
        {
            get
            {
                ResourcesManager resourcesManager = ResourcesManager.Create();
                if (imageResourceId != null)
                    return resourcesManager.Get("line", imageResourceId);
                else return null;
            }
        }
        public string Next { set; get; }
        public string Text { set; get; }
    }
}
