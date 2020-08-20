using System;

namespace BasicPlainTextReaderApp.Library
{
    public class TextModel
    {
        public string Text { get; }
        public string DataString { get; }
        public string Type { get; }
        public string DataPath { get; }

        public TextModel(string txt, string dt, string t, string dp)
        {
            Text = txt ?? "_null_";
            DataString = dt ?? "_null_";
            Type = t ?? "_null_";
            DataPath = dp ?? "_null";
        }
    }
}
