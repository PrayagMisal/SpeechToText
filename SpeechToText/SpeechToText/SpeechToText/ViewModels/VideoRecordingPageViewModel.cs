using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpeechToText.ViewModels
{
    public class VideoRecordingPageViewModel : BaseViewModel
    {
        public ICommand TakeVideoCommand { get; }
        public VideoRecordingPageViewModel()
        {
            TakeVideoCommand = new Command(TakeVideoCommandMethod);
        }

        #region Command Methods
        private async void TakeVideoCommandMethod(object obj)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Directory = "Sample",
                Name = "test.mp4"
            });

            if (file != null)
            {
                await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
            }
        }
        #endregion
    }
}
