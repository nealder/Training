using Hetfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfo.ViewModel
{
    class EmployeeDataConverter
    {
        Employee temp_emp;

        public EmployeeDataConverter(Employee con_temp_emp)
        {
            temp_emp = con_temp_emp;
        }

        public string GetFullName()
        {
            return (temp_emp.First_name + " " + temp_emp.Last_name);
        }

        public int GetAge()
        {
            int years = DateTime.Now.Year - temp_emp.BirthDate.Year;

            if (temp_emp.BirthDate.Month == DateTime.Now.Month && DateTime.Now.Day < temp_emp.BirthDate.Day)
            {
                years--;
            }
            else if (DateTime.Now.Month < temp_emp.BirthDate.Month)
            {
                years--;
            }
            return years;
        }

        public bool IsAdult()
        {
            return temp_emp.Age >= 18 ? true : false;
        }

        public String FormatedBirthdate()
        {
            return temp_emp.BirthDate.ToShortDateString();
        }
    }
}
