namespace com.quinncurtis.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class ChartPrint : ChartObj
    {
        private int chartcounter;
        internal ChartView[] chartViewArray;
        internal bool doubleBuffer;
        internal bool printBackgroundEnable;
        internal bool printBorderEnable;
        internal ChartView printChartView;
        internal bool printComponentsEnable;
        internal bool printDialogFlag;
        private PrintDocument printDoc;
        private PageSettings printPageSettings;
        internal Rectangle2D printRect;
        internal Rectangle2D[] printRectArray;
        internal int printSizeMode;

        public ChartPrint()
        {
            this.printChartView = null;
            this.chartViewArray = null;
            this.printRectArray = null;
            this.printDoc = new PrintDocument();
            this.printPageSettings = new PageSettings();
            this.printSizeMode = 2;
            this.printRect = new Rectangle2D(0.3, 0.3, 0.4, 0.4);
            this.printDialogFlag = false;
            this.doubleBuffer = true;
            this.printBackgroundEnable = true;
            this.printComponentsEnable = true;
            this.printBorderEnable = true;
            this.chartcounter = 0;
            this.printDoc.PrintPage += new PrintPageEventHandler(this.PrintView);
        }

        public ChartPrint(ChartView component)
        {
            this.printChartView = null;
            this.chartViewArray = null;
            this.printRectArray = null;
            this.printDoc = new PrintDocument();
            this.printPageSettings = new PageSettings();
            this.printSizeMode = 2;
            this.printRect = new Rectangle2D(0.3, 0.3, 0.4, 0.4);
            this.printDialogFlag = false;
            this.doubleBuffer = true;
            this.printBackgroundEnable = true;
            this.printComponentsEnable = true;
            this.printBorderEnable = true;
            this.chartcounter = 0;
            this.printChartView = component;
            this.printDoc.PrintPage += new PrintPageEventHandler(this.PrintView);
        }

        public ChartPrint(ChartView component, int nsizemode)
        {
            this.printChartView = null;
            this.chartViewArray = null;
            this.printRectArray = null;
            this.printDoc = new PrintDocument();
            this.printPageSettings = new PageSettings();
            this.printSizeMode = 2;
            this.printRect = new Rectangle2D(0.3, 0.3, 0.4, 0.4);
            this.printDialogFlag = false;
            this.doubleBuffer = true;
            this.printBackgroundEnable = true;
            this.printComponentsEnable = true;
            this.printBorderEnable = true;
            this.chartcounter = 0;
            this.printChartView = component;
            this.printSizeMode = nsizemode;
            this.printDoc.PrintPage += new PrintPageEventHandler(this.PrintView);
        }

        public ChartPrint(ChartView[] components, Rectangle2D[] posrects)
        {
            this.printChartView = null;
            this.chartViewArray = null;
            this.printRectArray = null;
            this.printDoc = new PrintDocument();
            this.printPageSettings = new PageSettings();
            this.printSizeMode = 2;
            this.printRect = new Rectangle2D(0.3, 0.3, 0.4, 0.4);
            this.printDialogFlag = false;
            this.doubleBuffer = true;
            this.printBackgroundEnable = true;
            this.printComponentsEnable = true;
            this.printBorderEnable = true;
            this.chartcounter = 0;
            this.chartViewArray = (ChartView[]) components.Clone();
            this.printRectArray = (Rectangle2D[]) posrects.Clone();
            this.printSizeMode = 3;
            this.printDoc.PrintPage += new PrintPageEventHandler(this.PrintViewArray);
        }

        public override object Clone()
        {
            ChartPrint source = new ChartPrint();
            base.Copy(source);
            return source;
        }

        public void Copy(ChartPrint source)
        {
            if (source != null)
            {
                this.printChartView = source.printChartView;
                this.printSizeMode = source.printSizeMode;
                this.printRect = source.printRect;
                this.printDialogFlag = source.printDialogFlag;
                this.doubleBuffer = source.doubleBuffer;
                this.printBackgroundEnable = source.printBackgroundEnable;
                this.printComponentsEnable = source.printComponentsEnable;
                this.printBorderEnable = source.printBorderEnable;
            }
        }

        public virtual void DocPrintPage(object sender, EventArgs e)
        {
            this.chartcounter = 0;
            this.printDoc.DefaultPageSettings = this.printPageSettings;
            if (this.printDoc != null)
            {
                try
                {
                    this.printDoc.Print();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            this.printChartView.UpdateDraw();
        }

        public bool DoPrintDialog()
        {
            PrintDialog dialog = new PrintDialog();
            dialog.Document = this.printDoc;
            this.printDialogFlag = dialog.ShowDialog() == DialogResult.OK;
            if (this.printDialogFlag)
            {
                this.printPageSettings.PrinterSettings = dialog.PrinterSettings;
            }
            return this.printDialogFlag;
        }

        public override int ErrorCheck(int nerror)
        {
            if (this.printChartView == null)
            {
                nerror = 50;
            }
            return base.ErrorCheck(nerror);
        }

        public PageSettings GetPageSettings()
        {
            return this.printPageSettings;
        }

        public bool GetPrintBackgroundEnable()
        {
            return this.printBackgroundEnable;
        }

        public bool GetPrintBorderEnable()
        {
            return this.printBorderEnable;
        }

        public ChartView GetPrintChartView()
        {
            return this.printChartView;
        }

        public bool GetPrintComponentsEnable()
        {
            return this.printComponentsEnable;
        }

        public bool GetPrintDialogFlag()
        {
            return this.printDialogFlag;
        }

        public PrintDocument GetPrintDoc()
        {
            return this.printDoc;
        }

        public Rectangle2D GetPrintRect()
        {
            Rectangle2D rectangled = null;
            if (this.printRect != null)
            {
                rectangled = (Rectangle2D) this.printRect.Clone();
            }
            return rectangled;
        }

        public int GetPrintSizeMode()
        {
            return this.printSizeMode;
        }

        public virtual void PageSetupItem(object sender, EventArgs e)
        {
            PageSetupDialog dialog = new PageSetupDialog();
            dialog.Document = this.printDoc;
            dialog.MinMargins = new Margins(0x19, 0x19, 0x19, 0x19);
            dialog.ShowDialog();
            this.printDoc.DefaultPageSettings = dialog.PageSettings;
        }

        public virtual void PrintPreviewItem(object sender, EventArgs e)
        {
            this.chartcounter = 0;
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            dialog.Document = this.printDoc;
            dialog.ShowDialog();
        }

        private void PrintView(object sender, PrintPageEventArgs e)
        {
            double num9 = 1.0;
            double num10 = 1.0;
            double num11 = 1.0;
            Graphics graphics = e.Graphics;
            Rectangle marginBounds = e.MarginBounds;
            double x = marginBounds.X;
            double y = marginBounds.Y;
            double height = marginBounds.Height;
            double width = marginBounds.Width;
            Rectangle2D viewport = this.printChartView.GetViewport();
            if (viewport == null)
            {
                viewport = new Rectangle2D(0.0, 0.0, (double) this.printChartView.ClientSize.Width, (double) this.printChartView.ClientSize.Height);
            }
            viewport.GetX();
            viewport.GetY();
            double w = viewport.GetWidth();
            double h = viewport.GetHeight();
            double num7 = x;
            double num8 = y;
            switch (this.printSizeMode)
            {
                case 0:
                    num9 = 1.0;
                    break;

                case 1:
                    num10 = height / h;
                    num11 = width / w;
                    if (num10 <= num11)
                    {
                        num9 = num10;
                        break;
                    }
                    num9 = num11;
                    break;

                case 2:
                    num10 = height / h;
                    num11 = width / w;
                    if (num10 <= num11)
                    {
                        num9 = num10;
                        break;
                    }
                    num9 = num11;
                    break;

                case 3:
                    num7 += this.printRect.GetX() * width;
                    num8 += this.printRect.GetY() * height;
                    num10 = (this.printRect.GetHeight() * height) / h;
                    num11 = (this.printRect.GetWidth() * width) / w;
                    if (num10 <= num11)
                    {
                        num9 = num10;
                        break;
                    }
                    num9 = num11;
                    break;
            }
            Matrix transform = graphics.Transform;
            Matrix matrix2 = graphics.Transform;
            matrix2.Translate((float) num7, (float) num8);
            matrix2.Scale((float) num9, (float) num9);
            graphics.Transform = matrix2;
            bool backgroundDrawEnable = this.printChartView.GetBackgroundDrawEnable();
            this.printChartView.SetBackgroundDrawEnable(this.printBackgroundEnable);
            this.printChartView.SetViewport(0.0, 0.0, w, h);
            this.printChartView.ResetPreviousChartObjectList();
            this.printChartView.RenderingMode = 2;
            this.printChartView.Draw(graphics);
            this.printChartView.RenderingMode = 0;
            this.printChartView.SetBackgroundDrawEnable(backgroundDrawEnable);
            graphics.Transform = transform;
        }

        private void PrintViewArray(object sender, PrintPageEventArgs e)
        {
            double num9 = 1.0;
            double num10 = 1.0;
            double num11 = 1.0;
            Graphics graphics = e.Graphics;
            Rectangle marginBounds = e.MarginBounds;
            double x = marginBounds.X;
            double y = marginBounds.Y;
            double height = marginBounds.Height;
            double width = marginBounds.Width;
            for (int i = this.chartcounter; i < this.chartViewArray.Length; i++)
            {
                Matrix matrix;
                this.printChartView = this.chartViewArray[i];
                if (this.printChartView == null)
                {
                    e.HasMorePages = true;
                    this.chartcounter++;
                    return;
                }
                e.HasMorePages = false;
                this.chartcounter++;
                this.printRect = this.printRectArray[i];
                Rectangle2D viewport = this.printChartView.GetViewport();
                if (viewport == null)
                {
                    viewport = new Rectangle2D(0.0, 0.0, (double) this.printChartView.ClientSize.Width, (double) this.printChartView.ClientSize.Height);
                }
                viewport.GetX();
                viewport.GetY();
                double w = viewport.GetWidth();
                double h = viewport.GetHeight();
                double num7 = x;
                double num8 = y;
                switch (this.printSizeMode)
                {
                    case 0:
                        num9 = 1.0;
                        goto Label_01FA;

                    case 1:
                        num10 = height / h;
                        num11 = width / w;
                        if (num10 <= num11)
                        {
                            break;
                        }
                        num9 = num11;
                        goto Label_01FA;

                    case 2:
                        num10 = height / h;
                        num11 = width / w;
                        if (num10 <= num11)
                        {
                            goto Label_018F;
                        }
                        num9 = num11;
                        goto Label_01FA;

                    case 3:
                        num7 += this.printRect.GetX() * width;
                        num8 += this.printRect.GetY() * height;
                        num10 = (this.printRect.GetHeight() * height) / h;
                        num11 = (this.printRect.GetWidth() * width) / w;
                        if (num10 <= num11)
                        {
                            goto Label_01E9;
                        }
                        num9 = num11;
                        goto Label_01FA;

                    default:
                        goto Label_01FA;
                }
                num9 = num10;
                goto Label_01FA;
            Label_018F:
                num9 = num10;
                goto Label_01FA;
            Label_01E9:
                num9 = num10;
            Label_01FA:
                matrix = graphics.Transform;
                Matrix transform = graphics.Transform;
                transform.Translate((float) num7, (float) num8);
                transform.Scale((float) num9, (float) num9);
                graphics.Transform = transform;
                bool backgroundDrawEnable = this.printChartView.GetBackgroundDrawEnable();
                this.printChartView.SetBackgroundDrawEnable(this.printBackgroundEnable);
                this.printChartView.SetViewport(0.0, 0.0, w, h);
                this.printChartView.ResetPreviousChartObjectList();
                this.printChartView.RenderingMode = 2;
                this.printChartView.Draw(graphics);
                this.printChartView.RenderingMode = 0;
                this.printChartView.SetBackgroundDrawEnable(backgroundDrawEnable);
                graphics.Transform = matrix;
            }
        }

        public void SetPageSettings(PageSettings pg)
        {
            this.printPageSettings = pg;
        }

        public void SetPrintBackgroundEnable(bool printbackground)
        {
            this.printBackgroundEnable = printbackground;
        }

        public void SetPrintBorderEnable(bool printborder)
        {
            this.printBorderEnable = printborder;
        }

        public void SetPrintChartView(ChartView component)
        {
            this.printChartView = component;
        }

        public void SetPrintComponentsEnable(bool printcomponents)
        {
            this.printComponentsEnable = printcomponents;
        }

        public void SetPrintDialogFlag(bool printdialogflag)
        {
            this.printDialogFlag = printdialogflag;
        }

        public void SetPrintDoc(PrintDocument doc)
        {
            this.printDoc = doc;
        }

        public void SetPrintRect(Rectangle2D rect)
        {
            if (rect != null)
            {
                this.printRect = (Rectangle2D) rect.Clone();
            }
        }

        public void SetPrintSizeMode(int nsizemode)
        {
            this.printSizeMode = nsizemode;
        }

        public bool PrintBackgroundEnable
        {
            get
            {
                return this.printBackgroundEnable;
            }
            set
            {
                this.printBackgroundEnable = value;
            }
        }

        public bool PrintBorderEnable
        {
            get
            {
                return this.printBorderEnable;
            }
            set
            {
                this.printBorderEnable = value;
            }
        }

        public ChartView PrintChartView
        {
            get
            {
                return this.printChartView;
            }
            set
            {
                this.printChartView = value;
            }
        }

        public bool PrintComponentsEnable
        {
            get
            {
                return this.printComponentsEnable;
            }
            set
            {
                this.printComponentsEnable = value;
            }
        }

        public PrintDocument PrintDoc
        {
            get
            {
                return this.printDoc;
            }
            set
            {
                this.printDoc = value;
            }
        }

        public PageSettings PrintPageSettings
        {
            get
            {
                return this.printPageSettings;
            }
            set
            {
                this.printPageSettings = value;
            }
        }

        public int PrintSizeMode
        {
            get
            {
                return this.printSizeMode;
            }
            set
            {
                this.printSizeMode = value;
            }
        }
    }
}

