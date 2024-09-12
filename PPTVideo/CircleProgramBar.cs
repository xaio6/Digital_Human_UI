using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPTVideo
{
    public class CircleProgramBar : Control
    {
        private Pen penBottom = null;//底层画笔
        private Pen penTop = null;//上层画笔
        private Pen penFinished = null;//√画笔


        //解决控件批量更新时带来的闪烁
        protected override CreateParams CreateParams { get { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }

        /// <summary>
        /// /圆形进度条
        /// </summary>
        public CircleProgramBar()
        {
            InitControl();
            //初始化变量
            this.penBottom = new Pen(this.bottomColor, 5);
            this.penTop = new Pen(this.topColor, 5);
            this.penFinished = new Pen(this.finishedColor, 5);

            this.SizeChanged += delegate
            {
                this.Invalidate();
            };
        }
        /// <summary>
        /// 初始化控件参数
        /// </summary>
        private void InitControl()
        {
            this.Width = 1000;
            this.Height = 1000;
            this.BackColor = Color.White;
        }

        #region 属性
        //-------------------值
        private int maxValue = 100;
        private int progress = 0;

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                if (value < this.progress)
                {
                    return;
                }
                this.maxValue = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 进度值
        /// </summary>
        public int Progress
        {
            get { return this.progress; }
            set
            {
                if (value > this.maxValue)
                {
                    return;
                }
                this.progress = value;
                this.Invalidate();
            }
        }

        //-------------------颜色
        private Color bottomColor = Color.FromArgb(224, 224, 224);
        private Color topColor = Color.FromArgb(109, 179, 63);
        private Color finishedColor = Color.FromArgb(109, 179, 63);

        public Color BottomColor
        {
            get { return this.bottomColor; }
            set
            {
                this.bottomColor = value;
                this.penBottom.Color = value;
                this.Invalidate();
            }
        }
        public Color TopColor
        {
            get { return this.topColor; }
            set
            {
                this.topColor = value;
                this.penTop.Color = value;
                this.Invalidate();
            }
        }
        public Color FinishedColor
        {
            get { return this.finishedColor; }
            set
            {
                this.finishedColor = value;
                this.penFinished.Color = value;
                this.Invalidate();
            }
        }
        #endregion

        //对Control进行绘制
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            DrawShape(e.Graphics);
        }

        private void DrawShape(Graphics g)
        {
            if (this.Width < 100 || this.Height < 100)
            {
                return;
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            //1、绘制背景
            g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
            int size = Math.Min(this.Width, this.Height);//圆的大小
            //2、绘制底层（最大值）
            int sizeOffset = 30;//进度条距离最外侧的偏移量
            Rectangle rectangle = new Rectangle(this.Width / 2 - size / 2 + sizeOffset, this.Height / 2 - size / 2 + sizeOffset, size - (sizeOffset * 2), size - (sizeOffset * 2));//计算圆的范围
            g.DrawArc(this.penBottom, rectangle, 0, 360);//绘制底层条
            //g.DrawRectangle(this.penBottom, rectangleBottom);//绘制标识测试区
            //3、绘制上层圆（进度条）
            decimal topAngle = (this.progress * 1.0M / this.maxValue) * 360M;//计算进度条划过的度数
            g.DrawArc(this.penTop, rectangle, 0, (int)topAngle);//绘制进度条
            //4、绘制进度显示值
            if (this.progress == maxValue)
            {
                //绘制完成标志
                int lineOffset = (int)(size * (1 / 9.5M));//对勾交叉点距离中心点的偏移量

                //计算左半对勾的结束位置
                double x1 = (this.Width / 2) + (size * (1 / 8D)) * Math.Cos((90 * 2 + 90 / 2D) * Math.PI / 180);
                double y1 = (this.Height / 2 + lineOffset) + (size * (1 / 8D)) * Math.Sin((90 * 2 + 90 / 2D) * Math.PI / 180);

                //计算右半对勾的结束位置
                double x2 = (this.Width / 2) + (size * (1 / 4D)) * Math.Cos((90 * 3 + 90 / 2D) * Math.PI / 180);
                double y2 = (this.Height / 2 + lineOffset) + (size * (1 / 4D)) * Math.Sin((90 * 3 + 90 / 2D) * Math.PI / 180);

                //绘制对勾
                var lines = new Point[] {
                    new Point((int)x1,(int)y1),
                    new Point((this.Width / 2 ) , (this.Height / 2 )+ lineOffset),
                    new Point((int)x2,(int)y2) };

            }

        }


    }
}
