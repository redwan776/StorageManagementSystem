using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Exp_Reports
{
    public partial class Form22 : Form
    {
        BindingSource ff3 = new BindingSource();

        DataTable dataTable = new DataTable();
        public DataTable get_data_to_dataGridVeiw()
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add("الرقم");
            dt.Columns.Add("الاسم");
            dt.Columns.Add("المبلغ");
            DataRow dr = dt.NewRow();
            dr["الرقم"] = 55;
            dr["الاسم"] = "علي";
            dr["المبلغ"] = 500;
            DataRow dr2 = dt.NewRow();
            dt.Rows.Add(dr);
            dr2["الرقم"] = 44;
            dr2["الاسم"] = "سعيد";
            dr2["المبلغ"] = 0;
            dt.Rows.Add(dr2);
            DataTable ff = new DataTable();
            ff = dt.Select("[المبلغ]>0").CopyToDataTable();
            return dt;
        }

        public Form22()
        {
            InitializeComponent();
            dataGridView1.DataSource = get_data_to_dataGridVeiw();
            dataGridView1.Columns.Add("all", "all");
            DataTable gg = new DataTable();
            gg = (DataTable)(dataGridView1.DataSource);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                //dataTable.Columns.Add(column.HeaderText, column.ValueType);
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable ddd2 = new DataTable();

            DataTable ddd = new DataTable();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                ddd.Columns.Add(column.HeaderText, column.ValueType);
                ddd2.Columns.Add(column.HeaderText, column.ValueType);
            }

            ff3.DataSource = dataGridView1.DataSource;
            dataGridView1.DataSource = ff3.DataSource;
            ff3.Filter = "[المبلغ]>0";
    

            HashSet<DataRow> importedRows = new HashSet<DataRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // تجاوز الصفوف التي ليست صفوف بيانات
                if (!row.IsNewRow)
                {
                    bool shouldImportRow = false; // مؤشر لتحديد ما إذا كان يجب استيراد الصف أم لا

                    // قم بتعديل الرقم 2 ليتناسب مع العمود الذي تحتاجه
                    int cellValue = Convert.ToInt32(row.Cells[3].Value);
                    if (cellValue > 0)
                    {
                        shouldImportRow = true;
                    }

                    if (shouldImportRow)
                    {
                        DataRow dataRow = ddd2.NewRow();

                        for (int columnIndex = 0; columnIndex < dataGridView1.Columns.Count; columnIndex++)
                        {
                            dataRow[columnIndex] = row.Cells[columnIndex].Value;
                        }

                        if (!importedRows.Contains(dataRow))
                        {
                            ddd2.Rows.Add(dataRow);
                            importedRows.Add(dataRow);
                        }
                    }
                }
            }

            dataGridView2.DataSource = ddd2;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindingSource fff = new BindingSource();
            fff.DataSource = ff3.DataSource;
           
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
            if (columnName == "المبلغ")
            {
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                Decimal x;
                if (string.IsNullOrEmpty(selectedRow.Cells["المبلغ"].Value.ToString()))
                {
                   x=0;
                }
                else{
                   x= Convert.ToDecimal(selectedRow.Cells["المبلغ"].Value);
                }
                if ( x> 0)
                {
                    DataRow newRow = dataTable.NewRow();
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        newRow[cell.ColumnIndex] = cell.Value;
                    }
                    dataTable.Rows.Add(newRow);
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
            if (columnName == "المبلغ")
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += dataGridView1_KeyPres;
                }
            }
        }
        private void dataGridView1_KeyPres(object sender, KeyPressEventArgs e)
        {
            string columnName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
            if (columnName == "المبلغ")
            {
                if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
          
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime startTime;
            DateTime endTime;
            int startHour;
            int endHour;
            startTime = dateTimePicker1.Value;
            endTime = dateTimePicker2.Value;
            //startHour = startTime.Hour;
            //  endHour = endTime.Hour;
              TimeSpan timdef = calculate(startTime, endTime);
              MessageBox.Show("" + timdef.ToString(), "");
        }
        static TimeSpan calculate(DateTime star, DateTime end)
         {
             if (end.TimeOfDay < star.TimeOfDay && end.TimeOfDay.Hours >= 0 && end.TimeOfDay.Hours < 12) {
                 end = end.AddDays(1);
             }
             TimeSpan timdef = end - star;
            //int houresdiff = (int)timdef.TotalHours;
            //if (timdef.TotalHours < 0) {
            //    timdef.Add(TimeSpan.FromHours(24));
            //}
            return timdef;
        }
    }
}
