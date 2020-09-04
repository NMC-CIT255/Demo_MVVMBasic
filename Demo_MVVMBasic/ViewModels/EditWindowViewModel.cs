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
    class EditWindowViewModel
    {
        public ICommand ButtonSaveCommand { get; set; }
        public ICommand ButtonCancelCommand { get; set; }

        public Widget UserWidget { get; set; }

        public EditWindowViewModel()
        {

        }

        public EditWindowViewModel(Widget newWidget)
        {
            UserWidget = newWidget;

            ButtonSaveCommand = new RelayCommand(new Action<object>(EditWidget));
            ButtonCancelCommand = new RelayCommand(new Action<object>(CancelEditWidget));
        }

        public void EditWidget(object parameter)
        {
            // validate user inputs


            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }

        public void CancelEditWidget(object parameter)
        {
            UserWidget.Name = "CANCEL";

            if (parameter is System.Windows.Window)
            {
                (parameter as System.Windows.Window).Close();
            }
        }
    }
}
