using System;
using System.Collections.Generic;
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
            int size1 = random.Next(200) + 30;
            int left1 = random.Next(containerWidth - 2 * size1) + size1;
            int top1 = random.Next(containerHeight - 2 * size1) + size1;

            int size2 = random.Next(200) + 30;
            int left2 = left1;
            int top2 = top1;
            do
            {
                size2 = random.Next(200) + 30;
                left2 = random.Next(containerWidth - 2 * size2) + size2;
                top2 = random.Next(containerHeight - 2 * size2) + size2;
            }
            while (((left1 + size1 < left2) || (top1 + size1 < top2) || (left2 + size2 < left1) || (top2 + size2 < top1)));

            viewModel.Button1.Left = left1;
            viewModel.Button1.Top = top1;
            viewModel.Button1.Size = size1;

            viewModel.Button2.Left = left2;
            viewModel.Button2.Top = top2;
            viewModel.Button2.Size = size2;
        }
    }
}
