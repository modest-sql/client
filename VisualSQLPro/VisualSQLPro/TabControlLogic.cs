using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void PrintTable(string json)
        {
            var table = JsonConvert.DeserializeObject<DataTable>(json);

            string title = "Query" + (queries_tabControl.TabCount) + "          ";
            TabPage myTabPage = new TabPage(title);
            queries_tabControl.TabPages.Add(myTabPage);

            DataGridView dataGrid = new DataGridView
            {
                DataSource = table,
                Dock = DockStyle.Fill
            };
            dataGrid.CellFormatting += dataGrid_CellFormatting;
            dataGrid.DataBindingComplete += dataGrid_DataBindingComplete;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.ReadOnly = true;
            dataGrid.EnableHeadersVisualStyles = false;
            FillRecordNo(dataGrid);
            myTabPage.Controls.Add(dataGrid);

            queries_tabControl.SelectedTab = myTabPage;
        }

        private void dataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FillRecordNo(((DataGridView) sender));
            ChangeBooleanCheckBoxes((DataGridView)sender);
        }

        private void FillRecordNo(DataGridView dataGrid)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                dataGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DBNull && e.DesiredType != typeof(bool))
            {
                e.Value = "NULL";
                e.CellStyle.Font = new Font(((DataGridView)sender).DefaultCellStyle.Font, FontStyle.Italic);
            }
        }

        private void ChangeBooleanCheckBoxes(DataGridView dataGrid)
        {
            int cont = 0;
            List<int> boolIndeces = new List<int>();
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                if (column.ValueType == typeof(bool))
                    boolIndeces.Add(cont);
                cont++;
            }

            foreach (var index in boolIndeces)
            {
                DataGridViewColumn column = dataGrid.Columns[index];
                var stringColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.Name,
                    DataPropertyName = column.DataPropertyName
                };
                dataGrid.Columns.RemoveAt(index);
                dataGrid.Columns.Insert(index, stringColumn);
            }
        }

        private void queries_tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.Gray, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(queries_tabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void queries_tabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < queries_tabControl.TabPages.Count; i++)
                {
                    Rectangle r = queries_tabControl.GetTabRect(i);
                    Rectangle checkedRectangle = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                    if (e.Button == MouseButtons.Middle)
                        checkedRectangle = r;
                    if (checkedRectangle.Contains(e.Location))
                    {
                        if (i == 0)
                            MessageBox.Show(@"Can't close this tab.");
                        else
                        {
                            queries_tabControl.TabPages.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
    }
}
