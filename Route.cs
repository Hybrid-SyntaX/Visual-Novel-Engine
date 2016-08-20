using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Visual_Novel_Engine
{
    public class Route : Script
    {
        public static Route Parse(XElement node)
        {

            Route route = new Route();
            route.Id = node.Attribute("id").Value;

            foreach (XElement routeLineNode in node.Descendants("Line"))
            {
                Line line = new Line();
                line.Text = routeLineNode.Value;
                line.Id = routeLineNode.Attribute("id").Value;

                XAttribute nextAttribute = routeLineNode.Attribute("next");
                if (nextAttribute != null && !string.IsNullOrEmpty(nextAttribute.Value))
                    line.Next = nextAttribute.Value;


                route.Add(line);
            }
            if (node.Element("Characters") != null)
                foreach (XElement characterNode in node.Element("Characters").Elements("Character"))
                {
                    route.Characters.Add(Character.Parse(characterNode));
                }

            return route;
        }
        List<Line> lines;
        public List<Line> Lines
        {
            get
            {
                if (lines == null)
                    lines = new List<Line>();

                return lines;
            }
        }

        public void Add(Line line)
        {
            Lines.Add(line);
        }
    }
}
