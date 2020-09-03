using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_MVVMBasic;

namespace Demo_MVVMBasic.Models
{
    public class Widget : ObservableObject
    {
        private int _name;
        public string  Name { get; set; }
        public string Color { get; set; }
        public double UnitPrice { get; set; }

        public int CurrentInventory
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(CurrentInventory));
            }
        }


    }
}
