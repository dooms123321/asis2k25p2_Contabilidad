using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_VisorReporte_BalanceDeSaldos : Form
    {
        public Frm_VisorReporte_BalanceDeSaldos()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Rockwell", 10);
        }
    }
}
