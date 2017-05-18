using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfTask.Annotations;
using WpfTask.Helpers;
using WpfTask.Models;
using Timer = System.Threading.Timer;

namespace WpfTask.ViewModels
{
    class Task5ViewModel 
    {
        
        public RelayCommand GenerateCommand { get; set; }
        public RelayCommand DequeueCommand { get; set; }
        public int EndNumber { get; set; }
        public int Count { get; set; }
        public ObservableQueue<int> Queue { get; set; }

        public Task5ViewModel()
        {
            Queue = new ObservableQueue<int>();
            Queue.Enqueue(1);
            Queue.Enqueue(2);
            
            GenerateCommand = new RelayCommand(GetGenerate);
            DequeueCommand = new RelayCommand(Dequeue);
        }

        private async void Dequeue(object obj)
        {
            if (Queue.Count()<=1)
            {
                await LoadDataFromBCL();
                Queue.Dequeue();
            }
            else
            {
                Queue.Dequeue();
            }
        }

        private async void GetGenerate(object obj)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadDataFromUrl();
            }
            catch (Exception e)
            {
                LoadDataFromBCL();
            }
        }

        private void LoadDataFromUrl()
        {
            List<int> numbers = new List<int>();
            string url = $"https://www.random.org/integers/?num={Count}&min=0&max={EndNumber}&col=1&base=10&format=plain&rnd=new";

            WebClient webClient = new WebClient();
            string data = webClient.DownloadString(url);
            

            numbers = data.Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();//Parse values

            FillQueue(numbers);//Fill queue from arr
        }

        private async Task LoadDataFromBCL()
        {
            List<int> values = new List<int>();
            var randEndNumber = new Random();
            

            for (int i = 0; i < Count; i++)
            {
                var endNumber = randEndNumber.Next(0, EndNumber);

                values.Add(endNumber);
            }
            

            FillQueue(values);//Fill queue from list
        }

        private void FillQueue(List<int> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Queue.Enqueue(lst[i]);
            }
        }
    }
}
