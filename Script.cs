using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{
    public class Script
    {
        public string Id { get; set; }

        /*
        public string Mood { get; set; }
        
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public string CharacterName { get; set; }
        public string CharacterImage {
            get 
            {
                if (CharacterName != null && Mood != null)
                    return string.Format(@"data\img\characters\{0}_{1}.png", CharacterName, Mood);
                else return null;
            } 
        }
        
         * */
        List<Character> characters;
        private ResourcesManager resourcesManager;
        public List<Character> Characters
        {
            get
            {
                if (characters == null)
                    characters = new List<Character>();
                return characters;
            }
        }
        public string Voice
        {
            get
            {
                return resourcesManager.Get("voice", Id);
            }
        }
        public Scene ParentScene { get; set; }
        public Script()
        {
            resourcesManager = ResourcesManager.Create();


        }

        //      virtual public Script GetLine();
    }
}
