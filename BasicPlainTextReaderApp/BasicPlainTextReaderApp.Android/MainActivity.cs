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
using BasicPlainTextReaderApp.Library;
using Java.Interop;
using Android.Util;
using Java.IO;

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
            TextModel data = null;
            
            if (Intent.ActionView.Equals(Intent.Action) && Intent.Data != null && !string.IsNullOrEmpty(Intent.Type))
            {
                try
                {
                    using (var parcelFileDescriptor = ContentResolver.OpenFileDescriptor(Intent.Data, "r"))
                    using (Java.IO.FileDescriptor fileDescriptor = parcelFileDescriptor.FileDescriptor)
                    using (var reader = new Java.IO.FileReader(fileDescriptor))
                    using (var bufferedReader = new Java.IO.BufferedReader(reader))
                    {
                        string line;
                        var sb = new StringBuilder();
                        while ((line = bufferedReader.ReadLine()) != null)
                        {
                            sb.AppendLine(line);
                        }
                        data = new TextModel(sb.ToString(), Intent.DataString, Intent.Type, Intent.Data.Path);
                    }
                }
                catch (Exception e)
                {
                    data = new TextModel(e.ToString(), Intent.DataString, Intent.Type, Intent.Data.Path);
                }
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(data));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Export("OpenTextFile")]
        public void OpenTextFile()
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test.txt");
            if (!System.IO.File.Exists(backingFile))
            {
                using (var writer = System.IO.File.CreateText(backingFile))
                {
                    writer.WriteLine("Test file\r\nTest file line 2");
                    writer.Flush();
                }
            }

            //Read file
            //using (var reader = new StreamReader(backingFile, true))
            //{
            //    string line;
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //    }
            //}

            var uri = Android.Net.Uri.Parse("file:///data/user/0/dev.jonwolf.basicplaintextreaderapp/files/test.txt");
            var intent2 = new Intent(Intent.ActionView, uri, this, typeof(MainActivity));
            
            intent2.SetDataAndType(uri, "text/plain");
            intent2.SetAction(Intent.ActionView);
            
            StartActivity(intent2);
        }
    }
}