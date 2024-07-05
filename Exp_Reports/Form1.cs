using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
namespace Exp_Reports
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Server=.\MSSQLSERVR2 ; Database=redw; Integrated Security=True");
        SqlDataAdapter da ;
        DataView dv;
        SqlCommand cmd ;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reportPath = "D:/report1/CrystalReport1.rpt";
            ReportDocument rd=new ReportDocument();
            rd.Load(reportPath);
            DataSet1 ds1 = new DataSet1();
            da = new SqlDataAdapter("select * from ITEMS", con);
            da.Fill(ds1,"INVOICE");
            dv = new DataView(ds1.Tables["INVOICE"]);
            rd.SetDataSource(dv);
            Frm_PrintReports frm = new Frm_PrintReports();
            frm.crystalReportViewer1.ReportSource = rd;
            frm.Show();
            frm.crystalReportViewer1.Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
