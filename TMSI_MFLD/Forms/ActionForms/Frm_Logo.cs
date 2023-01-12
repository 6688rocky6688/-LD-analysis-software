using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSI_MFLD.Forms.ActionForms
{
    public partial class Frm_Logo : UIPage
    {
        public Frm_Logo()
        {
            InitializeComponent();
            this.imageWhirligigExt1.Play();
        }

        private void imageWhirligigExt1_Paint(object sender, PaintEventArgs e)
        {
            Size size = this.Size;
            this.imageWhirligigExt1.ImageFrameWidth = size.Width;
            this.imageWhirligigExt1.ImageFrameHeight = size.Height;
        }
    }
}
