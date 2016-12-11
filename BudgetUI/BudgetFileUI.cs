using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Willowcat.Budget;
using Willowcat.Budget.Models;
using System.Linq;

namespace Willowcat.BudgetUi
{
    public partial class BudgetFileUI : UserControl
    {
        private PaycheckPeriodBudget _paycheckPeriodBudget;
        private BudgetTextParser _budgetFile;
        private DataTable _budgetDataTable;
        private string _selectedFile;

        public BudgetFileUI()
        {
            InitializeComponent();
        }

        public BudgetFileUI(string selectedFile)
        {
            _selectedFile = selectedFile;
            _budgetFile = new BudgetTextParser();

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //TODO: do on async
            _paycheckPeriodBudget = _budgetFile.ParseFile(_selectedFile);

            SetupColumns();
            RefreshDataTable(_paycheckPeriodBudget.LineItems);
        }

        private void SetupColumns()
        {
            //To rotate columns 90deg: http://our.componentone.com/groups/topic/rotate-text-vertical-in-datagrid-column-headers/

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
            totalAmountColumn.Width = 70;
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

            // 
            // guidColumn
            // 
            var guidColumn = new DataGridViewTextBoxColumn();
            guidColumn.DataPropertyName = "Guid";
            guidColumn.Name = "guidColumn";
            guidColumn.Visible = false;
            guidColumn.ReadOnly = true;
            dataGridView.Columns.Add(guidColumn);

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
                categoryColumn.Name = category + "Column";
                categoryColumn.Width = category == "nowork" ? 75 : 60;
                dataGridView.Columns.Add(categoryColumn);
            }
        }

        private void RefreshDataTable(List<BudgetLine> lineItems)
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
            table.Columns.Add("Guid", typeof(Guid));

            AddRowToGrid(table, _paycheckPeriodBudget.StartingBudget);
            foreach ( var item in _paycheckPeriodBudget.LineItems )
            {
                AddRowToGrid(table, item);
            }
            AddRowToGrid(table, _paycheckPeriodBudget.PayPeriodIncome);
            AddRowToGrid(table, _paycheckPeriodBudget.PayPeriodExpenses);
            AddRowToGrid(table, _paycheckPeriodBudget.PaycheckBudget);
            AddRowToGrid(table, _paycheckPeriodBudget.EndingBudget);

            _budgetDataTable = table;
            dataGridView.DataSource = table;
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
            row["Guid"] = lineItem.Guid;
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGrid = sender as DataGridView;
            if (dataGrid != null && dataGrid.Rows.Count > e.RowIndex)
            {
                var column = dataGrid.Columns[e.ColumnIndex];
                var row = dataGrid.Rows[e.RowIndex];
                var value = row.Cells[column.Name].Value;
                var guid = (Guid)row.Cells["guidColumn"].Value;

                var lineItem = _paycheckPeriodBudget.LineItems.FirstOrDefault(item => item.Guid == guid);

                if (lineItem != null)
                {
                    switch (column.DataPropertyName)
                    {
                        case "Date":
                            lineItem.Date = (DateTime)value;
                            break;

                        case "Description":
                            lineItem.Description = value as string;
                            break;

                        default: //must be a category column
                            lineItem.CategoryAmount[column.DataPropertyName] = (decimal)value;
                            break;
                    }

                    RefreshDataTable(_paycheckPeriodBudget.LineItems);
                }
                else
                {
                    MessageBox.Show($"Error! Unable to find matching line item for row {e.RowIndex}.");
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
