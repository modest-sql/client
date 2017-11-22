using System.Drawing;
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

            //Start style setup//
            if (OperatingSystem.IsWindows())
            {
                _myScintilla.StyleResetDefault();
                _myScintilla.Styles[Style.Default].Font = "Courier New";
                _myScintilla.Styles[Style.Default].Size = 10;
                _myScintilla.StyleClearAll();

                _myScintilla.Lexer = Lexer.Sql;

                _myScintilla.Margins[0].Width = 20;

                _myScintilla.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128);
                _myScintilla.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228);
                _myScintilla.Styles[Style.Sql.Comment].ForeColor = Color.Green;
                _myScintilla.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
                _myScintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.Green;
                _myScintilla.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
                _myScintilla.Styles[Style.Sql.Word].ForeColor = Color.Blue;
                _myScintilla.Styles[Style.Sql.Word2].ForeColor = Color.Fuchsia;
                _myScintilla.Styles[Style.Sql.User1].ForeColor = Color.Gray;
                _myScintilla.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(255, 00, 128, 192);
                _myScintilla.Styles[Style.Sql.String].ForeColor = Color.Red;
                _myScintilla.Styles[Style.Sql.Character].ForeColor = Color.Red;
                _myScintilla.Styles[Style.Sql.Operator].ForeColor = Color.Black;

                // Set keyword lists
                // Word = 0
                _myScintilla.SetKeywords(0,
                    @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
                // Word2 = 1
                _myScintilla.SetKeywords(1,
                    @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
                // User1 = 4
                _myScintilla.SetKeywords(4,
                    @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
                // User2 = 5
                _myScintilla.SetKeywords(5, @"sys objects sysobjects ");
            }
            //End style setup//

            //Start auto-complete setup//

            if (OperatingSystem.IsWindows())
            {
                var autoCompleteWrapper = new AutocompleteMenu { TargetControlWrapper = new ScintillaWrapper(_myScintilla) };
                string[] autoCompleteList = {"add", "alter", "create", "drop", "insert", "into", "key", "primary", "select", "table", "unique", "update",
                                        "where", "between", "inner", "join", "not null", "default", "from"};
                foreach (var str in autoCompleteList)
                {
                    autoCompleteWrapper.AddItem(str);
                }
                autoCompleteWrapper.AllowsTabKey = true;
            }

            //End auto-complete setup//

            _myScintilla.Dock = DockStyle.Fill;
            myTabPage.Controls.Add(_myScintilla);
        }
    }

    public static class OperatingSystem
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}
