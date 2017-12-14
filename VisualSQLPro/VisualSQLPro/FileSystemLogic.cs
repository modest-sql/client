using System.IO;
using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void PromptSave()
        {
            FileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "MySQL";
            saveFileDialog.DefaultExt = "sql";
            saveFileDialog.Filter = @"SQL file (*.sql)|*.sql|Text file (*.txt)|*.txt";
            DialogResult result = saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "" && result == DialogResult.OK && _sqlTextControl.Text != "")
                File.WriteAllText(saveFileDialog.FileName, _sqlTextControl.Text);
        }

        private void PromptLoad()
        {
            FileDialog loadFileDialog = new OpenFileDialog();
            loadFileDialog.DefaultExt = "sql";
            loadFileDialog.Filter = @"SQL files (*.sql)|*.sql|Text files (*.txt)|*.txt";
            DialogResult result = loadFileDialog.ShowDialog();
            if (loadFileDialog.FileName != "" && result == DialogResult.OK)
                _sqlTextControl.Text = File.ReadAllText(loadFileDialog.FileName);
        }
    }
}
