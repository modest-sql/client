using System.Windows.Forms;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private void SetUpMouseHooks()
        {
            MouseMove += OnMouseMove;
            HookMouseMove(Controls);
        }

        private void HookMouseMove(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                ctl.MouseMove += OnMouseMove;
                HookMouseMove(ctl.Controls);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor.Current != Cursors.Default)
                Cursor.Current = Cursors.Default;
        }
    }
}
