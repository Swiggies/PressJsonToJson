using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressJsonToJson
{
    public class Types
    {
        public string displayName;
        public string typeName;
        public List<string> commands = new List<string>();
        public List<Maps> SpecificMaps = new List<Maps>();

        public Types(string name, string[] commands, Maps[] maps)
        {
            displayName = name;
            typeName = name;
            this.commands = commands.ToList();
            SpecificMaps = maps.ToList();
        }

        public Types(string name, string[] commands, List<Maps> maps)
        {
            displayName = name;
            typeName = name;
            this.commands = commands.ToList();
            SpecificMaps = maps;
        }

        public Types(string name)
        {
            displayName = name;
            typeName = name;
        }
    }
}
