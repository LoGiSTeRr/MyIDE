using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MyVisualStudio.Messages;
using MyVisualStudio.Enums;
namespace MyVisualStudio.ViewModels
{
    internal partial class MainViewModel : BaseViewModel
    {

        [ObservableProperty]
        private BaseViewModel? currentViewModel;

        private StartViewModel? startViewModel;

        private RedactorViewModel? redactorViewModel;



        public MainViewModel()
        {
            startViewModel = new StartViewModel();

            redactorViewModel = new RedactorViewModel();


            WeakReferenceMessenger.Default.Register<ChangeViewModelMessage<ViewModelEnum>>(this, ChangeView);
            CurrentViewModel = startViewModel;
        }

        private void ChangeView(object r, ChangeViewModelMessage<ViewModelEnum> viewModel)
        {
            CurrentViewModel = viewModel.Value switch
            {
                ViewModelEnum.RedactorViewModel => redactorViewModel,
                ViewModelEnum.StartViewModel => startViewModel,
                _ => throw new Exception("Wrong ViewModel")
            };
        }
    }
}
