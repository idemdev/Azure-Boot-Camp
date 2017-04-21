using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.Capture;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;


namespace MyMood
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        IRandomAccessStream imageStream;

        const string APIKEY = "07878b414b404f54a49a3452d6aa9787";
        EmotionServiceClient emotionServiceClient = new EmotionServiceClient(APIKEY);
        Emotion[] emotionResult;

        public MainPage()
        {
            this.InitializeComponent();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
        }

        private async void takephoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if(photo == null)
                {
                    //user cancelled photo
                    return;
                }
                else
                {
                    imageStream = await photo.OpenAsync(FileAccessMode.Read);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
                    SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                    await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                    image.Source = bitmapSource;
                }
            }
            catch
            {
                output.Text = "Error taking photo";
            }
        }

        private async void getemotion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emotionResult = await emotionServiceClient.RecognizeAsync(imageStream.AsStream());
                if(emotionResult != null)
                {
                    Microsoft.ProjectOxford.Common.Contract.EmotionScores score = emotionResult[0].Scores;
                    output.Text = "Your emotions are: \n" +
                        "Happiness: " + score.Happiness.ToString("0.00000") + "\n" +
                        "Sadness: " + score.Sadness.ToString("0.00000") + "\n" +
                        "Surprise: " + score.Surprise.ToString("0.00000") + "\n" +
                        "Fear: " + score.Fear.ToString("0.00000") + "\n" +
                        "Anger: " + score.Anger.ToString("0.00000") + "\n" +
                        "Disgust: " + score.Disgust.ToString("0.00000") + "\n" +
                        "Neutral: " + score.Neutral.ToString("0.00000") + "\n";
                }
            }
            catch
            {
                output.Text = "Error calling emotion";
            }
        }
    }
}
