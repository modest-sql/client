using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        readonly List<Theme> _themeList = new List<Theme>();

        private void SetUpThemes()
        {
            Theme theme1 = new Theme()
            {
                ThemeName = "Dark",
                GeneralFormColor = ColorTranslator.FromHtml("#1E1E1E"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#616161"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#364656"),
                DefaultColor = ColorTranslator.FromHtml("#C0C0C0"),
                FailColor = ColorTranslator.FromHtml("#CC9393"),
                SuccessColor = ColorTranslator.FromHtml("#8DCBE2"),
                WatermarkColor = ColorTranslator.FromHtml("#9393CC"),
            };
            _themeList.Add(theme1);
            
            foreach (var theme in _themeList)
            {
                ToolStripMenuItem newTheme = new ToolStripMenuItem() {Name = theme.ThemeName, Text = theme.ThemeName};
                themesToolStripMenuItem.DropDownItems.Add(newTheme);
            }
        }

        private void themesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int themeIndex = themesToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            ApplyWholeTheme(_themeList[themeIndex]);
        }

        private void ApplyWholeTheme(Theme theme)
        {
            BackColor = theme.GeneralFormColor;
            foreach (Control control in Controls)
            {
                ApplyControlTheme(control, theme);
            }
        }

        private void ApplyControlTheme(Control control, Theme theme)
        {
            if (control.GetType() == typeof(GroupBox) || control.GetType() == typeof(ToolStrip) || control.GetType() == typeof(TabPage))
            {
                control.BackColor = theme.DataPresentersBackColor;
                control.ForeColor = theme.DefaultColor;
                foreach (Control subControl in control.Controls)
                {
                    ApplyControlTheme(subControl, theme);
                }
            }
            if (control.GetType() == typeof(MenuStrip))
            {
                control.BackColor = theme.ButtonsBackGroundColor;
                control.ForeColor = theme.DefaultColor;
            }
            if (control.GetType() == typeof(ListBox) || control.GetType() == typeof(ListView) || control.GetType() == typeof(RichTextBox)
                || control.GetType() == typeof(TextBox) || control.GetType() == typeof(Scintilla))
            {
                control.BackColor = theme.DataPresentersBackColor;
                control.ForeColor = theme.DefaultColor;
            }
            if (control.GetType() == typeof(Button))
            {
                control.BackColor = theme.ButtonsBackGroundColor;
                control.ForeColor = theme.DefaultColor;
                ((Button)control).FlatStyle = FlatStyle.Flat;
                ((Button)control).FlatAppearance.BorderColor = theme.ButtonsBackGroundColor;
            }
            if (control.GetType() == typeof(TabControl))
            {
                control.BackColor = theme.ButtonsBackGroundColor;
                control.ForeColor = theme.DefaultColor;
                foreach (Control tab in ((TabControl)control).TabPages)
                {
                    ApplyControlTheme(tab, theme);
                }
            }
        }
    }

    class Theme
    {
        public string ThemeName { get; set; }
        public Color GeneralFormColor { get; set; }
        public Color DataPresentersBackColor { get; set; }
        public Color ButtonsBackGroundColor { get; set; }
        public Color DefaultColor { get; set; }
        public Color FailColor { get; set; }
        public Color SuccessColor { get; set; }
        public Color WatermarkColor { get; set; }
    }
}
