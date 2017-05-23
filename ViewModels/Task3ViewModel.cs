using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfTask.Helpers;
using WpfTask.Interfaces;

namespace WpfTask.ViewModels
{
    class Task3ViewModel
    {
        public ObservableCollection<string> SomeCollection { get; set; }
        public RelayCommand StartCommand { get; set; }

        static Mutex mutexObj = new Mutex();
        static int x = 0;

        public Task3ViewModel()
        {
            SomeCollection = new ObservableCollection<string>();
            SomeCollection.Add("keks");
            StartCommand = new RelayCommand(SomeM);
        }

        public void SomeM(object s)
        {
            for (int i = 0; i < 5; i++)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Thread myThread = new Thread(Count);
                    myThread.Name = "Поток " + i.ToString();
                    myThread.Start();
                });
            }
        }

        public async void Count()
        {
            await Lock();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                App.Current.Dispatcher.Invoke((Action) delegate
                {
                    SomeCollection.Add(Thread.CurrentThread.Name + x.ToString());
                });
                x++;
            }
            await Release();
        }

        public async Task Lock()
        {
            mutexObj.WaitOne();
        }

        public async Task Release()
        {
            mutexObj.ReleaseMutex();
        }
    }
}
