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

namespace Demo_MVVMBasic
{
    class AddWindowViewModel
    {
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }

        public Widget NewWidget { get; set; }

        public AddWindowViewModel()
        {
            //NewWidget = new Widget();

            //ButtonAddCommand = new RelayCommand(new Action<object>(AddWidget));
            //ButtonCancelCommand = new RelayCommand(new Action<object>(CancelAddWidget));
        }

        public AddWindowViewModel(Widget newWidget)
        {
            NewWidget = newWidget;

            ButtonAddCommand = new RelayCommand(new Action<object>(AddWidget));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelAddWidget));
        }

        public void AddWidget(object parameter)
        {
            // validate user inputs

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

        public void CancelAddWidget(object parameter)
        {
            NewWidget = null;

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

    }
}
