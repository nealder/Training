using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfo.Model
{
    class EmployeeManager : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> _employee;
        public ObservableCollection<Employee> Employee
        {
            get { return _employee; }

            set
            {
                _employee = value;
            }
        }

        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
