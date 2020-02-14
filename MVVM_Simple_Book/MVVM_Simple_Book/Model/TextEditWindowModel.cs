using Microsoft.Win32;
using MVVM_Simple_Book.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_Simple_Book.Model
{
    class TextEditWindowModel : TextEditWndowViewModel
    {
        private string f_text;

        public string Text
        {
            get => f_text;
            set => Set(ref f_text, value);
        }

        private string f_FileName;

        public string FileName
        {
            get => f_FileName;
            set
            {
                if (Set(ref f_FileName, value))
                {
                    ReadFileAsync(value);
                }
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand QuitCommand { get; } = new LambdaCommand(p => Application.Current.Shutdown());

        public TextEditWindowModel()
        {
            CreateCommand = new LambdaCommand(OnCreateComandExecuted);
            SaveCommand = new LambdaCommand(OnSaveComandExecutedAsync, OnSaveCommandCanExecute);
        }

        private bool OnSaveCommandCanExecute(object FilePath)
        {
            //return f_text != null;
            return !string.IsNullOrEmpty(f_text);
        }

        private async void OnSaveComandExecutedAsync(object FilePath)
        {
            var file_name = FilePath as string;
            if (file_name == null)
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Save file ...",
                    Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*",
                    InitialDirectory = Environment.CurrentDirectory,
                    RestoreDirectory = true
                };
                if (dialog.ShowDialog() != true) return;
                file_name = dialog.FileName;
            }
            using (var writer = new StreamWriter(new FileStream(file_name, FileMode.Create, FileAccess.Write)))
                await writer.WriteAsync(f_text).ConfigureAwait(true);
            FileName = file_name;
        }

        private void OnCreateComandExecuted(object obj)
        {
            Text = "";
            FileName = null;
        }

        private async void ReadFileAsync(string FilePath)
        {
          Text = "";
            if (!File.Exists(FilePath)) return;
            using (var reader = File.OpenText(FilePath))
                Text = await reader.ReadToEndAsync().ConfigureAwait(true);
        }
    }
}
