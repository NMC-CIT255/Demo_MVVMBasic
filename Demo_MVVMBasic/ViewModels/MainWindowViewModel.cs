using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

using Demo_MVVMBasic.Models;
using Demo_MVVMBasic.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Demo_MVVMBasic.Views;
using System.Net.NetworkInformation;

namespace Demo_MVVMBasic
{
    class MainWindowViewModel : ObservableObject
    {
        public ICommand ButtonSellCommand { get; set; }
        public ICommand ButtonBuyCommand { get; set; }
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonEditCommand { get; set; }
        public ICommand ButtonDeleteCommand { get; set; }
        public ICommand ButtonQuitCommand { get; set; }

        private Widget _selectedWidget;

        public ObservableCollection<Widget> Widgets { get; set; }

        public Widget SelectedWidget
        {
            get { return _selectedWidget; }
            set
            {
                _selectedWidget = value;
                OnPropertyChanged(nameof(SelectedWidget));
            }
        }


        public MainWindowViewModel()
        {
            Widgets = new ObservableCollection<Widget>(WidgetData.GetAllWidgets());

            if (Widgets.Any()) SelectedWidget = Widgets[0];

            ButtonSellCommand = new RelayCommand(new Action<object>(SellWidgets));
            ButtonBuyCommand = new RelayCommand(new Action<object>(BuyWidgets));
            ButtonAddCommand = new RelayCommand(new Action<object>(AddWidget));
            ButtonEditCommand = new RelayCommand(new Action<object>(EditWidget));
            ButtonDeleteCommand = new RelayCommand(new Action<object>(DeleteWidget));
            ButtonQuitCommand = new RelayCommand(new Action<object>(QuitWidget));
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

        public void AddWidget(object parameter)
        {
            //
            // create WidgetOperation object to pass
            // open add window
            //
            WidgetOperation widgetOperation = new WidgetOperation()
            {
                Status = WidgetOperation.OperationStatus.CANCEL,
                Widget = new Widget()
            };
            Window addWdigetWindow = new AddWindow(widgetOperation);
            addWdigetWindow.ShowDialog();

            //
            // TODO consider refactoring and use a class with the Widget object and status
            //
            if (widgetOperation.Status != WidgetOperation.OperationStatus.CANCEL)
            {
                Widgets.Add(widgetOperation.Widget);
            }
        }

        public void EditWidget(object parameter)
        {
            //
            // create WidgetOperation object to pass with the current SelectedWidget object
            // open add window
            //
            WidgetOperation widgetOperation = new WidgetOperation()
            {
                Status = WidgetOperation.OperationStatus.CANCEL,
                Widget = SelectedWidget
            };
            Window editWidgetWindow = new EditWindow(widgetOperation);
            editWidgetWindow.ShowDialog();

            //
            // TODO consider refactoring and use a class with the Widget object and status
            //
            if (widgetOperation.Status != WidgetOperation.OperationStatus.CANCEL)
            {
                Widgets.Remove(SelectedWidget);
                Widgets.Add(widgetOperation.Widget);
                SelectedWidget = widgetOperation.Widget;
            }
        }

        public void DeleteWidget(object parameter)
        {
            if (SelectedWidget != null)
            {
                string widgetName = SelectedWidget.Name;

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {widgetName} widgets from inventory?", "Delete Widgets", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Widgets.Remove(SelectedWidget);
                        MessageBox.Show($"{widgetName} Widgets Deleted", "Delete Widgets");

                        if (Widgets.Any()) SelectedWidget = Widgets[0];
                        break;

                    case MessageBoxResult.No:
                        MessageBox.Show($"{widgetName} Widgets Deletion Canceled", "Delete Widgets");
                        break;
                }
            }
        }

        public void QuitWidget(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
