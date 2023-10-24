using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MyVisualStudio.Models;

namespace MyVisualStudio.Services
{
    internal class FilesDirectoryService
    {
        FilesRepository fileRepos;
        string mainPath;
        public FilesDirectoryService(FilesRepository _fileRepos, string projectPath)
        {
            mainPath = projectPath;
            fileRepos = _fileRepos;

        }
        public void AddFolder(Folder folder)
        {
            Directory.CreateDirectory(Path.Combine(folder.Path, folder.Name));
            UpdateUI();
        }
        public void AddFile(MyFile file)
        {
            var myFile = File.Create(Path.Combine(file.Path, file.Name));
            myFile.Close();
            File.WriteAllText(Path.Combine(file.Path,file.Name), file.Text);
            UpdateUI();
        }
        public void DeleteFile(MyFile file)
        {
            File.Delete(file.Path);
            UpdateUI();
        }
        public void DeleteFolder(Folder folder)
        {
            try
            {
                Directory.Delete(folder.Path);
            }
            catch (Exception)
            {
                return;
            }
            UpdateUI();
        }
        private void UpdateUI()
        {
            fileRepos.Folders.Clear();
            fileRepos.Folders.Add(new Folder(mainPath) { Name = "UserProject" });
        }
    }
}
