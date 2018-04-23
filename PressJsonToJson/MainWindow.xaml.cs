using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
using MahApps.Metro.Controls;

namespace PressJsonToJson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        List<Types> types = new List<Types>();
        List<Maps> maps = new List<Maps>();
        Defaults defaults = new Defaults();

        public MainWindow()
        {
            InitializeComponent();

            foreach (KeyValuePair<string, string> kv in defaults.defaultMaps)
            {
                var newItem = new ListBoxItem();
                newItem.Content = kv.Key;
                ListMaps.Items.Add(newItem);
            }

            var maps = GetMaps();
            foreach (string s in maps)
            {
                var newItem = new ListBoxItem();
                newItem.Content = s.Split('\\').Last();
                ListMaps.Items.Add(newItem);
            }

            var types = GetGametypes();
            foreach (KeyValuePair<string, string> kv in defaults.defaultTypes)
            {
                var newItem = new ListBoxItem();
                newItem.Content = kv.Key;
                ListVariants.Items.Add(newItem);
                newItem.Selected += (sender, r) => AdvancedGametypeWindow(maps, newItem);
            }

            foreach (string s in types)
            {
                var newItem = new ListBoxItem();
                newItem.Content = s.Split('\\').Last();
                ListVariants.Items.Add(newItem);
                newItem.Selected += (sender, r) => AdvancedGametypeWindow(maps, newItem);
            }
        }

        private void AdvancedGametypeWindow(string[] maps, ListBoxItem boxItem)
        {
            var w = new MoreGametype(maps, boxItem.Content.ToString());
            if (w.ShowDialog() == false)
            {
                Types foo = w.typeStuff;
                for (int i = 0; i < this.types.Count; i++)
                {
                    if (this.types[i].displayName == foo.displayName)
                    {
                        this.types[i] = foo;
                        return;
                    }
                }
                this.types.Add(foo);
            }
        }

        public string[] GetMaps()
        {
            try
            {
                return Directory.GetDirectories($"{Environment.CurrentDirectory}/../mods/maps");

            } catch (IOException e)
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
            }catch(IOException e)
            {
                MessageBox.Show("Couldn't find any variants. Is this in the right place?");
                return new List<string>();
            }
        }

    public void ConvertToJson()
        {
            if (ListMaps.SelectedItems.Count > 0)
                txt_Ouput.Text = MakeVotingJson();
            else
                txt_Ouput.Text = MakeVetoJson();          
        }

        public string MakeVetoJson()
        {
            Playlist p = new Playlist();
            List<Veto> vList = new List<Veto>();

            var t = ListVariants.SelectedItems;
            //Go through each selected item in the list
            foreach (ListBoxItem x in t)
            {
                Veto v = new Veto();
                v.gametype.displayName = x.Content.ToString();
                v.gametype.typeName = x.Content.ToString();
                //Check if they match with any of the types in this.types
                foreach (Types y in this.types)
                {
                    //If they do, do things
                    if (y.displayName == v.gametype.displayName && y.typeName == v.gametype.typeName)
                    {
                        v.gametype.commands = y.commands;
                        Console.WriteLine(y.SpecificMaps.Last().displayName);
                        if (defaults.defaultMaps.ContainsKey(y.SpecificMaps.Last().displayName))
                        {
                            v.map.displayName = y.SpecificMaps.Last().displayName;
                            v.map.mapName = defaults.defaultMaps[y.SpecificMaps.Last().displayName];
                        }
                        else
                        {
                            v.map.displayName = y.SpecificMaps.Last().displayName;
                            v.map.mapName = y.SpecificMaps.Last().displayName;
                        }
                    }
                }
                vList.Add(v);
            }
            p.playlist = vList;

            if (types.Count < 2)
                MessageBox.Show("You need at least 2 gametypes selected.");
            if (types.Count >= 2)
                MessageBox.Show("No maps selected. Making a veto.json instead.");

            return JsonConvert.SerializeObject(p, Formatting.Indented);
        }

        public string MakeVotingJson()
        {
            Voting v = new Voting();

            List<Maps> maps = new List<Maps>();
            var m = ListMaps.SelectedItems;
            foreach (ListBoxItem x in m)
            {
                Maps newMap = new Maps();
                if (defaults.defaultMaps.ContainsKey(x.Content.ToString()))
                {
                    newMap = new Maps(x.Content.ToString(), defaults.defaultMaps[x.Content.ToString()]);
                }
                else
                {
                    newMap = new Maps(x.Content.ToString());
                }
                maps.Add(newMap);
            }
            v.Maps = maps;

            List<Types> types = new List<Types>();
            var t = ListVariants.SelectedItems;
            foreach (ListBoxItem x in t)
            {
                Types type = new Types(x.Content.ToString());
                types.Add(type);
                foreach (Types y in this.types)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertToJson();
        }
    }
}
