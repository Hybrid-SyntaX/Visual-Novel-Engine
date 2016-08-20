using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{
    class Story
    {

        //public Dictionary<string, Script> scripts;
        Dictionary<string,Scene> scenes;
        public Story()
        {
            scenes = new Dictionary<string, Scene>();
            
        }
        /*
        public LinkedListNode<Scene> NextScene()
        {
            LinkedListNode<Scene> scene = Scenes.First;
            return scene.Next;
        }*/
        

        /*
        /// <summary>
        /// لیست تمام صحنه ها
        /// </summary>
        public Dictionary<string,Scene> Scenes 
        { 
            get
            {
                if (scenes == null)
                    scenes = new Dictionary<string,Scene>();

                return scenes;
            }
            
        }
        */
        public void AddScene(Scene scene)
        {
            scenes.Add(scene.Id,scene);
        }

        public Scene FindScene(string id)
        {
            return scenes[id];
        }
        public Scene First { get { return scenes.First().Value; } }
    }
}
