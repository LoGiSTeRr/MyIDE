using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyVisualStudio.Messages;
using MyVisualStudio.Models;
using MyVisualStudio.Services;
using MyVisualStudio.Views;
using System.Diagnostics;

namespace MyVisualStudio.ViewModels
{
    internal partial class RedactorViewModel : BaseViewModel
    {
        [ObservableProperty]
        FilesRepository fileRepos;

        [ObservableProperty]
        bool isAddItemButtonEnabled;

        string textOfRedactor;
        string mainPath;
        private MyFile currentFile;

        FilesDirectoryService directoryService;

        private AddItemView addItemView;
        public RedactorViewModel()
        {
            isAddItemButtonEnabled = false;
            FileRepos = new FilesRepository();
            addItemView = new AddItemView();
            WeakReferenceMessenger.Default.Register<SendPathMessage>(this, GetPath);
        }

        private void GetPath(object r, SendPathMessage data)
        {
            mainPath = data.Value;
            directoryService = new FilesDirectoryService(FileRepos, mainPath);

            Process p = new();
            p.StartInfo.FileName = "dotnet";
            p.StartInfo.Arguments = $"new console --output {mainPath}";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
        }


        [ICommand]
        void Open(MyFile file)
        {
            WeakReferenceMessenger.Default.Send(new SendTextEditorStringMessage(file));
            currentFile = file;
        }

        [ICommand]
        void Delete(object obj)
        {
            if (obj != null)
            {
                if (obj is Folder)
                {
                    directoryService.DeleteFolder(obj as Folder);
                }
                else if(obj is MyFile)
                {
                    directoryService.DeleteFile(obj as MyFile);
                }
            }
        }

        [ICommand]
        void AddItem(Folder folder)
        {
            WeakReferenceMessenger.Default.Send(new SendInfoForAddItem<FilesDirectoryService, Folder>(directoryService, folder));
            addItemView.ShowDialog();
            addItemView = new AddItemView();
        }

        [ICommand]
        void Save()
        {
            File.WriteAllText(currentFile.Path, currentFile.Text);
        }

        [ICommand]
        void Compile()
        {
            var a = Process.Start("dotnet", $"msbuild {mainPath}");
            a.WaitForExit();
            Process.Start( Path.Combine(mainPath,$"bin\\Debug\\net6.0\\{mainPath.Substring(mainPath.LastIndexOf(@"\"))}.exe"));
        }

    }
}
