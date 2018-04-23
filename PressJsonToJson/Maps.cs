using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressJsonToJson
{
    public class Maps
    {
        public string displayName;
        public string mapName;


        public Maps(string dName, string mName)
        {
            displayName = dName;
            mapName = mName;
        }


        public Maps(string name)
        {
            displayName = name;
            mapName = name;
        }

        public Maps() { }
    }


}
