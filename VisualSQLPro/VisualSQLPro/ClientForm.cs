using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using ScintillaNET;

namespace VisualSQLPro
{
    public partial class ClientForm : Form
    {
        private readonly System.Timers.Timer _metadataTimer = new System.Timers.Timer();
        private readonly System.Timers.Timer _consoleTimer = new System.Timers.Timer();
        private readonly Scintilla _myScintilla = new Scintilla();
        public ClientForm()
        {
            InitializeComponent();
            Text = "Modest SQL Client Pro";
            SetUpResizers();
            SetUpTimers();
            SetUpScintilla();
        }

        private void SetUpScintilla()
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
            _myScintilla.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
            // Word2 = 1
            _myScintilla.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
            // User1 = 4
            _myScintilla.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
            // User2 = 5
            _myScintilla.SetKeywords(5, @"sys objects sysobjects ");

            _myScintilla.Dock = DockStyle.Fill;
            query_tabPage.Controls.Add(_myScintilla);
        }

        private void SetUpTimers()
        {
            _metadataTimer.Interval = 50;
            _metadataTimer.Enabled = false;
            _metadataTimer.Elapsed += MetadataTimerEvent;
            _metadataTimer.AutoReset = true;

            _consoleTimer.Interval = 50;
            _consoleTimer.Enabled = false;
            _consoleTimer.Elapsed += ConsoleTimerEvent;
            _consoleTimer.AutoReset = true;
        }

        private void ConsoleTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateConPosition();
        }

        private void MetadataTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateMetaPosition();
        }

        delegate void UpdateMetadataPosition();
        delegate void UpdateConsolePosition();

        private void UpdateConPosition()
        {
            if (console_groupBox.InvokeRequired)
                console_groupBox.Invoke(new UpdateConsolePosition(UpdateConPosition));
            else
                console_groupBox.Height = Convert.ToInt32(Size.Height - PointToClient(Cursor.Position).Y - 30); // For some reason, resize is always 30 pixels off
        }

        private void UpdateMetaPosition()
        {
            if (metadata_group.InvokeRequired)
                metadata_group.Invoke(new UpdateMetadataPosition(UpdateMetaPosition));
            else
                metadata_group.Width = Convert.ToInt32(PointToClient(Cursor.Position).X + 2); // For some reason, resize is always 2 pixels off
        }

        private void SetUpResizers()
        {
            metadata_group.MouseDown += Metadata_group_MouseDown;
            metadata_group.MouseUp += Metadata_group_MouseUp;
            metadata_group.MouseMove += Metadata_group_MouseMove;
            console_groupBox.MouseDown += ConsoleLog_group_MouseDown;
            console_groupBox.MouseUp += ConsoleLog_group_MouseUp;
            console_groupBox.MouseMove += ConsoleLog_group_MouseMove;
        }
        
        private void Metadata_group_MouseDown(object sender, MouseEventArgs e)
        {
            _metadataTimer.Enabled = true;
        }

        private void Metadata_group_MouseUp(object sender, MouseEventArgs e)
        {
            _metadataTimer.Enabled = false;
        }

        private void Metadata_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.VSplit;
        }

        private void ConsoleLog_group_MouseDown(object sender, MouseEventArgs e)
        {
            _consoleTimer.Enabled = true;
        }

        private void ConsoleLog_group_MouseUp(object sender, MouseEventArgs e)
        {
            _consoleTimer.Enabled = false;
        }

        private void ConsoleLog_group_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.VSplit)
                Cursor.Current = Cursors.HSplit;
        }
    }
}
