using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TMSI_MFLD.Controls.Load
{
    /// <summary>
    /// 绘制虚线边框
    /// </summary>
    [Description("绘制虚线边框")]
    public class DottedLineBorderExtDesigner : ControlDesigner
    {
        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            base.OnPaintAdornments(pe);
            this.DrawBorder(pe.Graphics);
        }

        /// <summary>
        /// 绘制虚线边框
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawBorder(Graphics graphics)
        {
            Control control = this.Control;
            Rectangle clientRectangle = control.ClientRectangle;
            Pen pen = new Pen((double)control.BackColor.GetBrightness() >= 0.5 ? ControlPaint.Dark(control.BackColor) : ControlPaint.Light(control.BackColor));
            pen.DashStyle = DashStyle.Dash;
            --clientRectangle.Width;
            --clientRectangle.Height;
            graphics.DrawRectangle(pen, clientRectangle);
            pen.Dispose();
        }
    }
}
