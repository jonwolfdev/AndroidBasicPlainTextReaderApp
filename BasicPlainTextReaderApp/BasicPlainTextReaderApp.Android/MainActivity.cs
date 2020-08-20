using System;

using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Provider;
using System.Text;

namespace BasicPlainTextReaderApp.Droid
{
    [Activity(Label = "Basic Plain Text Reader", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"*/*")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string action = Intent.Action;
            string type = Intent.Type;
            StringBuilder sb = null;

            if(Intent.ActionView.Equals(action) && !string.IsNullOrEmpty(type))
            {
                Android.Net.Uri fileUri = Intent.Data;
                if (fileUri != null)
                {
                    try
                    {
                        using (var parcelFileDescriptor = ContentResolver.OpenFileDescriptor(Intent.Data, "r"))
                        using (Java.IO.FileDescriptor fileDescriptor = parcelFileDescriptor.FileDescriptor)
                        using (var reader = new Java.IO.FileReader(fileDescriptor))
                        using (var bufferedReader = new Java.IO.BufferedReader(reader))
                        {
                            string line;
                            sb = new StringBuilder();
                            while ((line = bufferedReader.ReadLine()) != null)
                            {
                                sb.AppendLine(line);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sb = new StringBuilder();
                        sb.AppendLine(e.ToString());
                    }
                }
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(sb.ToString()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}