namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class ChartBrushes : ChartObj
    {
        public ArrayList brushCache = new ArrayList();
        public QBrush[] currentBrush = new QBrush[2];

        public ChartBrushes()
        {
            base.chartObjType = 0x28b;
        }

        public override object Clone()
        {
            ChartBrushes brushes = new ChartBrushes();
            brushes.Copy(this);
            return brushes;
        }

        public void Copy(ChartBrushes source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.brushCache = (ArrayList) source.brushCache.Clone();
            }
        }

        public QBrush GetBrush(Color brushcolor)
        {
            SolidBrush theBrush = null;
            QBrush brush2 = null;
            bool flag = false;
            bool flag2 = false;
            if (brushcolor.Equals(this.currentBrush[0]))
            {
                brush2 = this.currentBrush[0];
                flag = true;
            }
            else if (brushcolor.Equals(this.currentBrush[1]))
            {
                brush2 = this.currentBrush[1];
                flag = true;
            }
            else
            {
                for (int i = this.brushCache.Count - 1; i >= 0; i--)
                {
                    brush2 = (QBrush) this.brushCache[i];
                    if (brushcolor.Equals(brush2.BrushColor))
                    {
                        flag = true;
                        flag2 = true;
                        theBrush = brush2.TheBrush;
                        break;
                    }
                }
            }
            if (!flag)
            {
                theBrush = new SolidBrush(brushcolor);
                brush2 = new QBrush(theBrush);
                this.brushCache.Add(brush2);
                flag2 = true;
            }
            if (flag2)
            {
                this.currentBrush[1] = this.currentBrush[0];
                this.currentBrush[0] = brush2;
            }
            return brush2;
        }
    }
}

