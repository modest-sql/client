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
            _themeList.Add(new Theme("Oblivion")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#1E1E1E"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#1E1E1E"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#2A2A2A"),
                DefaultTextColor = ColorTranslator.FromHtml("#D8D8D8"),
                FailColor = ColorTranslator.FromHtml("#D25252"),
                SuccessColor = ColorTranslator.FromHtml("#79ABFF"),
                WatermarkColor = ColorTranslator.FromHtml("#D9E577"),
                CommentColor = ColorTranslator.FromHtml("#D9E577"),
                NumberColor = ColorTranslator.FromHtml("#7FB347"),
                Keyword1Color = ColorTranslator.FromHtml("#C7DD0C"),
                Keyword2Color = ColorTranslator.FromHtml("#FFC600"),
                Keyword3Color = ColorTranslator.FromHtml("#79ABFF"),
                Keyword4Color = ColorTranslator.FromHtml("#D197D9"),
                StringAndCharColor = ColorTranslator.FromHtml("#D25252"),
                OperatorColor = ColorTranslator.FromHtml("#D8D8D8")
            });

            _themeList.Add(new Theme("Sublime 2")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#272822"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#272822"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#404040"),
                DefaultTextColor = ColorTranslator.FromHtml("#CFBFAD"),
                FailColor = ColorTranslator.FromHtml("#FF0000"),
                SuccessColor = ColorTranslator.FromHtml("#52E3F6"),
                WatermarkColor = ColorTranslator.FromHtml("#D8D8D8"),
                CommentColor = ColorTranslator.FromHtml("#FFFFFF"),
                NumberColor = ColorTranslator.FromHtml("#C48CFF"),
                Keyword1Color = ColorTranslator.FromHtml("#52E3F6"),
                Keyword2Color = ColorTranslator.FromHtml("#A7EC21"),
                Keyword3Color = ColorTranslator.FromHtml("#FF007F"),
                Keyword4Color = ColorTranslator.FromHtml("#CC9900"),
                StringAndCharColor = ColorTranslator.FromHtml("#FF0000"),
                OperatorColor = ColorTranslator.FromHtml("#CFBFAD")
            });

            _themeList.Add(new Theme("Zenburn")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#404040"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#404040"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#404040"),
                DefaultTextColor = ColorTranslator.FromHtml("#F6F3E8"),
                FailColor = ColorTranslator.FromHtml("#CC9393"),
                SuccessColor = ColorTranslator.FromHtml("#53DCCD"),
                WatermarkColor = ColorTranslator.FromHtml("#BCADAD"),
                CommentColor = ColorTranslator.FromHtml("#7F9F7F"),
                NumberColor = ColorTranslator.FromHtml("#8ACCCF"),
                Keyword1Color = ColorTranslator.FromHtml("#CAE682"),
                Keyword2Color = ColorTranslator.FromHtml("#DFBE95"),
                Keyword3Color = ColorTranslator.FromHtml("#93A2CC"),
                Keyword4Color = ColorTranslator.FromHtml("#B3B5AF"),
                StringAndCharColor = ColorTranslator.FromHtml("#CC9393"),
                OperatorColor = ColorTranslator.FromHtml("#F6F3E8")
            });

            _themeList.Add(new Theme("Roboticket")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#F5F5F5"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#F5F5F5"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#F5F5F5"),
                DefaultTextColor = ColorTranslator.FromHtml("#585858"),
                FailColor = ColorTranslator.FromHtml("#AB2525"),
                SuccessColor = ColorTranslator.FromHtml("#295F94"),
                WatermarkColor = ColorTranslator.FromHtml("#FFDF99"),
                CommentColor = ColorTranslator.FromHtml("#AD95AF"),
                NumberColor = ColorTranslator.FromHtml("#AF0F91"),
                Keyword1Color = ColorTranslator.FromHtml("#2C577C"),
                Keyword2Color = ColorTranslator.FromHtml("#55aa55"),
                Keyword3Color = ColorTranslator.FromHtml("#AD95AF"),
                Keyword4Color = ColorTranslator.FromHtml("#CC9393"),
                StringAndCharColor = ColorTranslator.FromHtml("#317ECC"),
                OperatorColor = ColorTranslator.FromHtml("#585858")
            });

            _themeList.Add(new Theme("Solarized Light")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#FDF6E3"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#FDF6E3"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#ECE7D5"),
                DefaultTextColor = ColorTranslator.FromHtml("#657A81"),
                FailColor = ColorTranslator.FromHtml("#D30102"),
                SuccessColor = ColorTranslator.FromHtml("#2AA198"),
                WatermarkColor = ColorTranslator.FromHtml("#93A1A1"),
                CommentColor = ColorTranslator.FromHtml("#586E75"),
                NumberColor = ColorTranslator.FromHtml("#2AA198"),
                Keyword1Color = ColorTranslator.FromHtml("#B58900"),
                Keyword2Color = ColorTranslator.FromHtml("#00FF00"),
                Keyword3Color = ColorTranslator.FromHtml("#D33682"),
                Keyword4Color = ColorTranslator.FromHtml("#BCADAD"),
                StringAndCharColor = ColorTranslator.FromHtml("#D30102"),
                OperatorColor = ColorTranslator.FromHtml("#657A81")
            });

            _themeList.Add(new Theme("Retta")
            {
                GeneralFormColor = ColorTranslator.FromHtml("#000000"),
                DataPresentersBackColor = ColorTranslator.FromHtml("#000000"),
                ButtonsBackGroundColor = ColorTranslator.FromHtml("#2A2A2A"),
                DefaultTextColor = ColorTranslator.FromHtml("#F8E1AA"),
                FailColor = ColorTranslator.FromHtml("#DE6546"),
                SuccessColor = ColorTranslator.FromHtml("#A4B0C0"),
                WatermarkColor = ColorTranslator.FromHtml("#D6C248"),
                CommentColor = ColorTranslator.FromHtml("#83786E"),
                NumberColor = ColorTranslator.FromHtml("#D6C248"),
                Keyword1Color = ColorTranslator.FromHtml("#C97138"),
                Keyword2Color = ColorTranslator.FromHtml("#FFFF00"),
                Keyword3Color = ColorTranslator.FromHtml("#E79E3C"),
                Keyword4Color = ColorTranslator.FromHtml("#DE6546"),
                StringAndCharColor = ColorTranslator.FromHtml("#E79E3C"),
                OperatorColor = ColorTranslator.FromHtml("#F8E1AA")
            });

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
