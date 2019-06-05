using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static Form1 CreateForm1()
        {
            foreach (Form frm in Application.OpenForms)
                if (frm is Form1)
                {
                    frm.Activate();
                    return frm as Form1;
                }
            Form1 form = new Form1();
            form.Show();
            return form;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            CreateForm1();
            Form1 f1 = new Form1();
            f1.gMapControl1.Visible = true;


        }
    }
}
