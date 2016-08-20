using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
namespace Visual_Novel_Engine
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    
    public class Character
    {
        private string imageResourceId;
        private ResourcesManager resourcesManager;
        public static Character Parse(XElement node)
        {
            Character character = new Character();
            if (node.Attribute("name") != null)
                character.Name = node.Attribute("name").Value;
            if (node.Attribute("state") != null)
                character.State = node.Attribute("state").Value;
            if (node.Attribute("position") != null)
            { 
                string [] positionValue=node.Attribute("position").Value.Split(',');
                character.Position.X =  double.Parse(positionValue[0]);
                character.Position.Y =double.Parse( positionValue[1]);
            }
            if(node.Attribute("imageId")!=null)
            {
                character.imageResourceId = node.Attribute("imageId").Value;
            }

            return character;
        }
        
        public Character()
        {
            resourcesManager = ResourcesManager.Create();
        }

        Position _position;
        public Position Position
        {
            get
            {
                if (_position == null)
                    _position = new Position();

                return _position;
            }

        }
        public string State { get; set; }
        public string Name { get; set; }
        public string Image
        {
            get
            {
                if (imageResourceId != null)
                    return resourcesManager.Get("character", imageResourceId);
                else if (Name != null && State != null)
                    return resourcesManager.Get("character", Name, State);
                else return null;
            }
        }
    }
}
