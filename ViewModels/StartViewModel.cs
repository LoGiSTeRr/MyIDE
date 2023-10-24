using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using MyVisualStudio.Messages;
using MyVisualStudio.Enums;

namespace MyVisualStudio.ViewModels
{
    internal partial class StartViewModel : BaseViewModel
    {
        [ICommand]
        void CreateNewProject()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WeakReferenceMessenger.Default.Send(new SendPathMessage(dialog.SelectedPath));

                }
                else
                {
                    return;
                }
            }
            WeakReferenceMessenger.Default.Send(new ChangeViewModelMessage<ViewModelEnum>(ViewModelEnum.RedactorViewModel));
        }
    }
}
