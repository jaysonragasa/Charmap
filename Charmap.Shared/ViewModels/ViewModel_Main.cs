/*
 * (c) JaraIO 2020
🐵🐵🐵🐵🐵🐵🐵🍌🐵🐵🍌🍌🍌🍌🐵🐵🐵🐵🐵🐵🐵
🐵🍌🍌🍌🍌🍌🐵🍌🐵🐵🍌🐵🐵🍌🐵🍌🍌🍌🍌🍌🐵
🐵🍌🐵🐵🐵🍌🐵🍌🍌🍌🐵🐵🍌🍌🐵🍌🐵🐵🐵🍌🐵
🐵🍌🐵🐵🐵🍌🐵🍌🐵🍌🍌🐵🍌🍌🐵🍌🐵🐵🐵🍌🐵
🐵🍌🐵🐵🐵🍌🐵🍌🍌🍌🍌🐵🍌🍌🐵🍌🐵🐵🐵🍌🐵
🐵🍌🍌🍌🍌🍌🐵🍌🍌🐵🐵🐵🐵🍌🐵🍌🍌🍌🍌🍌🐵
🐵🐵🐵🐵🐵🐵🐵🍌🐵🍌🐵🍌🐵🍌🐵🐵🐵🐵🐵🐵🐵
🍌🍌🍌🍌🍌🍌🍌🍌🐵🐵🍌🐵🐵🍌🍌🍌🍌🍌🍌🍌🍌
🐵🍌🐵🐵🍌🐵🐵🐵🍌🍌🍌🐵🐵🍌🐵🍌🍌🐵🍌🐵🐵
🍌🍌🐵🐵🐵🍌🍌🍌🐵🐵🐵🍌🐵🍌🐵🐵🍌🍌🐵🍌🐵
🐵🍌🐵🍌🐵🐵🐵🍌🍌🐵🐵🐵🍌🐵🐵🐵🍌🍌🐵🐵🐵
🐵🍌🐵🍌🐵🐵🍌🍌🐵🍌🐵🍌🍌🍌🍌🐵🐵🍌🍌🐵🍌
🍌🍌🍌🐵🐵🐵🐵🐵🍌🐵🍌🍌🐵🍌🍌🐵🍌🍌🍌🍌🐵
🍌🍌🍌🍌🍌🍌🍌🍌🐵🍌🍌🍌🐵🐵🍌🍌🐵🐵🐵🍌🍌
🐵🐵🐵🐵🐵🐵🐵🍌🐵🍌🐵🍌🍌🍌🐵🐵🐵🐵🍌🐵🍌
🐵🍌🍌🍌🍌🍌🐵🍌🐵🐵🍌🍌🍌🍌🍌🐵🐵🐵🐵🐵🍌
🐵🍌🐵🐵🐵🍌🐵🍌🍌🍌🐵🐵🍌🐵🍌🍌🍌🐵🍌🐵🍌
🐵🍌🐵🐵🐵🍌🐵🍌🐵🍌🍌🐵🐵🐵🍌🐵🍌🍌🐵🐵🍌
🐵🍌🐵🐵🐵🍌🐵🍌🐵🍌🐵🍌🐵🍌🐵🍌🍌🍌🍌🍌🍌
🐵🍌🍌🍌🍌🍌🐵🍌🍌🍌🐵🐵🍌🍌🐵🐵🍌🍌🍌🍌🐵
🐵🐵🐵🐵🐵🐵🐵🍌🐵🐵🍌🍌🍌🍌🐵🍌🐵🍌🐵🍌🍌
 */

