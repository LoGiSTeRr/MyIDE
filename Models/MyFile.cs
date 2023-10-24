using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MyVisualStudio.Models
{
    [INotifyPropertyChanged]
    internal partial class MyFile
    {
        [ObservableProperty]
        string text;

        [ObservableProperty]
        string name;

        public string Path { get; set; }

    }
}
