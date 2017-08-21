using Hetfo.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace Hetfo.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Employee> ActualListOfEmployees { get; set; }
        
        private CollectionViewSource usersCollection;

        public MainViewModel()
        {
            ActualListOfEmployees = new ObservableCollection<Employee>();

            usersCollection = new CollectionViewSource();
            usersCollection.Source = ActualListOfEmployees;
            usersCollection.Filter += new FilterEventHandler(Collection_Filter_Adult);
            usersCollection.Filter += new FilterEventHandler(Collection_Filter_Age);
            usersCollection.Filter += new FilterEventHandler(Collection_Filter_Birthdate);
            usersCollection.Filter += new FilterEventHandler(Collection_Filter_Fullname);
        }

        public void Open_xml_click_handler(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "XML files (*.XML)|*.XML|All files (*.*)|*.*",
                InitialDirectory = "c\\Users/dbencs/source/repos/Hetfo/Hetfo/Data",
                RestoreDirectory = true,
                Multiselect = false,
                
            };

            bool? userClickedOK = openFileDialog.ShowDialog();

            if ( openFileDialog != null)
            {
                if (userClickedOK == true)
                {
                    if (!String.IsNullOrEmpty(openFileDialog.FileName) && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {

                        if (new System.IO.FileInfo(openFileDialog.FileName).Length > 0)
                        {
                            try
                            {
                                XDocument xmlDocument = XDocument.Load(openFileDialog.FileName);
                                var employees = from x in xmlDocument.Descendants("Employee")
                                                select new
                                                {
                                                    FirstName = x.Element("firstname").Value,
                                                    LastName = x.Element("lastname").Value,
                                                    Birthdate = x.Element("birthdate").Value,
                                                };

                                if ( employees.Any() )
                                {
                                    int i = 0;
                                    foreach (var temp in employees)
                                    {
                                        ActualListOfEmployees.Add(new Employee
                                        {
                                            First_name = temp.FirstName.ToString(),
                                            Last_name = temp.LastName.ToString(),
                                            BirthDate = DateTime.Parse(temp.Birthdate.ToString(), new CultureInfo("en-US")),
                                        });
                                        ActualListOfEmployees[i].Fullname = new EmployeeDataConverter(ActualListOfEmployees[i]).GetFullName();
                                        ActualListOfEmployees[i].Age = new EmployeeDataConverter(ActualListOfEmployees[i]).GetAge();
                                        ActualListOfEmployees[i].Adult = new EmployeeDataConverter(ActualListOfEmployees[i]).IsAdult();
                                        ActualListOfEmployees[i].FormattedBirthDate = new EmployeeDataConverter(ActualListOfEmployees[i]).FormatedBirthdate();
                                        i++;
                                    } 
                                }
                            }
                            catch (XmlException Exception)
                            {
                                throw new Exception("The file is not in a correct XML format", Exception);
                            }

 
                        }else { throw new Exception("File is empty!"); }
                    }
                    else { throw new Exception("The name of the file is incorrect.(Null/Empty/[only]Whitespace[s])"); } 
                }
            }else { throw new Exception("The file dialog panel could not be initialized!"); }
            
        }

        public ICollectionView SourceCollection
        {
            get
            {
                return this.usersCollection.View;
            }
        }

        private string filterTextFullName;
        public string FilterTextFullName
        {
            get
            {
                return filterTextFullName;
            }
            set
            {
                filterTextFullName = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("FilterTextFullName");
                System.Diagnostics.Debug.WriteLine("Fullname filter text refresshed ...");
            }
        }

        private string filterTextAge;
        public string FilterTextAge
        {
            get
            {
                return filterTextAge;
            }
            set
            {
                filterTextAge = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("filterTextAge");
            }
        }

        private string filterTextBirthdate;
        public string FilterTextBirthdate
        {
            get
            {
                return filterTextBirthdate;
            }
            set
            {
                filterTextBirthdate = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("filterTextBirthdate");
            }
        }

        private string filterTextAdult;
        public string FilterTextAdult
        {
            get
            {
                return filterTextAdult;
            }
            set
            {
                filterTextAdult = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("filterTextAdult");
            }
        }

        void Collection_Filter_Fullname(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextFullName))
            {
                e.Accepted = true;
                return;
            }

            Employee usr = e.Item as Employee;
            if (usr.Fullname.ToUpper().Contains(FilterTextFullName.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        void Collection_Filter_Age(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextAge))
            {
                e.Accepted = true;
                return;
            }

            Employee usr = e.Item as Employee;
            if (usr.Age.ToString().ToUpper().Contains(FilterTextAge.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        void Collection_Filter_Birthdate(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextBirthdate))
            {
                e.Accepted = true;
                return;
            }

            Employee usr = e.Item as Employee;
            if (usr.BirthDate.ToString().ToUpper().Contains(FilterTextBirthdate.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        void Collection_Filter_Adult(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextAdult))
            {
                e.Accepted = true;
                return;
            }

            Employee usr = e.Item as Employee;
            if (usr.Adult.ToString().ToUpper().Contains(FilterTextAdult.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
