using FormControls.Forms;
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

namespace TMSI_MFLD.Forms.ShowDialog
{
    public partial class FrmCircleBase : FrmBase
    {
        #region 字段定义

        #endregion

        #region 属性定义

        [Description("确定按钮颜色"), Category("自定义")]
        public Color ThisOkBtn
        {
            get
            {
                return this.btnOK.BackColor;
            }
            set
            {
                this.btnOK.BackColor = value;
            }
        }

        [Description("取消按钮颜色"), Category("自定义")]
        public Color ThisCloseBtn
        {
            get
            {
                return this.btnCancel.BackColor;
            }
            set
            {
                this.btnCancel.BackColor = value;
            }
        }

        [Description("窗体标题"), Category("自定义")]
        public string FrmName
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }

        public DialogResult ThisDialogResult
        {
            get
            {
                return this.DialogResult;
            }
        }

        [Description("ok按钮是否显示"), Category("自定义")]
        public bool BtnOkShow
        {
            get
            {
                return this.btnOK.Visible;
            }
            set
            {
                this.btnOK.Visible = value;
            }
        }

        [Description("ok按钮名称"), Category("自定义")]
        public string  BtnOkName
        {
            get
            {
                return this.btnOK.BtnText;
            }
            set
            {
                this.btnOK.BtnText = value;
            }
        }

        [Description("Cancel按钮名称"), Category("自定义")]
        public string BtnCloseShow
        {
            get
            {
                return this.btnCancel.BtnText;
            }
            set
            {
                this.btnCancel.BtnText = value;
            }
        }
        #endregion

        #region 构造函数

        public FrmCircleBase()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件定义

        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}
