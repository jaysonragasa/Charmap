using Charmap.Shared.Interfaces;
using Charmap.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charmap.UWP
{
    public class Fonty : IFonty
    {
        public List<Model_Character> CharacterLists { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool HasSelectedAFile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SelectedFontFamilyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object FontFamily { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Model_Character> GetCharactersFromFontFamiy(object fontFamily)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetFontFamilies()
        {
            throw new NotImplementedException();
        }

        public Task<List<Model_Character>> Parse()
        {
            throw new NotImplementedException();
        }

        public void ShowOpenFileDialog()
        {
            throw new NotImplementedException();
        }
    }
}