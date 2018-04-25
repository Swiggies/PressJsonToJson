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
using PressJsonToJson.Objects;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PressJsonToJson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string modsPath = Settings.Default.modsPath;

        List<VotingTypes> types = new List<VotingTypes>();
        List<Maps> maps = new List<Maps>();

        public MainWindow()
        {

            InitializeComponent();
            //CheckLocation();
            PopulateListViews();
            if (Settings.Default.firstRun)
            {
                ShowLocateDialog();
                Settings.Default.firstRun = false;
                Settings.Default.Save();
            }
        }

        //Fill the list view with default stuff and custom stuff.
        private void PopulateListViews()
        {
            //Get Maps
            foreach (KeyValuePair<string, string> kv in Defaults.defaultMaps)
            {
                var newItem = ListItem.MakeMapListItem(kv.Key);
                ListMaps.Items.Add(newItem);
            }

            var maps = GetMaps();
            foreach (string s in maps)
            {
                var newItem = ListItem.MakeMapListItem(s.Split('\\').Last());
                ListMaps.Items.Add(newItem);
            }

            foreach (KeyValuePair<string, string> kv in Defaults.defaultTypes)
            {
                var newItem = ListItem.MakeVariantListItem(kv.Key);
                ListVariants.Items.Add(newItem);
                newItem.Selected += (sender, r) => AdvancedGametypeWindow(maps, newItem);
            }

            //Get Gametypes
            var types = GetGametypes();
            foreach (string s in types)
            {
                var newItem = ListItem.MakeMapListItem(s.Split('\\').Last());
                ListVariants.Items.Add(newItem);
                newItem.Selected += (sender, r) => AdvancedGametypeWindow(maps, newItem);
            }
        }

        private bool CheckLocation()
        {
            if (modsPath == "" || !Directory.Exists($"{modsPath}/server"))
                return false;
            return true;
        }

        private void ShowLocateDialog()
        {
            modsPath = "";
            while (!CheckLocation())
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.IsFolderPicker = true;
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    modsPath = dlg.FileName;
                else
                    return;
                Settings.Default.modsPath = modsPath;
                Settings.Default.Save();

                PopulateListViews();
            }
        }

        private string[] GetMaps()
        {
            try
            {
                return Directory.GetDirectories($"{modsPath}/maps");
            }
            catch (IOException e)
            {
                //MessageBox.Show("Couldn't find any maps. You either have none or you selected the wrong place.");
                return new string[0];
            }
        }

        private List<string> GetGametypes()
        {
            try
            {
                return Directory.GetDirectories($"{modsPath}/variants").ToList();
            }
            catch (IOException e)
            {
                //MessageBox.Show("Couldn't find any variants. You either have none or you selected the wrong place.");
                return new List<string>();
            }
        }

        //Open the AdvancedGametype window and return information.
        private void AdvancedGametypeWindow(string[] maps, ListBoxItem boxItem)
        {
            var w = new MoreGametype(maps, boxItem.Content.ToString());
            if (w.ShowDialog() == false)
            {
                VotingTypes foo = w.typeStuff;
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

        private void OpenFTPWindow(object sender, RoutedEventArgs e)
        {
            var w = new FTPWindow();
            if (w.ShowDialog() == false)
            {
                var types = w.typesList;
                foreach (string s in types)
                    ListVariants.Items.Add(ListItem.MakeVariantListItem(s));
                var maps = w.mapsList;
                foreach (string s in maps)
                    ListMaps.Items.Add(ListItem.MakeVariantListItem(s));
            }
        }

        //Convert what's selected to json
        private void ConvertToJson()
        {
            if (ListMaps.SelectedItems.Count > 0)
                txt_Ouput.Text = MakeVotingJson();
            else
                txt_Ouput.Text = MakeVetoJson();          
        }

        //Make the veto json
        private string MakeVetoJson()
        {
            return JsonGenerator.MakeVetoJson(ListVariants.SelectedItems, types);
        }

        private string MakeVotingJson()
        {
            return JsonGenerator.MakeVotingJson(ListMaps.SelectedItems, ListVariants.SelectedItems, types);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertToJson();
        }

        private void RelocateButton(object sender, RoutedEventArgs e)
        {
            ListMaps.Items.Clear();
            ListVariants.Items.Clear();
            ShowLocateDialog();
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Show();
        }


    }
}
