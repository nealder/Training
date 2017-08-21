using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfo.Model
{
    class Employee : INotifyPropertyChanged
    {
        private string _first_name;
        public string First_name
        {
            get { return _first_name; }
            set
            {
                _first_name = value;
                NotifyPropertyChanged("First_name");
            }
        }

        private string _last_name;
        public string Last_name
        {
            get { return _last_name; }
            set
            {
                _last_name = value;
                NotifyPropertyChanged("Last_name");
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                NotifyPropertyChanged("Age");
            }
        }

        private DateTime _birthdate;
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set
            {
                _birthdate = value;
                NotifyPropertyChanged("Birthdate");
            }
        }

        private String _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set
            {
                _fullname = value;
                NotifyPropertyChanged("Fullname");
            }
        }

        private bool _adult;
        public bool Adult
        {
            get { return _adult; }
            set
            {
                _adult = value;
                NotifyPropertyChanged("Adult");
            }
        }

        private String _formattedBirthDate;
        public String FormattedBirthDate
        {
            get { return _formattedBirthDate; }
            set
            {
                _formattedBirthDate = value;
                NotifyPropertyChanged("ForamttedBirthDate");
            }
        }

        void NotifyPropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
