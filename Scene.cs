using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{
    public class Scene
    {
        public string State { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Dialogue { get; set; }

        private ResourcesManager resourcesManager;
        public string BackgroundImage
        {
            get
            {
                return resourcesManager.Get("background", Name, State);
            }
        }
        public string BackgroundMusic
        {
            get
            {
                return resourcesManager.Get("backgroundMusic", Id, State);
            }
        }
        private Dictionary<string, Script> scripts;

        public static Scene Parse(System.Xml.Linq.XElement node)
        {
            Scene scene = new Scene();
            scene.Id = node.Attribute("id").Value;

            if (node.Attribute("name") != null)
                scene.Name = node.Attribute("name").Value;
            
            if (node.Attribute("state") != null)
                scene.State = node.Attribute("state").Value;

            return scene;
        }
        public Scene()
        {
            scripts = new Dictionary<string, Script>();
            resourcesManager = ResourcesManager.Create();
        }
        /*
        public Dictionary<string, Script> Scripts
        {
            get
            {
                if (scripts == null)
                    scripts = new Dictionary<string,Script>();
                return scripts;
            }

        }*/

        public void AddScript(Script script)
        {
            script.ParentScene = this;
            scripts.Add(script.Id, script);
        }
        public Script Get(string id)
        {
            return scripts[id];
        }
    }
}
