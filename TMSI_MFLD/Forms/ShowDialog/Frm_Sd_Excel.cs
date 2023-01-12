using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TMSI_MFLD.Forms.ShowDialog.FrmMode;

namespace TMSI_MFLD.Forms.ShowDialog
{
    public partial class Frm_Sd_Excel : FrmCircleBase
    {
        public Frm_Sd_Excel(FrmModeExcel frmModeExcel)
        {
            InitializeComponent();
            //不显示ok按钮
            this.BtnOkShow = true;
            this.btnCancel.BtnText = StringParser.Parse(ResourceService.GetString("cancel"));
            //this.btnOK.BtnText = StringParser.Parse(ResourceService.GetString("ok"));
            //显示文字
            if (frmModeExcel == FrmModeExcel.Success) this.lab_excel.Text = StringParser.Parse(ResourceService.GetString("Excel export success"));
            else if (frmModeExcel == FrmModeExcel.Failure) this.lab_excel.Text = StringParser.Parse(ResourceService.GetString("Excel export failure"));
            else if (frmModeExcel == FrmModeExcel.Csv) this.lab_excel.Text = StringParser.Parse(ResourceService.GetString("Rawdata export success"));
        }

        private void lab_excel_TextChanged(object sender, EventArgs e)
        {
            if(this.lab_excel.Text == "Excel export success" && this.lab_excel.Text == "Excel export failure")
            {
                this.lab_excel.Location = new Point(100, 95);
            }
            else
            {
                this.lab_excel.Location = new Point(85, 95);
            }
        }
    }
}
