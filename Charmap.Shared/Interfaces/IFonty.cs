using Charmap.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charmap.Shared.Interfaces
{
    public interface IFonty
    {
        List<Model_Character> CharacterLists { get; set; }
        bool HasSelectedAFile { get; set; }
        void ShowOpenFileDialog();
        Task<List<Model_Character>> Parse();
        string SelectedFontFamilyName { get; set; }
        object FontFamily { get; set; }
        Task<List<object>> GetFontFamilies();
        List<Model_Character> GetCharactersFromFontFamiy(object fontFamily);
    }
}