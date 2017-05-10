using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfTask.Annotations;
using WpfTask.Helpers;
using WpfTask.Models;

namespace WpfTask.ViewModels
{
    class Task5ViewModel : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public RelayCommand Generate { get; set; }
        public string EndNumber { get; set; }
        public string Count { get; set; }
        public Queue Queue { get; set;  }

        public Task5ViewModel()
        {
            Queue = new Queue(){ };
            Queue.Enqueue(1);
            Queue.Enqueue(2);
            Queue.Enqueue(3);
            Queue.Enqueue(4);
            Generate = new RelayCommand(GetGenerate);
        }

        private void GetGenerate(object obj)
        {
            string url = $"https://www.random.org/integers/?num={Count}&min=0&max={EndNumber}&col=1&base=10&format=plain&rnd=new";
            var data = LoadData(url);

            string[] subStrings = data.Split('\n');

            FillQueue(subStrings);

        }

        private string LoadData(string url)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString(url);
        }

        private void FillQueue(string[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int item = Convert.ToInt32(arr[i]);
                Queue.Enqueue(item);
            }

            foreach (var item in arr)
            {

                Queue.Enqueue(item);
                CollectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, item));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
