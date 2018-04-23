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

            foreach (KeyValuePair<string, string> kv in defaults.defaultTypes)
            {
                var newItem = new ListBoxItem();
                newItem.Content = kv.Key;
                ListVariants.Items.Add(newItem);
            }

            var types = GetGametypes();
            foreach (string s in types)
            {
                var newItem = new ListBoxItem();
                newItem.Content = s.Split('\\').Last();
                ListVariants.Items.Add(newItem);
                newItem.Selected += (sender, r) =>
                {
                    var w = new MoreGametype(maps, newItem.Content.ToString());
                    if (w.ShowDialog() == false)
                    {
                        Types foo = w.typeStuff;
                        for(int i = 0; i < this.types.Count; i++)
                        {
                            if (this.types[i].displayName == foo.displayName)
                            {
                                this.types[i] = foo;
                                return;
                            }
                        }
                        this.types.Add(foo);
                    }
                };
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

        public string[] GetGametypes()
        {
            try
            {
                return Directory.GetDirectories($"{Environment.CurrentDirectory}/../mods/variants");
            }catch(IOException e)
            {
                MessageBox.Show("Couldn't find any variants. Is this in the right place?");
                return new string[0];
            }
        }

    public string ConvertToJson()
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
                }else
                {
                    newMap = new Maps(x.Content.ToString());
                }
                maps.Add(newMap);
            }
            v.Maps = maps;

            List<Types> types = new List<Types>();
            var t = ListVariants.SelectedItems;
            foreach(ListBoxItem x in t)
            {
                Types type = new Types(x.Content.ToString());
                type.displayName = x.Content.ToString();
                type.typeName = x.Content.ToString();
                types.Add(type);
                foreach(Types y in this.types)
                {
                    if(y.displayName == type.displayName && y.typeName == type.typeName)
                    {
                        type.commands = y.commands;
                        type.SpecificMaps = y.SpecificMaps;
                    }
                }
            }
            v.Types = types;
            return JsonConvert.SerializeObject(v, Formatting.Indented);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_Ouput.Text = ConvertToJson();
        }
    }
}
