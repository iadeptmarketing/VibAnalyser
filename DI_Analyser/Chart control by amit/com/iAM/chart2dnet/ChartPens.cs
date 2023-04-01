namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ChartPens : ChartObj
    {
        public QPen[] currentPen = new QPen[2];
        public ArrayList penCache = new ArrayList();

        public ChartPens()
        {
            base.chartObjType = 0x28c;
        }

        public override object Clone()
        {
            ChartPens pens = new ChartPens();
            pens.Copy(this);
            return pens;
        }

        public void Copy(ChartPens source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.penCache = (ArrayList) source.penCache.Clone();
            }
        }

        public QPen GetPen(Color pencolor, float penwidth, DashStyle penstyle)
        {
            Pen thePen = null;
            QPen qpen = null;
            bool flag = false;
            bool flag2 = false;
            if (this.PenFound(this.currentPen[0], pencolor, penwidth, penstyle))
            {
                qpen = this.currentPen[0];
                flag = true;
            }
            else if (this.PenFound(this.currentPen[1], pencolor, penwidth, penstyle))
            {
                qpen = this.currentPen[1];
                flag = true;
            }
            else
            {
                for (int i = this.penCache.Count - 1; i >= 0; i--)
                {
                    qpen = (QPen) this.penCache[i];
                    if (this.PenFound(qpen, pencolor, penwidth, penstyle))
                    {
                        flag = true;
                        thePen = qpen.ThePen;
                        flag2 = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                thePen = new Pen(pencolor, penwidth);
                if (penstyle != DashStyle.Solid)
                {
                    thePen.DashStyle = penstyle;
                }
                qpen = new QPen(thePen);
                this.penCache.Add(qpen);
                flag2 = true;
            }
            if (flag2)
            {
                this.currentPen[1] = this.currentPen[0];
                this.currentPen[0] = qpen;
            }
            return qpen;
        }

        public bool PenFound(QPen qpen, Color pencolor, float penwidth, DashStyle penstyle)
        {
            bool flag = false;
            if (((qpen != null) && pencolor.Equals(qpen.PenColor)) && ((Math.Abs((float) (penwidth - qpen.Width)) < 0.1) && (penstyle == qpen.DashStyle)))
            {
                flag = true;
            }
            return flag;
        }
    }
}

