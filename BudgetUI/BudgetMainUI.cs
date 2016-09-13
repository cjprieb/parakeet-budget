using Budget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgetUI
{
    public partial class BudgetMainUI : Form
    {
        public BudgetMainUI()
        {
            InitializeComponent();
            Initialize();
        }

        #region Event Handlers...
        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentTab();
        }

        private void generateNextPaycheckPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNextPaycheckPeriod();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewBudgetFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBudgetFile();
        }

        private void previousPaycheckPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPreviousPaycheckPeriod();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentTab();
        }

        private void toolStripButtonEditCategories_Click(object sender, EventArgs e)
        {
            OpenEditCategoriesDialog();
        }

        private void toolStripButtonNextPaycheckPeriod_Click(object sender, EventArgs e)
        {
            OpenNextPaycheckPeriod();
        }

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenBudgetFile();
        }

        private void toolStripButtonPreviousPaycheckPeriod_Click(object sender, EventArgs e)
        {
            OpenPreviousPaycheckPeriod();
        }

        private void toolStripButtonRecalculatePaycheckPeriod_Click(object sender, EventArgs e)
        {
            RecalculateCurrentBudgetFile();
        }

        private void toolStripButtonSaveFile_Click(object sender, EventArgs e)
        {
            SaveCurrentTab();
        }
        #endregion Event Handlers...

        #region Private methods...
        private void CloseCurrentTab()
        {
            //TODO display confirm dialog if changes were made
            var selectedIndex = tabControlBudgetFiles.SelectedIndex;
            if ( tabControlBudgetFiles.TabCount > selectedIndex+1 )
            {
                selectedIndex++;
            }
            else if ( tabControlBudgetFiles.TabCount == 1 )
            {
                selectedIndex = -1;
                EnableButtons(false);
            }
            tabControlBudgetFiles.TabPages.Remove(tabControlBudgetFiles.SelectedTab);
            tabControlBudgetFiles.SelectedIndex = selectedIndex;
        }

        private void CreateNewBudgetFile()
        {
            throw new NotImplementedException();
        }

        private void Initialize()
        {
            openFileDialog.InitialDirectory = @"C:\Users\Crystal\Documents\budget";
            EnableButtons(false);
        }

        private void OpenBudgetFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenBudgetTab(openFileDialog.FileName);
            }
        }

        private void OpenBudgetTab(string selectedFile)
        {
            try
            {
                var tabPanel = new BudgetFileUI(selectedFile);
                tabPanel.Dock = DockStyle.Fill;

                var tabPage = new TabPage(Path.GetFileName(selectedFile));
                tabPage.Controls.Add(tabPanel);

                tabControlBudgetFiles.TabPages.Add(tabPage);
                tabControlBudgetFiles.SelectedTab = tabPage;
                Text = selectedFile;

                EnableButtons(true);
            }
            catch (Exception ex)
            {
                //TODO: nlog error message
                var caption = "Unable to load file";
                var message = $"An error occurred while loading { selectedFile }: { ex.Message }";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableButtons(bool enableIt)
        {
            saveToolStripMenuItem.Enabled = enableIt;
            toolStripButtonSaveFile.Enabled = enableIt;

            editCategoriesToolStripMenuItem.Enabled = enableIt;
            toolStripButtonEditCategories.Enabled = enableIt;

            generateNextPaycheckPeriodToolStripMenuItem.Enabled = enableIt;
            toolStripButtonNextPaycheckPeriod.Enabled = enableIt;

            previousPaycheckPeriodToolStripMenuItem.Enabled = enableIt;
            toolStripButtonPreviousPaycheckPeriod.Enabled = enableIt;

            toolStripButtonRecalculatePaycheckPeriod.Enabled = enableIt;

            closeFileToolStripMenuItem.Enabled = enableIt;
        }

        private void OpenEditCategoriesDialog()
        {
            throw new NotImplementedException();
        }

        private void OpenNextPaycheckPeriod()
        {
            throw new NotImplementedException();
        }

        private void OpenPreviousPaycheckPeriod()
        {
            throw new NotImplementedException();
        }

        private void RecalculateCurrentBudgetFile()
        {
            throw new NotImplementedException();
        }

        private void SaveCurrentTab()
        {
            var budgetTab = tabControlBudgetFiles.SelectedTab.Controls[0] as BudgetFileUI;
            if ( budgetTab != null )
            {
                budgetTab.Save();
            }
        }
        #endregion Private methods...
    }
}
