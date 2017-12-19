using System.Timers;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private readonly Timer _taskManagerUpdateTimer = new Timer();
        private string _currentTaskManagerText = "";

        private void SetUpTaskManager()
        {
            task_manager_groupBox.Visible = false;
            _taskManagerUpdateTimer.Interval = 1500;
            _taskManagerUpdateTimer.Elapsed += TaskManagerUpdateTimerEvent;
            _taskManagerUpdateTimer.Enabled = true;
            _taskManagerUpdateTimer.AutoReset = false;
        }

        private void TaskManagerUpdateTimerEvent(object sender, ElapsedEventArgs e)
        {
            if (task_manager_groupBox.Visible)
                BuildAndSendServerRequest((int) ServerRequests.ShowTransaction," ");
            _taskManagerUpdateTimer.Start();
        }
        private void UpdateTaskManager(string tasks)
        {
            if (_currentTaskManagerText == tasks)
                return;
            _currentTaskManagerText = tasks;
            task_manager_listBox.Items.Clear();
            var transactions = JsonConvert.DeserializeObject<TransactionTable>(tasks);

            int counter = 1;
            if (transactions == null)
                return;
            foreach (var task in transactions.Transactions)
            {
                task_manager_listBox.Items.Add("Transaction " + counter);
                string transactionId;
                switch (task.Transaction_State)
                {
                    case 1:
                        transactionId = "Executing";
                        break;
                    case 2:
                        transactionId = "Completed";
                        break;
                    default:
                        transactionId = "Queued";
                        break;
                }
                task_manager_listBox.Items.Add("\t" +"Transaction ID: " + task.Transaction_ID);
                task_manager_listBox.Items.Add("\t" + "Transaction State: " + transactionId);
                task_manager_listBox.Items.Add("\t" + "Current transaction: " + task.Current_Command);
                task_manager_listBox.Items.Add("\t" + "Transaction Queries:");
                foreach (var query in task.TransactionQueries)
                {
                    task_manager_listBox.Items.Add("\t\t" + query);
                }
                counter++;
            }
        }
    }

    class TransactionTable
    {
        public TransactionRegister[] Transactions { get; set; }
    }

    class TransactionRegister
    {
        public string Transaction_ID { get; set; }
        public string[] TransactionQueries { get; set; }
        public int Transaction_State { get; set; }
        public string Current_Command { get; set; }
    }
}
