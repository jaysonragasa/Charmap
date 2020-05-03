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

using Charmap.Shared.Helpers;
using Charmap.Shared.Interfaces;
using Charmap.Shared.Models;

using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Charmap.NetCore.WPF
{
    public class Fonty : IFonty
    {
        public List<Model_Character> CharacterLists { get; set; } = new List<Model_Character>();

        FontFamily CurrentFontFamily { get; set; } = null;

        public bool HasSelectedAFile { get; set; } = false;

        public string SelectedFontFamilyName { get; set; } = string.Empty;

        public object FontFamily { get; set; } = null;

        public void ShowOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "TTF Files|*.ttf",
                Multiselect = false
            };
            {
                var hassomethinginit = ofd.ShowDialog().Value;
                this.HasSelectedAFile = false;
                if (hassomethinginit && !string.IsNullOrEmpty(ofd.FileName))
                {
                    this.HasSelectedAFile = true;

                    string path = Path.GetDirectoryName(ofd.FileName);
                    string filename = Path.GetFileName(ofd.FileName);

                    path = Path.Combine(path, filename);

                    this.CurrentFontFamily = new FontFamily(path);

                    // get font familyname
                    {
                        var fontfamily = Fonts.GetFontFamilies(this.CurrentFontFamily.Source).FirstOrDefault();
                        int startIndex = fontfamily.ToString().IndexOf('#');
                        this.SelectedFontFamilyName = fontfamily.ToString().Substring(startIndex + 1);
                        this.FontFamily = fontfamily;
                    }
                }
            }
        }

        public async Task<List<Model_Character>> Parse()
        {
            var t = await Task.Run(() =>
            {
                var fontfamilies = Fonts.GetFontFamilies(this.CurrentFontFamily.Source);

                if (fontfamilies.Any())
                {
                    List<Model_Character> chars = GetCharactersFromFontFamiy(fontfamilies.FirstOrDefault());

                    return chars;
                }

                return null;
            });

            return t;
        }

        public List<Model_Character> GetCharactersFromFontFamiy(object fontFamily)
        {
            List<Model_Character> chars = new List<Model_Character>();

            IDictionary<int, ushort> characterMap;
            GlyphTypeface glyph = null;

            var typefaces = ((FontFamily)fontFamily).GetTypefaces();

            foreach (Typeface typeface in typefaces)
            {
                typeface.TryGetGlyphTypeface(out glyph);

                if (glyph != null)
                {
                    characterMap = glyph.CharacterToGlyphMap;

                    chars.Clear();
                    for (int i = 0; i < characterMap.Count; i++)
                    {
                        // Keys is actually the actual value
                        int value = characterMap.Keys.ElementAt(i);

                        string toUtf16 = "";
                        string c = "";
                        UnicodeEncoding u = new UnicodeEncoding();

                        try
                        {
                            string toHex = value.ToString("x");

                            if (toHex.Length < 4)
                                toHex = value.ToString("x4");

                            toUtf16 = UTFHelper.UnicodePointToUTF16(toHex);
                            byte[] bytes = UTFHelper.GetUTF16Bytes(toUtf16);
                            c = u.GetString(bytes);

                            //toUtf16 = UTFHelper.UnicodePointToUTF16("0000");
                            //bytes = UTFHelper.GetUTF16Bytes(toUtf16);
                            //c += u.GetString(bytes);

                            chars.Add(new Model_Character()
                            {
                                IndexHex = toHex,
                                Character = c.Trim()
                            });
                        }
                        catch (Exception ex)
                        {
                            //if(ex.Message == "Value was either too large or too small for a character.")
                            //{
                            //    chars.Add("");
                            //}
                            //else
                            {
                                chars.Add(new Model_Character()
                                {
                                });
                            }
                        }
                    }
                }

                break;
            }

            return chars;
        }

        public async Task<List<object>> GetFontFamilies()
        {
            List<object> fontlists = new List<object>();

            var t = await Task.Run(() =>
            {
                List<FontFamily> fonts = Fonts.SystemFontFamilies.ToList();
                for (int i = 0; i < fonts.Count; i++)
                {
                    fontlists.Add(fonts[i]);
                }

                return fontlists;
            });

            return fontlists;
        }
    }
}