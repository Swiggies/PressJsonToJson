using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace PressJsonToJson
{
    /// <summary>
    /// Interaction logic for MoreGametype.xaml
    /// </summary>
    public partial class MoreGametype : MetroWindow
    {
        public VotingTypes typeStuff;

        public MoreGametype(string[] maps, string typeName)
        {
            InitializeComponent();
            typeStuff = new VotingTypes(typeName);

            foreach (KeyValuePair<string, string> kv in Defaults.defaultMaps)
            {
                var newItem = new ListBoxItem();
                newItem.Content = kv.Key;
                mapsList.Items.Add(newItem);
            }

            foreach (string map in maps)
            {
                var newItem = new ListBoxItem();
                newItem.Content = map.Split('\\').Last();
                mapsList.Items.Add(newItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = mapsList.SelectedItems;
            foreach (ListBoxItem x in t)
            {
                var m = new Maps(x.Content.ToString());
                if (Defaults.defaultMaps.ContainsKey(m.displayName))
                    typeStuff.SpecificMaps.Add(new Maps(m.displayName, Defaults.defaultMaps[m.displayName]));
                else
                    typeStuff.SpecificMaps.Add(m);
            }
            int lineCount = commands.LineCount;

            for (int line = 0; line < lineCount; line++)
            {
                string s = commands.GetLineText(line).Trim();
                typeStuff.commands.Add(s);
            }

            this.Close();
        }
    }
}
