using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;

namespace CAMTEST.Media
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePhotoPage : ContentPage
    {
        public TakePhotoPage()
        {
            InitializeComponent();
        }
        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {

            var crossmedia = CrossMedia.Current;
            if (crossmedia.IsCameraAvailable && crossmedia.IsTakePhotoSupported)
            {

                var file = await crossmedia.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Name = DateTime.Now.Millisecond.ToString() + ".jgp",
                    Directory = "Pictures"

                });
                await DisplayAlert("Alert!", file.Path, "ok");
                myImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();


                    return stream;
                });

            }
            else
            {
                await DisplayAlert("Alert!", "Camera is not available", "ok");
            }
        }
    }

}