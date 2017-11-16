using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void PrintTable(string json)
        {
            var table = JsonConvert.DeserializeObject<DataTable>(json);

            string title = "Query " + (queries_tabControl.TabCount).ToString();
            TabPage myTabPage = new TabPage(title);
            queries_tabControl.TabPages.Add(myTabPage);

            DataGridView dataGrid = new DataGridView {DataSource = table};
            dataGrid.Dock = DockStyle.Fill;
            myTabPage.Controls.Add(dataGrid);

            queries_tabControl.SelectedTab = myTabPage;
        }
    }
}
