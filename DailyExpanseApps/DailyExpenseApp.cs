using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSVLib;

namespace DailyExpanseApps2
{
    public partial class DailyExpenseApp : Form
    {
        private string fileLocation = @"DailyExpense.csv";
        public DailyExpenseApp()
        {
            InitializeComponent();
        }


        List<string> dailyList = new List<string>();

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (amountTextBox.Text!=string.Empty && categoryComboBox.Text!=string.Empty && particularTextBox.Text!=string.Empty)
            {
                double amount = Convert.ToDouble(amountTextBox.Text);
                string category = categoryComboBox.Text;
                string particular = particularTextBox.Text;
                dailyList.Add(amount.ToString());
                dailyList.Add(category);
                dailyList.Add(particular);

                FileStream aFileStream = new FileStream(fileLocation, FileMode.Append);
                CsvFileWriter aWriter = new CsvFileWriter(aFileStream);

                aWriter.WriteRow(dailyList);
                aFileStream.Close();
                MessageBox.Show("Your Expanse Has Been Saved");
            }
            else
            {
                MessageBox.Show("Plz Enter All Value");
            }
            amountTextBox.Text = string.Empty;
            categoryComboBox.Text = string.Empty;
            particularTextBox.Text = string.Empty;


        }

        private void summaryShowButton_Click(object sender, EventArgs e)
        {
            FileStream aFileStream = new FileStream(fileLocation,FileMode.Open);
            CsvFileReader aReader = new CsvFileReader(aFileStream);
            List<string> arecodList = new List<string>();
            List<double> maximum = new List<double>();
            double totalExpanse = 0;
          
            while (aReader.ReadRow(arecodList))
            {
                string expanse = arecodList[0];
                totalExpanse +=Convert.ToDouble(expanse);
                maximum.Add(Convert.ToDouble(expanse));
            }
            double Amount=maximum.Max();
            totalExpenseTextBox.Text = totalExpanse.ToString();
            maximumExpenseTextBox.Text =Amount.ToString();
            aFileStream.Close();
        }

        private void expenseShowButton_Click(object sender, EventArgs e)
        {
            if (expenseCetagoryComboBox.Text != string.Empty)
            {
                FileStream afileStream = new FileStream(fileLocation, FileMode.Open);
                CsvFileReader aReader = new CsvFileReader(afileStream);
                List<string> arecodList = new List<string>();
                double totalAmount = 0;

                while (aReader.ReadRow(arecodList))
                {
                    if (arecodList[1] == expenseCetagoryComboBox.Text)
                    {
                        listView1.Items.Clear();
                        ListViewItem item = new ListViewItem(arecodList[0]);
                        item.SubItems.Add(arecodList[2]);
                        listView1.Items.Add(item);
                        totalAmount += Convert.ToDouble(arecodList[0]);

                    }

                }
                totalTextBox.Text = totalAmount.ToString();
                afileStream.Close();
            }
            else
            {
                MessageBox.Show("Plz Select Cetacory");
            }
        }
        
    }
}
