using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace Visual_Novel_Engine
{
    public class VisualNovelEngine
    {


        XDocument storyFile;
        private Story story;
        int steps = 0;
        private Dictionary<string, Script> scripts;
        string[] saveFileNames;
        public string[] SaveFiles
        {
            get
            {
                return saveFileNames;
            }
        }
        string saveDirectory;

        List<Script> progress;
        private static string storyFileName;
        private static VisualNovelEngine instance;

        public  void Reset()
        {
            saveDirectory = "saves";
            this.story = new Story();
            progress = new List<Script>();
            scripts = new Dictionary<string, Script>();
            steps = 0;
            
            if (storyFileName != null && File.Exists(storyFileName))
                this.OpenStory(storyFileName);
            else throw new FileNotFoundException("Story file is not found");
            getAllSaveFiles();
            
            //if (instance != null)
                //instance = new VisualNovelEngine(storyFileName);
        }
        public  static VisualNovelEngine Create(string storyFileName = null)
        {
            if(instance==null)
                instance = new VisualNovelEngine(storyFileName);
            return instance;
        }
        private VisualNovelEngine(string storyFileName = null)
        {
            
            //saveDirectory = (new System.Configuration.AppSettingsReader()).GetValue("SaveFilesLocation",typeof(string)).ToString();
            saveDirectory = "saves";
            this.story = new Story();
            progress = new List<Script>();
            scripts = new Dictionary<string, Script>();
            steps = 0;
            VisualNovelEngine.storyFileName=storyFileName;
            if (storyFileName != null && File.Exists(storyFileName))
                this.OpenStory(storyFileName);
            else throw new FileNotFoundException("Story file is not found");
            getAllSaveFiles();
        }
        /// <summary>
        /// Opens an story file
        /// </summary>
        /// <param name="storyFileName">Location of story file</param>
        public void OpenStory(string storyFileName)
        {
            ///[Story file was read]
            if (File.Exists(storyFileName))
            {
                storyFile = XDocument.Load(storyFileName);
            }
            else throw new FileNotFoundException("Story file was not found in " + storyFileName);
            ///[Story file was read]

            ///[Story file was parsed]
            if (storyFile != null)
                parseStory();
            ///[Story file was parsed]


        }

        /// <summary>
        /// Parses story
        /// </summary>
        private void parseStory()
        {
            foreach (XElement sceneNode in storyFile.Descendants("Scene"))
            {
                Scene scene = Scene.Parse(sceneNode);

                foreach (XElement lineNode in sceneNode.Elements("Line"))
                {
                    Line line = Line.Parse(lineNode);

                    scripts.Add(line.Id, line);
                    scene.AddScript(line);
                }
                foreach (XElement routeNode in sceneNode.Elements("Route"))
                {

                    Route route = Route.Parse(routeNode);

                    scripts.Add(route.Id, route);
                    scene.AddScript(route);
                }
                ///Scene was added to story
                if (story != null)
                    story.AddScene(scene);

            }
            begin = scripts.Values.First();
        }

        Script begin;
        /// <summary>
        /// Fetches the next Script
        /// </summary>
        /// <returns>Script</returns>
        public Script Next(Script choice = null)
        {
            if (choice == null)
                choice = begin;
            if (steps == 0)
            {
                begin = choice;
            }
            else if (choice is Line && (choice as Line).Next != null)
            {
                begin = scripts[(choice as Line).Next];

            }

            steps++;
            progress.Add(begin);
            return begin;
        }


        void getAllSaveFiles()
        {
         
            if (Directory.Exists(saveDirectory))
                saveFileNames=Directory.GetFiles(saveDirectory);
        }
        public void Save(int id)
        {
            string[] progressArray= (from Script s in progress select s.Id).ToArray<string>();
            //string progressContent = string.Join(",",progressArray);
            
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            string saveFileName = string.Format("saves\\{0}.sav", id);
            //string saveContent = begin.Id;
            string saveContent = string.Join(",", progressArray);

            File.WriteAllText(saveFileName, saveContent);

        }
        
        public void Load(int id)
        {
            string saveFileName = string.Format("saves\\{0}.sav", id);

            if (File.Exists(saveFileName))
            {
                string loadContent = File.ReadAllText(saveFileName);
                string lastId=loadContent.Split(',').Last();
                begin = scripts[lastId];
            }
        }
    }

}