using Charmap.Shared.Interfaces;
using Charmap.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Charmap.Shared.ViewModels
{
    public class ViewModel_Main : INotifyPropertyChanged
    {
        #region events
        public EventHandler OnOpenFontFileClicked;
        #endregion

        #region vars
        #endregion

        #region properties
        public IFonty Fonty { get; set; } = null;

        public ObservableCollection<Model_Character> Characters { get; set; } = new ObservableCollection<Model_Character>();

        public ObservableCollection<object> FontFamilies { get; set; } = new ObservableCollection<object>();

        private Model_Character _SelectedCharacter = new Model_Character();
        public Model_Character SelectedCharacter
        {
            get { return _SelectedCharacter; }
            set
            {
                _SelectedCharacter = value;

                RaisePropertyChanged("SelectedCharacter");
            }
        }

        private object _SelectedFontFamily = new object();
        public object SelectedFontFamily
        {
            get { return _SelectedFontFamily; }
            set
            {
                _SelectedFontFamily = value;
                RaisePropertyChanged("SelectedFontFamily");

                LoadCharacters(value);
            }
        }

        private bool _ShowProgressBar = false;
        public bool ShowProgressBar
        {
            get { return _ShowProgressBar; }
            set
            {
                if (_ShowProgressBar != value)
                {
                    _ShowProgressBar = value;
                    RaisePropertyChanged("ShowProgressBar");
                }
            }
        }

        private string _FontFamilyName = string.Empty;
        public string FontFamilyName
        {
            get { return _FontFamilyName; }
            set
            {
                if (this._FontFamilyName != value)
                {
                    _FontFamilyName = value;
                    RaisePropertyChanged("FontFamilyName");
                }
            }
        }

        private object _FontFamily = new object();
        public object FontFamily
        {
            get { return _FontFamily; }
            set
            {
                _FontFamily = value;
                RaisePropertyChanged("FontFamily");
            }
        }

        private string _XamlCode = null;
        public string XamlCode
        {
            get { return _XamlCode; }
            set
            {
                if (_XamlCode != value)
                    _XamlCode = value;

                RaisePropertyChanged("XamlCode");
            }
        }

        private string _Unicode = string.Empty;
        public string Unicode
        {
            get { return _Unicode; }
            set
            {
                if (_Unicode != value)
                    _Unicode = value;

                RaisePropertyChanged("Unicode");
            }
        }

        private bool _ShowSidePanel = false;
        public bool ShowSidePanel
        {
            get { return _ShowSidePanel; }
            set
            {
                if (_ShowSidePanel != value)
                    _ShowSidePanel = value;

                RaisePropertyChanged("ShowSidePanel");
            }
        }

        private bool _ShowFontLists = false;
        public bool ShowFontLists
        {
            get { return _ShowFontLists; }
            set
            {
                if (_ShowFontLists != value)
                    _ShowFontLists = value;

                RaisePropertyChanged("ShowFontLists");
            }
        }
        #endregion

        #region commands
        public ICommand Command_OpenFontFile { get; set; }
        public ICommand Command_SelectedCharacter { get; set; }
        public ICommand Command_ShowFontLists { get; set; }
        #endregion

        #region ctors
        public ViewModel_Main()
        {
            InitCommands();

            /*
			// used only in UWP & WPF
			// or anything that supports design time updates
			if(base.IsInDesignMode)
			{
				DesignData();
			}
			else
			{
				RuntimeData();
			}
			*/
        }
        #endregion

        #region command methods
        async void Command_OpenFontFile_Click()
        {
            if (this.Fonty != null)
            {
                this.Fonty?.ShowOpenFileDialog();

                if (this.Fonty.HasSelectedAFile)
                {
                    ShowFontLists = false;
                    ShowSidePanel = false;
                    ShowProgressBar = true;

                    this.FontFamilyName = this.Fonty.SelectedFontFamilyName;
                    this.FontFamily = this.Fonty.FontFamily;

                    var charlist = await this.Fonty.Parse();

                    AddCharactersToList(charlist.ToList());

                    ShowProgressBar = false;
                }
            }
        }

        void Command_SelectedCharacter_Click(Model_Character character)
        {
            this.SelectedCharacter = character;

            this.Unicode = "\\U000" + character.IndexHex.ToUpperInvariant();
            this.XamlCode = $"&#x" + character.IndexHex;

            ShowSidePanel = true;
        }

        async void Command_ShowFontLists_Click()
        {
            this.ShowFontLists = !this.ShowFontLists;

            if(this.FontFamilies.Count == 0)
            {
                var fontfams = await this.Fonty.GetFontFamilies();
                for (int i = 0; i < fontfams.Count; i++)
                {
                    this.FontFamilies.Add(fontfams[i]);
                }
            }
        }
        #endregion

        #region methods
        void InitCommands()
        {
            if (Command_OpenFontFile == null) Command_OpenFontFile = new RelayCommand(Command_OpenFontFile_Click);
            if (Command_ShowFontLists == null) Command_ShowFontLists = new RelayCommand(Command_ShowFontLists_Click);
            if (Command_SelectedCharacter == null) Command_SelectedCharacter = new RelayCommand<Model_Character>(Command_SelectedCharacter_Click);
        }

        void DesignData()
        {

        }

        void RuntimeData()
        {

        }

        public async Task RefreshData()
        {

        }

        void LoadCharacters(object fontfamily)
        {
            ShowSidePanel = false;
            ShowProgressBar = true;

            this.FontFamilyName = fontfamily.ToString();
            this.FontFamily = fontfamily;
            var charlist = this.Fonty.GetCharactersFromFontFamiy(fontfamily);

            AddCharactersToList(charlist);

            ShowProgressBar = false;
        }

        void AddCharactersToList(List<Model_Character> chars)
        {
            this.Characters.Clear();
            for (int i = 0; i < chars.Count; i++)
            {
                this.Characters.Add(new Model_Character()
                {
                    IndexHex = chars[i].IndexHex,
                    Character = chars[i].Character
                });
            }
        }
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}