using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MyVisualStudio.Models
{
    [INotifyPropertyChanged]
    internal partial class FilesRepository
    {
        [ObservableProperty]
        ObservableCollection<Folder> folders;
        public FilesRepository()
        {
            Folders = new ObservableCollection<Folder>();
        }
    }
}
