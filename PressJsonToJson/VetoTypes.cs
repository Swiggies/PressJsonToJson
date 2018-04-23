using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressJsonToJson
{
    public class VetoTypes
    {
        public string displayName;
        public string typeName;
        public List<string> commands = new List<string>();

        public VetoTypes(string name, string[] commands, Maps[] maps)
        {
            displayName = name;
            typeName = name;
            this.commands = commands.ToList();
        }

        public VetoTypes (string name, string[] commands, List<Maps> maps)
        {
            displayName = name;
            typeName = name;
            this.commands = commands.ToList();
        }

        public VetoTypes (string name)
        {
            displayName = name;
            typeName = name;
        }

        public VetoTypes () { }
    }
}
