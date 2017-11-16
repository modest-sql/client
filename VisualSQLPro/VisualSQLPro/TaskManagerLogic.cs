using System.Timers;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private readonly Timer _taskManagerUpdateTimer = new Timer();

        private void SetUpTaskManager()
        {
            task_manager_groupBox.Visible = false;
            _taskManagerUpdateTimer.Interval = 1000;
            _taskManagerUpdateTimer.Elapsed += TaskManagerUpdateTimerEvent;
            _taskManagerUpdateTimer.Enabled = true;
            _taskManagerUpdateTimer.AutoReset = true;
        }

        private void TaskManagerUpdateTimerEvent(object sender, ElapsedEventArgs e)
        {
            if (task_manager_groupBox.Visible)
                BuildAndSendServerRequest((int) ServerRequests.ShowTransaction,"");
        }
        private void UpdateTaskManager(string tasks)
        {
            var transactions = JsonConvert.DeserializeObject<TransactionTable>(tasks);

            int counter = 1;
            foreach (var task in transactions.Transactions)
            {
                task_manager_listBox.Items.Add("Transaction " + counter);
                task_manager_listBox.Items.Add("\t" +"Transaction ID: " + task.Transaction_ID);
                task_manager_listBox.Items.Add("\t" + "Transaction State: " + task.Transaction_State);
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
    }
}
