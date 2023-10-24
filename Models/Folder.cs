using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MyVisualStudio.Models
{
    [INotifyPropertyChanged]
    internal partial class Folder
    {
        [ObservableProperty]
        string name;
        public string Path { get; set; }

        public List<MyFile> Files;

        public List<Folder> Folders;

        public Folder(string _path)
        {
            Path = _path; 
            Files = new List<MyFile>();
            Folders = new List<Folder>();
            updateChildren();
        }
        private void updateChildren()
        {
            foreach (string directory in Directory.GetDirectories(Path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                Folders.Add(new Folder(Path + @"\" + directoryInfo.Name) { name = directoryInfo.Name} );
            }
            foreach (string file in Directory.GetFiles(Path))
            {
                FileInfo fileInfo = new FileInfo(file);
                Files.Add(new MyFile() { Name = fileInfo.Name, Path = fileInfo.FullName, Text = File.ReadAllText(file) });
            }
        }

        public System.Collections.IList Children
        {
            get
            {
                return new CompositeCollection()
                {
                    new CollectionContainer() { Collection = Files},
                    new CollectionContainer() { Collection = Folders}
                };

            }
        }
    }
}
