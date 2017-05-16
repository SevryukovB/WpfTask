using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ExtensionMethods;
using WpfTask.Helpers;
using WpfTask.Models;

namespace WpfTask.ViewModels
{
    
    class Task1ViewModel
    {
        public ObservableCollection<Person> People { get; set; }
        public RelayCommand CloneCommand { get; set; }


        public Task1ViewModel()
        {
            People = new ObservableCollection<Person>
            {
                new Person { FirstName="Tom", LastName="Jones", Age=80 },
                new Person { FirstName="Dick", LastName="Tracey", Age=40 },
                new Person { FirstName="Harry", LastName="Hill", Age=60 },
            };
            
            CloneCommand = new RelayCommand(Clone);
        }

        private void Clone(object parameter)
        {
            var p = parameter.CloneObject<Person>();

            foreach (Person value in (IEnumerable)p)
            {
                People.Add(new Person() { FirstName = value.FirstName, LastName = value.LastName, Age = value.Age});
            }
        }


    }
}