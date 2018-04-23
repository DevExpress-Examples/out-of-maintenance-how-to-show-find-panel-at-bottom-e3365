using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GridControlWithFindPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Random rnd = new Random();
            DataTable table = new DataTable();
            table.Columns.Add("Value1");
            table.Columns.Add("Value2");
            for (int i = 0; i < rnd.Next(100); i++)
            {
                table.Rows.Add(new object[] { "Custom" + rnd.Next(15), "Address" + rnd.Next(20) });
            }
            myGridControl1.DataSource = table;
            checkBox1.Checked = myGridView1.ShowFindPanelOnTop;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            myGridView1.ShowFindPanelOnTop = checkBox1.Checked;
        }
    }
}
