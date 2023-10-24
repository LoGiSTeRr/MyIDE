using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using RoslynPad.Editor;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using MyVisualStudio.Messages;
using MyVisualStudio.Models;

namespace MyVisualStudio.Views
{
    /// <summary>
    /// Interaction logic for RedactorView.xaml
    /// </summary>
    public partial class RedactorView : UserControl
    {
        private RoslynHost? _host;
        private MyFile currentFile;
        public RedactorView()
        {
            InitializeComponent();
            TextRedactor.Text = "//Choose File in solution explorer";
            WeakReferenceMessenger.Default.Register<SendTextEditorStringMessage>(this, GetTextEditorData);
            //WeakReferenceMessenger.Default.Register<ReguestTextEditorStringMessage>(this, SendTextEditorData);
            Loaded += OnLoaded;

        }
        //private void SendTextEditorData(object r, ReguestTextEditorStringMessage _)
        //{
        //    WeakReferenceMessenger.Default.Send(new SendTextEditorStringMessage(TextRedactor.Text));
        //}
        private void GetTextEditorData(object r, SendTextEditorStringMessage data)
        {
            currentFile = data.Value;
            TextRedactor.Text = currentFile.Text;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            _host = new RoslynHost(additionalAssemblies: new[]
            {
                Assembly.Load("RoslynPad.Roslyn.Windows"),
                Assembly.Load("RoslynPad.Editor.Windows")
            }, RoslynHostReferences.NamespaceDefault.With(assemblyReferences: new[]
            {
                Assembly.Load("System.Runtime"),
                typeof(object).Assembly,
                typeof(System.Console).Assembly,
                typeof(System.Text.RegularExpressions.Regex).Assembly,
                typeof(System.Linq.Enumerable).Assembly,
            }));
            TextRedactor.Initialize(_host, new DarkModeHighlightColors(), Directory.GetCurrentDirectory(), String.Empty, sourceCodeKind: SourceCodeKind.Regular);
        }

        private void TextRedactor_TextChanged(object sender, EventArgs e)
        {
            if (currentFile == null)
            {
                return;
            }
            currentFile.Text = TextRedactor.Text;
        }
    }
}
