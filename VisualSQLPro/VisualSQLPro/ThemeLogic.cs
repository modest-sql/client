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
            _themeList.Add(_activeTheme);
            Theme theme1 = new Theme("Dark")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#1E1E1E"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#1E1E1E"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#616161"),
                DefaultTextColor = ColorTranslator.FromHtml("#E2E2E2"),
                FailColor = ColorTranslator.FromHtml("#CC9393"),
                SuccessColor = ColorTranslator.FromHtml("#8DCBE2"),
                WatermarkColor = ColorTranslator.FromHtml("#9393CC"),
                CommentColor = ColorTranslator.FromHtml("#73879B"),
                NumberColor = ColorTranslator.FromHtml("#EAB882"),
                Keyword1Color = ColorTranslator.FromHtml("#8DCBE2"),
                Keyword2Color = ColorTranslator.FromHtml("#A893CC"),
                Keyword3Color = ColorTranslator.FromHtml("#BCADAD"),
                Keyword4Color = ColorTranslator.FromHtml("#93A2CC"),
                StringAndCharColor = ColorTranslator.FromHtml("#CC9393"),
                OperatorColor = ColorTranslator.FromHtml("#E2E2E2")
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
            _activeTheme = _themeList[themeIndex];
            ApplyWholeTheme(_themeList[themeIndex]);
        }

        private void ApplyWholeTheme(Theme theme)
        {
            console_log.Focus();
            BackColor = theme.GeneralFormColor;
            foreach (Control control in Controls)
            {
                ApplyControlTheme(control, theme);
            }
        }

        private void ApplyControlTheme(Control control, Theme theme)
        {
            if (control.GetType() == typeof(GroupBox) || control.GetType() == typeof(ToolStrip) || control.GetType() == typeof(TabPage)
                || control.GetType() == typeof(TabControl))
            {
                control.BackColor = theme.DataPresentersBackColor;
                control.ForeColor = theme.DefaultTextColor;
                if (control.GetType() != typeof(TabControl))
                {
                    foreach (Control subControl in control.Controls)
                    {
                        ApplyControlTheme(subControl, theme);
                    }
                }
                else
                {
                    foreach (Control tab in ((TabControl)control).TabPages)
                    {
                        ApplyControlTheme(tab, theme);
                    }
                }
            }
            if (control.GetType() == typeof(MenuStrip))
            {
                control.BackColor = theme.ButtonsBackGroundColor;
                control.ForeColor = theme.DefaultTextColor;
            }
            if (control.GetType() == typeof(ListBox) || control.GetType() == typeof(ListView) || control.GetType() == typeof(RichTextBox)
                || control.GetType() == typeof(TextBox))
            {
                control.BackColor = theme.DataPresentersBackColor;
                control.ForeColor = theme.DefaultTextColor;
            }
            if (control.GetType() == typeof(Button))
            {
                control.BackColor = theme.ButtonsBackGroundColor;
                control.ForeColor = theme.DefaultTextColor;
                ((Button)control).FlatStyle = FlatStyle.Flat;
                ((Button)control).FlatAppearance.BorderColor = theme.ButtonsBackGroundColor;
            }
            if (control.GetType() == typeof(Scintilla))
            {
                SetUpScintillaStyle((Scintilla)control);
                WriteWatermark((Scintilla)control);
            }
            if (control.GetType() == typeof(DataGridView))
            {
                ((DataGridView) control).ColumnHeadersDefaultCellStyle.BackColor = theme.ButtonsBackGroundColor;
                ((DataGridView) control).ColumnHeadersDefaultCellStyle.ForeColor = theme.DefaultTextColor;
                ((DataGridView) control).RowHeadersDefaultCellStyle.BackColor = theme.ButtonsBackGroundColor;
                ((DataGridView)control).RowHeadersDefaultCellStyle.ForeColor = theme.DefaultTextColor;
                foreach (DataGridViewRow row in ((DataGridView)control).Rows)
                {
                    row.DefaultCellStyle.BackColor = theme.DataPresentersBackColor;
                }
                ((DataGridView)control).GridColor = theme.DefaultTextColor;
                control.ForeColor = theme.DefaultTextColor;
            }
        }
    }

    class Theme
    {
        public string ThemeName { get; set; }
        public Color GeneralFormColor { get; set; }
        public Color DataPresentersBackColor { get; set; }
        public Color ButtonsBackGroundColor { get; set; }
        public Color DefaultTextColor { get; set; }
        public Color FailColor { get; set; }
        public Color SuccessColor { get; set; }
        public Color WatermarkColor { get; set; }
        public Color CommentColor { get; set; }
        public Color NumberColor { get; set; }
        public Color Keyword1Color { get; set; }
        public Color Keyword2Color { get; set; }
        public Color Keyword3Color { get; set; }
        public Color Keyword4Color { get; set; }
        public Color StringAndCharColor { get; set; }
        public Color OperatorColor { get; set; }

        public Theme(string themeName)
        {
            ThemeName = themeName;
            GeneralFormColor = SystemColors.Control;
            DataPresentersBackColor = SystemColors.Window;
            ButtonsBackGroundColor = SystemColors.Control;
            DefaultTextColor = SystemColors.WindowText;
            FailColor = Color.Red;
            SuccessColor = Color.Blue;
            WatermarkColor = Color.Gray;
            CommentColor = Color.Green;
            NumberColor = Color.Maroon;
            Keyword1Color = Color.Blue;
            Keyword2Color = Color.Fuchsia;
            Keyword3Color = Color.Gray;
            Keyword4Color = Color.FromArgb(255, 00, 128, 192);
            StringAndCharColor = Color.Red;
            OperatorColor = Color.Black;
        }
    }
}
