using System;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void execute_Button_Click(object sender, EventArgs e)
        {
            execute_sql();
        }

        private void load_Button_Click(object sender, EventArgs e)
        {
            PromptLoad();
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            PromptSave();
        }
    }
}
