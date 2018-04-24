using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows;

namespace PressJsonToJson
{
    public class Defaults
    {
        public Dictionary<string, string> defaultMaps = new Dictionary<string, string>()
        {
            {"Standoff", "bunkerworld" },
            {"Valhalla", "riverworld" },
            {"Guardian", "guardian" },
            {"Last Resort", "zanzibar" },
            {"Sandtrap", "shrine" },
            {"Diamondback", "s3d_avalanche" },
            {"High Ground", "deadlock" },
            {"Ice Box", "s3d_turf" },
            {"Narrows", "chill" },
            {"The Pit", "cyberdyne" },
            {"Reactor", "s3d_reactor" },
            { "Edge", "s3d_edge" },
        };

        public Dictionary<string, string> defaultTypes = new Dictionary<string, string>()
        {
            {"Slayer", "slayer" },
            {"Multi Flag", "Team CTF" },
            {"Team King", "Team Crazy King" },
            {"Oddball", "Oddball" }
        };

        public string[] GetMaps()
        {
            try
            {
                return Directory.GetDirectories($"{Environment.CurrentDirectory}/../mods/maps");

            }
            catch (IOException e)
            {
                MessageBox.Show("Couldn't find any maps. Is this in the right place?");
                return new string[0];
            }
        }

        public List<string> GetGametypes()
        {
            try
            {
                return Directory.GetDirectories($"{Environment.CurrentDirectory}/../mods/variants").ToList();
            }
            catch (IOException e)
            {
                MessageBox.Show("Couldn't find any variants. Is this in the right place?");
                return new List<string>();
            }
        }
    }
}
