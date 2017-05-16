﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfTask.Helpers;

namespace WpfTask.ViewModels
{
    class Task3ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> SomeCollection { get; set; }
        public RelayCommand StartCommand { get; set; }

        static Mutex mutexObj = new Mutex();
        static int x = 0;

        public Task3ViewModel()
        {
            SomeCollection = new ObservableCollection<string>();
            StartCommand = new RelayCommand(SomeM);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void SomeM(object obj)
        {
            await kek();
            //for (int i = 0; i < 10000; i++)
            //{
            //    SomeCollection.Add("asd "+ i);
            //    //Thread myThread = new Thread(Count);
            //    //myThread.Name = "Поток " + i.ToString();
            //    //myThread.Start();
            //}
        }

        public async Task  kek()
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < 10000; i++)
                {
                    Thread.Sleep(100);
                    App.Current.Dispatcher.Invoke(
                        delegate
                        {
                            SomeCollection.Add("asd " + i.ToString());
                        });
                }
                
            });
            //for (int i = 0; i < 10000; i++)
            //{
            //     SomeCollection.Add("asd " + i);
            //    //Thread myThread = new Thread(Count);
            //    //myThread.Name = "Поток " + i.ToString();
            //    //myThread.Start();
            //}

        }
        

        public async void Count()
        {
            mutexObj.WaitOne();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                SomeCollection.Add($"{Thread.CurrentThread.Name}: {x}");
                //Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
            mutexObj.ReleaseMutex();
        }
    }
}