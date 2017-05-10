using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ExtensionMethods;
using WpfTask.Annotations;
using WpfTask.Helpers;
using WpfTask.Models;

namespace WpfTask.ViewModels
{
    public class Task2ViewModel
    {
        public static TaskCommand Start { get; set; }
        private List<Person> p;

        public Task2ViewModel()
        {
            p = new List<Person>()
            {
                new Person { FirstName="Tom", LastName="Jones", Age=80 },
                new Person { FirstName="Dick", LastName="Tracey", Age=40 },
                new Person { FirstName="Harry", LastName="Hill", Age=60 },
            };

            Start = new TaskCommand(StartMethods);
        }



        
        public void ActionMethod()
        {
            MessageBox.Show("Method with action");
        }

        public void ActionTMethod()
        {
            MessageBox.Show("Method with Action<Task>");
        }

        public void FuncMethod<T>() where T : Person, new ()
        {
            T t = new T();
            t.FirstName = "Hey";
            MessageBox.Show("Method with Func<T>" + t.FirstName);
        }

        public void FuncMethod<Task,T>() where T : Person, new()
        {
            T t = new T();
            t.FirstName = "Hey";
            MessageBox.Show("Method with Func<Task, T> " + t.FirstName);
        }

        public async Task Method(Action a)
        {
             a();
        }

        public async Task Method(Action<Task> a)
        {
            Task t = new Task(() =>
            {
                //something here
            });
            a(t);
        }

        public async Task Method<T>(Func<T> a)
        {
            a();
        }

        public async Task Method<T>(Func<Task, T> a)
        {
            Task t = new Task(() =>
            {
                //something here
            });
            a(t);
        }
        


        public async void StartMethods()
        {
            await Method(ActionMethod);

            await Method(ActionTMethod);

            await Method(FuncMethod<Person>);

            await Method(FuncMethod<Person>);
        }
    }
}
