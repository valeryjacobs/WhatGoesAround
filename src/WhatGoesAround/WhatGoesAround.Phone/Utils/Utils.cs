using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatGoesAround.Phone.ViewModels;

namespace WhatGoesAround.Phone.Utils
{
    public static class Utils
    {
        static Random random = new Random();
        public static void GetRandomPositions(MainViewModel viewModel, int containerWidth, int containerHeight)
        {
            int maxsize = Math.Min(containerHeight, containerWidth) / 4;
            int minsize = Convert.ToInt32(maxsize * 0.7);
            int size1 = random.Next(minsize, maxsize);
            int left1 = random.Next(size1, containerWidth - size1);
            int top1 = random.Next(size1, containerHeight - size1);

            int size2 = 0;
            int left2 = 0;
            int top2 = 0;
            do
            {
                size2 = random.Next(minsize, maxsize);
                left2 = random.Next(size2, containerWidth - size2);
                top2 = random.Next(size2, containerHeight - size2);
            }
            while (!((left1 + size1 < left2) || (top1 + size1 < top2) || (left2 + size2 < left1) || (top2 + size2 < top1)));

            viewModel.Button1.Left = left1;
            viewModel.Button1.Top = top1;
            viewModel.Button1.Size = size1;

            viewModel.Button2.Left = left2;
            viewModel.Button2.Top = top2;
            viewModel.Button2.Size = size2;
        }

        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }

        public static void SortDescending<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderByDescending(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}
