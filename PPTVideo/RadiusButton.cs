
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
 
namespace PPTVideo
{
     class RadiusButton : Button
    {
        /// <summary>
        /// 是否圆角
        /// </summary>
        private bool _isRoundCorner = false;
        /// <summary>
        /// 是否圆角
        /// </summary>
        [Description("是否圆角"), Category("自定义")]
        public virtual bool IsRoundCorner
        {
            get
            {
                return _isRoundCorner;
            }
            set
            {
                _isRoundCorner = value;
                Refresh();
            }
        }
        /// <summary>
        /// 圆角半径
        /// </summary>
        private int _roundRadius = 24;

        /// <summary>
        /// 圆角半径
        /// </summary>
        [Description("圆角半径"), Category("自定义")]
        public virtual int RoundRadius
        {
            get
            {
                return _roundRadius;
            }
            set
            {
                if (value < 1)//圆角半径最小为1
                {
                    value = 1;
                }
                _roundRadius = value;
                Refresh();
            }
        }
 
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Visible)
            {
                if (_isRoundCorner)
                {
                    PaintRoundCorner();
                }
                else
                {
                    //关闭圆角后显示为原矩形
                    GraphicsPath g = new GraphicsPath();
                    g.AddRectangle(base.ClientRectangle);//添加一个矩形
                    g.CloseFigure();//闭合当前图形并开始新的图形
                    base.Region = new Region(g);
                }
            }
            base.OnPaint(e);
        }
        /// <summary>
        /// 绘制圆角
        /// </summary>
        private void PaintRoundCorner()
        {
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            Rectangle rect2 = new Rectangle(rect.Location, new Size(_roundRadius, _roundRadius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//左上角
            rect2.X = rect.Right - _roundRadius;
            graphicsPath.AddArc(rect2, 270f, 90f);//右上角
            rect2.Y = rect.Bottom - _roundRadius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//右下角           
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//左下角
            graphicsPath.CloseFigure();
            base.Region = new Region(graphicsPath);
        }
        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        void SetGDIHigh(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }



    }


}
 
