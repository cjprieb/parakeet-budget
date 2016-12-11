using System;
using System.Windows.Forms;
using NLog;

namespace Willowcat.BudgetUi
{
    public class BudgetErrorBox
    {
        private static ILogger _Logger = LogManager.GetCurrentClassLogger();
        private const string _Caption = "Budget Program Error";

        public static void Show(string message)
        {
            _Logger.Error(message);
            MessageBox.Show(message, _Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Show(Exception ex)
        {
            _Logger.Error(ex);
            MessageBox.Show(ex.Message, _Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Show(string message, Exception ex)
        {
            _Logger.Error(ex, message);
            MessageBox.Show($"{message}\n{ex.Message}", _Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
