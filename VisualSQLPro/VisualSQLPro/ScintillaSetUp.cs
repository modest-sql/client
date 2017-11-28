using System;
using System.Windows.Forms;
using AutocompleteMenuNS;
using ScintillaNET;
using System.Runtime.InteropServices;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private readonly Scintilla _myScintilla = new Scintilla();
        private void SetUpScintilla()
        {
            string title = "Query Text" + "          ";
            TabPage myTabPage = new TabPage(title);
            queries_tabControl.TabPages.Add(myTabPage);

            SetUpScintillaStyle(_myScintilla);
            SetupScintillaAutoComplete(_myScintilla);
            SetUpScintillaEvents(_myScintilla);

            _myScintilla.Dock = DockStyle.Fill;
            myTabPage.Controls.Add(_myScintilla);
        }

        private void SetUpScintillaEvents(Scintilla myScintilla)
        {
            myScintilla.GotFocus += ScintillaFocusGainedEvent;
            myScintilla.LostFocus += ScintillaFocusLostEvent;
        }

        private void ScintillaFocusGainedEvent(object sender, EventArgs e)
        {
            if (((Scintilla)sender).Text == @"Enter query here...")
            {
                ((Scintilla)sender).SelectAll();
                ((Scintilla)sender).Clear();
            }
        }

        private void ScintillaFocusLostEvent(object sender, EventArgs e)
        {
            WriteWatermark((Scintilla) sender);
        }

        private void SetUpScintillaStyle(Scintilla myScintilla)
        {
            //myScintilla.StyleResetDefault();
            myScintilla.Styles[Style.Default].BackColor = _activeTheme.DataPresentersBackColor;
            myScintilla.Styles[Style.Default].Font = "Courier New";
            myScintilla.Styles[Style.Default].Size = 10;
            myScintilla.Styles[Style.Default].ForeColor = _activeTheme.DefaultTextColor;
            myScintilla.CaretForeColor = _activeTheme.DefaultTextColor;
            myScintilla.StyleClearAll();

            myScintilla.Lexer = Lexer.Sql;

            myScintilla.Margins[0].Width = 40;
            
            myScintilla.Styles[Style.LineNumber].ForeColor = _activeTheme.DefaultTextColor;
            myScintilla.Styles[Style.LineNumber].BackColor = _activeTheme.ButtonsBackGroundColor;
            myScintilla.Styles[Style.Sql.Comment].ForeColor = _activeTheme.CommentColor;
            myScintilla.Styles[Style.Sql.CommentLine].ForeColor = _activeTheme.CommentColor;
            myScintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = _activeTheme.CommentColor;
            myScintilla.Styles[Style.Sql.Number].ForeColor = _activeTheme.NumberColor;
            myScintilla.Styles[Style.Sql.Word].ForeColor = _activeTheme.Keyword1Color;
            myScintilla.Styles[Style.Sql.Word2].ForeColor = _activeTheme.Keyword2Color;
            myScintilla.Styles[Style.Sql.User1].ForeColor = _activeTheme.Keyword3Color;
            myScintilla.Styles[Style.Sql.User2].ForeColor = _activeTheme.Keyword4Color;
            myScintilla.Styles[Style.Sql.String].ForeColor = _activeTheme.StringAndCharColor;
            myScintilla.Styles[Style.Sql.Character].ForeColor = _activeTheme.StringAndCharColor;
            myScintilla.Styles[Style.Sql.Operator].ForeColor = _activeTheme.OperatorColor;

            // Set keyword lists
            // Word = 0
            myScintilla.SetKeywords(0,
                @"add alter as authorization backup begin bigint binary bit boolean break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert integer intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
            // Word2 = 1
            myScintilla.SetKeywords(1,
                @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
            // User1 = 4
            myScintilla.SetKeywords(4,
                @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
            // User2 = 5
            myScintilla.SetKeywords(5, @"sys objects sysobjects ");
        }

        private void SetupScintillaAutoComplete(Scintilla myScintilla)
        {
            var autoCompleteWrapper = new AutocompleteMenu { TargetControlWrapper = new ScintillaWrapper(myScintilla) };
            string[] autoCompleteList = {"add", "alter", "create", "drop", "insert", "into", "key", "primary", "select", "table", "unique", "update",
                                        "where", "between", "inner", "join", "not null", "default", "from", "integer", "boolean"};
            foreach (var str in autoCompleteList)
            {
                autoCompleteWrapper.AddItem(str);
            }
            autoCompleteWrapper.AllowsTabKey = true;
        }

        private void WriteWatermark(Scintilla myScintilla)
        {
            if (myScintilla.Text == "" || myScintilla.Text == @"Enter query here...")
            {
                myScintilla.Text = @"Enter query here...";
                myScintilla.SelectAll();
                myScintilla.SetSelectionBackColor(true, _activeTheme.DataPresentersBackColor);
                myScintilla.SetSelectionForeColor(true, _activeTheme.WatermarkColor);
            }
        }
    }

    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOs() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
