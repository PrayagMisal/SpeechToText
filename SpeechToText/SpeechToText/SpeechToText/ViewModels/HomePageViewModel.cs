using SpeechToText.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechToText.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private ISpeechToText _speechRecongnitionInstance;

        public string Message { get; set; } = "Speech will appear here...";
        public ObservableCollection<string> TextRecordingItems { get; set; }
        public ICommand MicCommand { get; }

        public HomePageViewModel()
        {
            MicCommand = new Command(MicCommandMethod);
            TextRecordingItems = new ObservableCollection<string>();
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }
        #region Methods
        private void SpeechToTextFinalResultRecieved(string args)
        {
            Message = args;
            TextRecordingItems.Add(args);
        }
        #endregion

        #region Command Methods
        private void MicCommandMethod(object obj)
        {
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
