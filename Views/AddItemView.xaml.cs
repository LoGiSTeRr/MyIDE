using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyVisualStudio.Messages;
using MyVisualStudio.Models;
using MyVisualStudio.Services;
using MyVisualStudio.ViewModels;

namespace MyVisualStudio.Views
{
    /// <summary>
    /// Interaction logic for RightClickOnItemView.xaml
    /// </summary>
    
    [INotifyPropertyChanged]
    public partial class AddItemView : Window
    {
        public AddItemView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<SendInfoForAddItem<FilesDirectoryService, Folder>>(this, forConstructor);
            DataContext = this;
        }

        Folder folder;

        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        string extension;

        FilesDirectoryService directoryService;

        private void forConstructor(object r, SendInfoForAddItem<FilesDirectoryService, Folder> data)
        {
            directoryService = data.Value1;
            folder = data.Value2;
        }

        [ICommand]
        void Create()
        {
            if (extension == "folder")
            {
                directoryService.AddFolder(new Folder(folder.Path) { Name = fileName });
                this.Close();
                return;
            }
            directoryService.AddFile(new MyFile() { Path = folder.Path, Name = FileName + extension});
            this.Close();
        }
    }
}
