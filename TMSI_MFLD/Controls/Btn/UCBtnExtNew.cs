using FunctionClassLibrary.Controls.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSI_MFLD.Controls.Btn
{
    public partial class UCBtnExtNew : UCControlBase
    {
        #region 字段定义

        /// <summary>
        /// 按钮背景色
        /// </summary>
        private Color _btnBackColor = Color.Red;

        /// <summary>
        ///按钮字体颜色
        /// </summary>
        private Color _btnForeColor = Color.White;

        /// <summary>
        /// 按钮字体
        /// </summary>
        private Font _btnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        /// <summary>
        /// 按钮文字
        /// </summary>
        private string _btnText;

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        [Description("按钮点击事件"), Category("自定义")]
        public event EventHandler BtnClick;

        #endregion

        #region 属性定义

        /// <summary>
        /// 按钮背景色
        /// </summary>
        /// <value>The color of the BTN back.</value>
        [Description("按钮背景色"), Category("自定义")]
        public Color BtnBackColor
        {
            get { return _btnBackColor; }
            set
            {
                _btnBackColor = value;
                this.BackColor = value;
            }
        }

        /// <summary>
        /// 按钮字体颜色
        /// </summary>
        [Description("按钮字体颜色"), Category("自定义")]
        public virtual Color BtnForeColor
        {
            get { return _btnForeColor; }
            set
            {
                _btnForeColor = value;
                this.lbl.ForeColor = value;
            }
        }

        /// <summary>
        /// 按钮字体
        /// </summary>
        [Description("按钮字体"), Category("自定义")]
        public Font BtnFont
        {
            get { return _btnFont; }
            set
            {
                _btnFont = value;
                this.lbl.Font = value;
            }
        }

        /// <summary>
        /// 按钮文字
        /// </summary>
        /// <value>The BTN text.</value>
        [Description("按钮文字"), Category("自定义")]
        public virtual string BtnText
        {
            get { return _btnText; }
            set
            {
                _btnText = value;
                lbl.Text = value;
            }
        }

        #endregion

        #region 构造函数

        public UCBtnExtNew()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件定义

        //变色美观
        private void lbl_MouseEnter(object sender, System.EventArgs e)
        {
            if (BackColor != Color.Empty && BackColor != null)
            {
                this.BackColor = this.BackColor.ChangeColor(-0.2f);
            }
        }

        private void lbl_MouseLeave(object sender, System.EventArgs e)
        { 
            if (BackColor != Color.Empty && BackColor != null)
            {
                this.BackColor = this._btnBackColor;
            }
        }

        private void lbl_Click(object sender, System.EventArgs e)
        {
            if (this.BtnClick != null)
            {
                BtnClick(this, e);
            }
        }

        #endregion
    }
}
