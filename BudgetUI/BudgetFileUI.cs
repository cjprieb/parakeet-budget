using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Budget;
using Budget.Models;

namespace BudgetUI
{
    public partial class BudgetFileUI : UserControl
    {
        private PaycheckPeriodBudget _paycheckPeriodBudget;
        private BudgetFile _budgetFile;
        private DataTable _budgetDataTable;

        public BudgetFileUI()
        {
            InitializeComponent();
        }

        public BudgetFileUI(string selectedFile)
        {
            _budgetFile = new BudgetFile(selectedFile);

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //TODO: do on async
            _paycheckPeriodBudget = _budgetFile.ParseFile();

            SetupColumns();
            _budgetDataTable = ConvertToDataTable(_paycheckPeriodBudget.LineItems);
            dataGridView.DataSource = _budgetDataTable;
        }

        private void SetupColumns()
        {
            // 
            // dateColumn
            // 
            var dateColumn = new DataGridViewTextBoxColumn();
            dateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dateColumn.DataPropertyName = "Date";
            dateColumn.Frozen = true;
            dateColumn.HeaderText = "Date";
            dateColumn.Name = "dateColumn";
            dateColumn.Width = 60;
            dataGridView.Columns.Add(dateColumn);

            // 
            // descriptionColumn
            // 
            var descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.Frozen = true;
            descriptionColumn.HeaderText = "Description";
            descriptionColumn.MinimumWidth = 100;
            descriptionColumn.Name = "descriptionColumn";
            descriptionColumn.Width = 300;
            dataGridView.Columns.Add(descriptionColumn);

            // 
            // totalAmountColumn
            // 
            var totalAmountColumn = new DataGridViewTextBoxColumn();
            totalAmountColumn.DataPropertyName = "TotalAmount";
            totalAmountColumn.DefaultCellStyle.Format = "c";
            totalAmountColumn.Frozen = true;
            totalAmountColumn.HeaderText = "Total";
            totalAmountColumn.Name = "totalAmountColumn";
            totalAmountColumn.Width = 60;
            totalAmountColumn.ReadOnly = true;
            dataGridView.Columns.Add(totalAmountColumn);

            // 
            // readOnlyColumn
            // 
            var readOnlyColumn = new DataGridViewTextBoxColumn();
            readOnlyColumn.DataPropertyName = "ReadOnly";
            readOnlyColumn.Name = "readOnlyColumn";
            readOnlyColumn.Visible = false;
            readOnlyColumn.ReadOnly = true;
            dataGridView.Columns.Add(readOnlyColumn);

            foreach ( var category in _paycheckPeriodBudget.CategoryOrder )
            {
                // 
                // categoryColumn
                // 
                var categoryColumn = new DataGridViewTextBoxColumn();
                categoryColumn.DataPropertyName = category;
                categoryColumn.DefaultCellStyle.Format = "c";
                categoryColumn.Frozen = false;
                categoryColumn.HeaderText = category;
                categoryColumn.Name = "categoryColumn";
                categoryColumn.Width = category == "nowork" ? 75 : 60;
                dataGridView.Columns.Add(categoryColumn);
            }
        }

        private DataTable ConvertToDataTable(List<BudgetLine> lineItems)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Description", typeof(string));
            foreach (var category in _paycheckPeriodBudget.CategoryOrder)
            {
                table.Columns.Add(category, typeof(decimal));
            }
            table.Columns.Add("TotalAmount", typeof(decimal));
            table.Columns.Add("ReadOnly", typeof(bool));

            AddRowToGrid(table, _paycheckPeriodBudget.StartingBudget);
            foreach ( var item in _paycheckPeriodBudget.LineItems )
            {
                AddRowToGrid(table, item);
            }
            AddRowToGrid(table, _paycheckPeriodBudget.TotalBudget);
            AddRowToGrid(table, _paycheckPeriodBudget.PaycheckBudget);
            AddRowToGrid(table, _paycheckPeriodBudget.EndingBudget);

            return table;
        }

        private void AddRowToGrid(DataTable table, BudgetLine lineItem)
        {
            var row = table.Rows.Add();
            row["Date"] = lineItem.Date;
            row["Description"] = lineItem.Description;
            foreach (var category in _paycheckPeriodBudget.CategoryOrder)
            {
                if (lineItem.CategoryAmount.ContainsKey(category))
                {
                    row[category] = lineItem.CategoryAmount[category];
                }
            }
            row["TotalAmount"] = lineItem.TotalAmount;
            row["ReadOnly"] = lineItem.ReadOnly;
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_budgetDataTable.Rows.Count > e.RowIndex)
            {
                var column = _budgetDataTable.Columns[e.ColumnIndex];
                var row = _budgetDataTable.Rows[e.RowIndex];
                var value = row[column.ColumnName];
                var lineItem = _paycheckPeriodBudget.LineItems[e.RowIndex];

                switch (column.ColumnName)
                {
                    case "Date":
                        lineItem.Date = (DateTime)value;
                        break;

                    case "Description":
                        lineItem.Description = value as string;
                        break;

                    default: //must be a category column
                        lineItem.CategoryAmount[column.ColumnName] = (decimal)value;
                        break;
                }
            }
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if ( e.Context == DataGridViewDataErrorContexts.Parsing ||
                 e.Context == DataGridViewDataErrorContexts.Formatting )
            {
                MessageBox.Show("Error happened " + e.Context.ToString());
                e.ThrowException = false;
            }
        }

        internal void Save()
        {
            _budgetFile.WriteToFile(_paycheckPeriodBudget, "test.txt"); //TODO: for testing only
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_budgetDataTable != null && _budgetDataTable.Rows.Count > e.RowIndex)
            {
                var dataRow = _budgetDataTable.Rows[e.RowIndex];
                var isReadOnly = (bool)dataRow["ReadOnly"];
                if (isReadOnly)
                {
                    e.CellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_budgetDataTable != null && _budgetDataTable.Rows.Count > e.RowIndex)
            {
                var dataRow = _budgetDataTable.Rows[e.RowIndex];
                var isReadOnly = (bool)dataRow["ReadOnly"];
                var grid = (DataGridView)sender;
                if (isReadOnly)
                {
                    grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    e.Cancel = true;
                }
                else
                {
                    grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
            }
        }
    }
}
