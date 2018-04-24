using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PressJsonToJson.Objects
{
    public static class ListItem
    {
        public static ListBoxItem MakeMapListItem(string name)
        {
            var newItem = new ListBoxItem();
            newItem.Content = name;
            return newItem;
        }

        public static ListBoxItem MakeVariantListItem(string name)
        {
            var newItem = new ListBoxItem();
            newItem.Content = name;
            return newItem;
        }
    }
}
