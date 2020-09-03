using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

using Demo_MVVMBasic.Models;
using Demo_MVVMBasic.Data;
using System.Windows.Controls;

namespace Demo_MVVMBasic
{
    class MainWindowViewModel
    {
        public ICommand ButtonSellCommand { get; set; }
        public ICommand ButtonBuyCommand { get; set; }

        public List<Widget> Widgets { get; set; }
        public Widget SelectedWidget { get; set; }

        public MainWindowViewModel()
        {
            Widgets = WidgetData.GetAllWidgets();
            if (Widgets.Any()) SelectedWidget = Widgets[0];

            ButtonSellCommand = new RelayCommand(new Action<object>(SellWidgets));
            ButtonBuyCommand = new RelayCommand(new Action<object>(BuyWidgets));
        }

        public void SellWidgets(object parameter)
        {
            int.TryParse((string)parameter, out int quantity);
            SelectedWidget.CurrentInventory -= quantity;
        }

        public void BuyWidgets(object parameter)
        {
            int.TryParse((string)parameter, out int quantity);
            SelectedWidget.CurrentInventory += quantity;
        }
    }
}
