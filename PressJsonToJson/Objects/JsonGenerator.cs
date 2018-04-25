using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows;
using System.Collections;

namespace PressJsonToJson.Objects
{
    public static class JsonGenerator
    {
        //Make the veto json
        public static string MakeVetoJson(IList typeItems, List<VotingTypes> types)
        {
            Playlist p = new Playlist();
            List<Veto> vList = new List<Veto>();

            var t = typeItems;
            //Go through each selected item in the list
            foreach (ListBoxItem x in t)
            {
                Veto v = new Veto();
                v.gametype.displayName = x.Content.ToString();
                v.gametype.typeName = x.Content.ToString();
                //Check if they match with any of the types in this.types
                foreach (VotingTypes y in types)
                {
                    //If they do, do things
                    if (y.displayName == v.gametype.displayName && y.typeName == v.gametype.typeName)
                    {
                        //Add commands to the veto
                        v.gametype.commands = y.commands;
                        foreach (Maps m in y.SpecificMaps)
                        {
                            //If a map in the list = a default map or not
                            if (Defaults.defaultMaps.ContainsKey(m.displayName))
                            {
                                v.map.displayName = m.displayName;
                                v.map.mapName = Defaults.defaultMaps[m.displayName];
                            }
                            else
                            {
                                v.map.displayName = m.displayName;
                                v.map.mapName = m.displayName;
                            }
                            vList.Add(v);
                        }
                    }
                }

            }
            p.playlist = vList;

            if (vList.Count < 2)
                MessageBox.Show("You need at least 2 gametypes selected.");
            if (types.Count >= 2)
                MessageBox.Show("No maps selected. Making a veto.json instead.");

            return JsonConvert.SerializeObject(p, Formatting.Indented);
        }

        public static string MakeVotingJson(IList mapItems, IList typeItems, List<VotingTypes> types)
        {
            Voting v = new Voting();

            List<Maps> maps = new List<Maps>();
            var m = mapItems;
            foreach (ListBoxItem x in m)
            {
                Maps newMap = new Maps();
                if (Defaults.defaultMaps.ContainsKey(x.Content.ToString()))
                {
                    newMap = new Maps(x.Content.ToString(), Defaults.defaultMaps[x.Content.ToString()]);
                }
                else
                {
                    newMap = new Maps(x.Content.ToString());
                }
                maps.Add(newMap);
            }
            v.Maps = maps;

            List<VotingTypes> _types = new List<VotingTypes>();
            var t = typeItems;
            foreach (ListBoxItem x in t)
            {
                VotingTypes type = new VotingTypes(x.Content.ToString());
                types.Add(type);
                foreach (VotingTypes y in types)
                {
                    if (y.displayName == type.displayName && y.typeName == type.typeName)
                    {
                        type.commands = y.commands;
                        type.SpecificMaps = y.SpecificMaps;
                    }
                }
            }
            v.Types = types;
            if (maps.Count < 2 || types.Count < 2)
                MessageBox.Show("You have less than 2 maps or game variants selected. There will be some compatibility issues with Eldewrito.");
            return JsonConvert.SerializeObject(v, Formatting.Indented);
        }
    }
}
