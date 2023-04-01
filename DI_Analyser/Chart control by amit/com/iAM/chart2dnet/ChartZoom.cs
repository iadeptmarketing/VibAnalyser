namespace com.iAM.chart2dnet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ChartZoom : MouseListener
    //{
    //    private PhysicalCoordinates initialZoomCoordinates;
    //    internal int numberSuperZoomScales;
    //    internal Point2D PrevDevLocation;
    //    internal bool rescaleFlag;
    //    internal bool superZoomFlag;
    //    private ArrayList superZoomScales;
    //    internal Point2D zoomCurrentLocation;
    //    internal Point2D zoomDevEndLoc;
    //    internal Point2D zoomDevLocation;
    //    internal Point2D zoomDevStartLoc;
    //    internal Point2D zoomMaxLocation;
    //    internal Point2D zoomMinLocation;
    //    private bool zoomObjActive;
    //    internal Dimension zoomRangeLimits;
    //    internal Dimension zoomRangeLimitsRatio;
    //    private Stack zoomStack;
    //    internal bool zoomStackEnable;
    //    internal bool zoomXEnable;
    //    internal int zoomXRoundMode;
    //    internal bool zoomYEnable;
    //    internal int zoomYRoundMode;

    //    public ChartZoom()
    //    {
    //        this.zoomMinLocation = new Point2D();
    //        this.zoomMaxLocation = new Point2D();
    //        this.zoomCurrentLocation = new Point2D();
    //        this.zoomDevLocation = new Point2D();
    //        this.PrevDevLocation = new Point2D();
    //        this.zoomDevStartLoc = new Point2D();
    //        this.zoomDevEndLoc = new Point2D();
    //        this.superZoomScales = null;
    //        this.initialZoomCoordinates = new CartesianCoordinates();
    //        this.numberSuperZoomScales = 0;
    //        this.superZoomFlag = false;
    //        this.rescaleFlag = false;
    //        this.zoomXEnable = true;
    //        this.zoomYEnable = true;
    //        this.zoomXRoundMode = 2;
    //        this.zoomYRoundMode = 2;
    //        this.zoomStack = new Stack();
    //        this.zoomStackEnable = false;
    //        this.zoomRangeLimits = new Dimension(0.01, 0.01);
    //        this.zoomRangeLimitsRatio = new Dimension(0.001, 0.001);
    //        this.zoomObjActive = false;
    //        this.InitZoomDefaults();
    //        base.tempGraphics = null;
    //    }

    //    public ChartZoom(ChartView component, PhysicalCoordinates transform, bool brescale)
    //    {
    //        this.zoomMinLocation = new Point2D();
    //        this.zoomMaxLocation = new Point2D();
    //        this.zoomCurrentLocation = new Point2D();
    //        this.zoomDevLocation = new Point2D();
    //        this.PrevDevLocation = new Point2D();
    //        this.zoomDevStartLoc = new Point2D();
    //        this.zoomDevEndLoc = new Point2D();
    //        this.superZoomScales = null;
    //        this.initialZoomCoordinates = new CartesianCoordinates();
    //        this.numberSuperZoomScales = 0;
    //        this.superZoomFlag = false;
    //        this.rescaleFlag = false;
    //        this.zoomXEnable = true;
    //        this.zoomYEnable = true;
    //        this.zoomXRoundMode = 2;
    //        this.zoomYRoundMode = 2;
    //        this.zoomStack = new Stack();
    //        this.zoomStackEnable = false;
    //        this.zoomRangeLimits = new Dimension(0.01, 0.01);
    //        this.zoomRangeLimitsRatio = new Dimension(0.001, 0.001);
    //        this.zoomObjActive = false;
    //        this.InitSimpleZoom(component, transform, brescale);
    //    }

    //    public ChartZoom(ChartView component, PhysicalCoordinates[] transforms, bool brescale)
    //    {
    //        this.zoomMinLocation = new Point2D();
    //        this.zoomMaxLocation = new Point2D();
    //        this.zoomCurrentLocation = new Point2D();
    //        this.zoomDevLocation = new Point2D();
    //        this.PrevDevLocation = new Point2D();
    //        this.zoomDevStartLoc = new Point2D();
    //        this.zoomDevEndLoc = new Point2D();
    //        this.superZoomScales = null;
    //        this.initialZoomCoordinates = new CartesianCoordinates();
    //        this.numberSuperZoomScales = 0;
    //        this.superZoomFlag = false;
    //        this.rescaleFlag = false;
    //        this.zoomXEnable = true;
    //        this.zoomYEnable = true;
    //        this.zoomXRoundMode = 2;
    //        this.zoomYRoundMode = 2;
    //        this.zoomStack = new Stack();
    //        this.zoomStackEnable = false;
    //        this.zoomRangeLimits = new Dimension(0.01, 0.01);
    //        this.zoomRangeLimitsRatio = new Dimension(0.001, 0.001);
    //        this.zoomObjActive = false;
    //        this.InitSuperZoom(component, transforms, brescale);
    //    }

    //    private void CalcZoomRangeLimits(PhysicalCoordinates chartscale)
    //    {
    //        double w = Math.Abs((double) (chartscale.GetScaleStopX() - chartscale.GetScaleStartX()));
    //        double h = Math.Abs((double) (chartscale.GetScaleStopY() - chartscale.GetScaleStartY()));
    //        w *= this.zoomRangeLimitsRatio.GetWidth();
    //        h *= this.zoomRangeLimitsRatio.GetHeight();
    //        this.zoomRangeLimits.SetSize(w, h);
    //    }

    //    public override object Clone()
    //    {
    //        ChartZoom zoom = new ChartZoom();
    //        zoom.Copy(this);
    //        return zoom;
    //    }

    //    public void Copy(ChartZoom source)
    //    {
    //        if (source != null)
    //        {
    //            base.Copy(source);
    //            this.numberSuperZoomScales = source.numberSuperZoomScales;
    //            this.superZoomScales.Clear();
    //            for (int i = 0; i < this.numberSuperZoomScales; i++)
    //            {
    //                this.superZoomScales.Add(source.superZoomScales[i]);
    //            }
    //            this.superZoomFlag = source.superZoomFlag;
    //            base.chartObjComponent = source.chartObjComponent;
    //            this.zoomObjActive = source.zoomObjActive;
    //            this.rescaleFlag = source.rescaleFlag;
    //            this.zoomXEnable = source.zoomXEnable;
    //            this.zoomYEnable = source.zoomYEnable;
    //            this.zoomXRoundMode = source.zoomXRoundMode;
    //            this.zoomYRoundMode = source.zoomYRoundMode;
    //            this.zoomStackEnable = false;
    //            if (source.zoomRangeLimits != null)
    //            {
    //                this.zoomRangeLimits = (Dimension) source.zoomRangeLimits.Clone();
    //            }
    //            if (source.zoomRangeLimitsRatio != null)
    //            {
    //                this.zoomRangeLimitsRatio = (Dimension) source.zoomRangeLimitsRatio.Clone();
    //            }
    //        }
    //    }

    //    public override void Draw(Graphics g2)
    //    {
    //        double num7;
    //        if (this.superZoomFlag)
    //        {
    //            ((PhysicalCoordinates) this.superZoomScales[0]).SetCurrentAttributes(base.chartObjAttributes);
    //        }
    //        else
    //        {
    //            base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
    //        }
    //        double x = this.zoomDevStartLoc.GetX();
    //        double num3 = this.zoomCurrentLocation.GetX();
    //        double y = this.zoomDevStartLoc.GetY();
    //        double num4 = this.zoomCurrentLocation.GetY();
    //        if (x > num3)
    //        {
    //            num7 = x;
    //            x = num3;
    //            num3 = num7;
    //        }
    //        if (y > num4)
    //        {
    //            num7 = y;
    //            y = num4;
    //            num4 = num7;
    //        }
    //        double num6 = num3 - x;
    //        double num5 = num4 - y;
    //        Rectangle r = new Rectangle((int) x, (int) y, (int) num6, (int) num5);
    //        r = base.chartObjComponent.RectangleToScreen(r);
    //        base.chartObjScale.DrawReversibleFrame(r, Color.White, FrameStyle.Dashed);
    //    }

    //    public override int ErrorCheck(int nerror)
    //    {
    //        if (base.chartObjComponent == null)
    //        {
    //            nerror = 50;
    //        }
    //        if (this.superZoomFlag)
    //        {
    //            for (int i = 0; i < this.numberSuperZoomScales; i++)
    //            {
    //                if (this.superZoomScales[i] == null)
    //                {
    //                    nerror = 20;
    //                }
    //            }
    //        }
    //        else if (this.GetChartObjScale() == null)
    //        {
    //            nerror = 20;
    //        }
    //        return base.ErrorCheck(nerror);
    //    }

    //    public Point2D GetZoomMax(int nmode)
    //    {
    //        Point2D dest = new Point2D();
    //        base.chartObjScale.ConvertCoord(dest, nmode, this.zoomDevEndLoc, 0);
    //        return dest;
    //    }

    //    public Point2D GetZoomMin(int nmode)
    //    {
    //        Point2D dest = new Point2D();
    //        base.chartObjScale.ConvertCoord(dest, nmode, this.zoomDevStartLoc, 0);
    //        return dest;
    //    }

    //    public Dimension GetZoomRangeLimits()
    //    {
    //        return (Dimension) this.zoomRangeLimits.Clone();
    //    }

    //    public Dimension GetZoomRangeLimitsRatio()
    //    {
    //        return (Dimension) this.zoomRangeLimitsRatio.Clone();
    //    }

    //    public bool GetZoomStackEnable()
    //    {
    //        return this.zoomStackEnable;
    //    }

    //    public bool GetZoomXEnable()
    //    {
    //        return this.zoomXEnable;
    //    }

    //    public int GetZoomXRoundMode()
    //    {
    //        return this.zoomXRoundMode;
    //    }

    //    public bool GetZoomYEnable()
    //    {
    //        return this.zoomYEnable;
    //    }

    //    public int GetZoomYRoundMode()
    //    {
    //        return this.zoomYRoundMode;
    //    }

    //    private void InitSimpleZoom(ChartView component, PhysicalCoordinates transform, bool brescale)
    //    {
    //        this.InitZoomDefaults();
    //        base.chartObjComponent = component;
    //        this.initialZoomCoordinates = transform;
    //        this.SetChartObjScale(transform);
    //        this.CalcZoomRangeLimits(this.initialZoomCoordinates);
    //        this.rescaleFlag = brescale;
    //        this.superZoomFlag = false;
    //        base.enabled = true;
    //    }

    //    private void InitSuperZoom(ChartView component, PhysicalCoordinates[] transforms, bool brescale)
    //    {
    //        int length = transforms.Length;
    //        this.InitZoomDefaults();
    //        if (length >= 1)
    //        {
    //            base.chartObjComponent = component;
    //            this.SetChartObjScale(transforms[0]);
    //            this.initialZoomCoordinates = transforms[0];
    //            this.CalcZoomRangeLimits(this.initialZoomCoordinates);
    //            this.numberSuperZoomScales = length;
    //            for (int i = 0; i < this.numberSuperZoomScales; i++)
    //            {
    //                this.superZoomScales.Add(transforms[i]);
    //            }
    //            this.superZoomFlag = true;
    //            this.rescaleFlag = brescale;
    //            base.enabled = true;
    //        }
    //    }

    //    private void InitZoomDefaults()
    //    {
    //        base.chartObjType = 900;
    //        this.zoomObjActive = false;
    //        this.rescaleFlag = false;
    //        this.SetLineStyle(DashStyle.Dash);
    //        base.LineColor = Color.White;
    //        this.superZoomScales = new ArrayList(20);
    //    }

    //    public override void OnClick(EventArgs mouseevent)
    //    {
    //    }

    //    public override void OnDoubleClick(EventArgs mouseevent)
    //    {
    //    }

    //    public override void OnMouseDown(MouseEventArgs mouseevent)
    //    {
    //        if (((this.ErrorCheck(0) == 0) && base.enabled) && ((mouseevent.Button & base.buttonMask) != MouseButtons.None))
    //        {
    //            if (this.superZoomFlag)
    //            {
    //                ((PhysicalCoordinates) this.superZoomScales[0]).ChartTransform(base.tempGraphics);
    //            }
    //            else
    //            {
    //                base.chartObjScale.ChartTransform(base.tempGraphics);
    //            }
    //            this.zoomObjActive = true;
    //            this.zoomDevLocation.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
    //            this.zoomDevStartLoc.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //            this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //            base.chartObjScale.StartXORMode(base.chartObjComponent, base.chartObjAttributes.PrimaryColor, 0);
    //            this.Draw(base.tempGraphics);
    //            this.PrevDevLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //        }
    //    }

    //    public override void OnMouseMove(MouseEventArgs mouseevent)
    //    {
    //        MouseButtons button = mouseevent.Button;
    //        if (this.zoomObjActive && (button == base.buttonMask))
    //        {
    //            this.zoomDevLocation.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
    //            this.zoomCurrentLocation.SetLocation(this.PrevDevLocation.GetX(), this.PrevDevLocation.GetY());
    //            this.Draw(base.tempGraphics);
    //            this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //            this.Draw(base.tempGraphics);
    //            this.PrevDevLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //        }
    //    }

    //    public override void OnMouseUp(MouseEventArgs mouseevent)
    //    {
    //        if ((mouseevent.Button == base.buttonMask) & this.zoomObjActive)
    //        {
    //            this.zoomDevLocation.SetLocation((double) mouseevent.X, (double) mouseevent.Y);
    //            this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //            this.Draw(base.tempGraphics);
    //            base.chartObjScale.EndXORMode(base.chartObjComponent);
    //            this.zoomDevEndLoc.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
    //            this.PushZoomStack();
    //            base.chartObjComponent.ResetPreviousChartObjectList();
    //            if (!this.superZoomFlag)
    //            {
    //                this.ProcessSimpleZoom();
    //            }
    //            else
    //            {
    //                this.ProcessSuperZoom();
    //            }
    //            this.zoomObjActive = false;
    //            base.tempGraphics = null;
    //        }
    //    }

    //    private int PopSimpleZoomStack()
    //    {
    //        int num = 0;
    //        if (!this.zoomStackEnable)
    //        {
    //            return num;
    //        }
    //        if (this.zoomStack.Count == 0)
    //        {
    //            return 0;
    //        }
    //        PhysicalCoordinates source = (PhysicalCoordinates) this.zoomStack.Pop();
    //        base.chartObjScale.Copy(source);
    //        this.RescaleAxesToTransform(base.chartObjScale);
    //        base.chartObjComponent.UpdateDraw();
    //        return this.zoomStack.Count;
    //    }

    //    private int PopSuperZoomStack()
    //    {
    //        int num = 0;
    //        if (!this.zoomStackEnable)
    //        {
    //            return num;
    //        }
    //        for (int i = 0; i < this.numberSuperZoomScales; i++)
    //        {
    //            if (this.zoomStack.Count == 0)
    //            {
    //                return 0;
    //            }
    //            PhysicalCoordinates transform = (PhysicalCoordinates) this.superZoomScales[i];
    //            PhysicalCoordinates source = (PhysicalCoordinates) this.zoomStack.Pop();
    //            transform.Copy(source);
    //            this.RescaleAxesToTransform(transform);
    //        }
    //        base.chartObjComponent.UpdateDraw();
    //        return this.zoomStack.Count;
    //    }

    //    public int PopZoomStack()
    //    {
    //        base.chartObjComponent.ResetPreviousChartObjectList();
    //        if (!this.superZoomFlag)
    //        {
    //            return this.PopSimpleZoomStack();
    //        }
    //        return this.PopSuperZoomStack();
    //    }

    //    private void ProcessSimpleZoom()
    //    {
    //        int nroundmodeX = 1;
    //        int nroundmodeY = 1;
    //        this.SaveAxesIntercepts(base.chartObjScale);
    //        if (this.SetPhysZoomLocations() && this.rescaleFlag)
    //        {
    //            double num3 = Math.Abs((double) (this.zoomMaxLocation.GetX() - this.zoomMinLocation.GetX()));
    //            double num4 = Math.Abs((double) (this.zoomMaxLocation.GetY() - this.zoomMinLocation.GetY()));
    //            if ((num3 >= this.zoomRangeLimits.GetWidth()) && (num4 >= this.zoomRangeLimits.GetHeight()))
    //            {
    //                if (this.zoomXEnable)
    //                {
    //                    base.chartObjScale.SetScaleX(this.zoomMinLocation.GetX(), this.zoomMaxLocation.GetX());
    //                    nroundmodeX = this.zoomXRoundMode;
    //                }
    //                else
    //                {
    //                    nroundmodeX = 0;
    //                }
    //                if (this.zoomYEnable)
    //                {
    //                    base.chartObjScale.SetScaleY(this.zoomMinLocation.GetY(), this.zoomMaxLocation.GetY());
    //                    nroundmodeY = this.zoomYRoundMode;
    //                }
    //                else
    //                {
    //                    nroundmodeY = 0;
    //                }
    //                base.chartObjScale.AutoScale(nroundmodeX, nroundmodeY);
    //                this.RescaleAxesToTransform(base.chartObjScale);
    //                base.chartObjComponent.UpdateDraw();
    //            }
    //        }
    //    }

    //    private void ProcessSuperZoom()
    //    {
    //        for (int i = 0; i < this.numberSuperZoomScales; i++)
    //        {
    //            PhysicalCoordinates transform = (PhysicalCoordinates) this.superZoomScales[i];
    //            this.SaveAxesIntercepts(transform);
    //            int nroundmodeX = 1;
    //            int nroundmodeY = 1;
    //            if (this.SetPhysSuperZoomLocations(transform) && this.rescaleFlag)
    //            {
    //                if (i == 0)
    //                {
    //                    double num4 = Math.Abs((double) (this.zoomMaxLocation.GetX() - this.zoomMinLocation.GetX()));
    //                    double num5 = Math.Abs((double) (this.zoomMaxLocation.GetY() - this.zoomMinLocation.GetY()));
    //                    if ((num4 < this.zoomRangeLimits.GetWidth()) || (num5 < this.zoomRangeLimits.GetHeight()))
    //                    {
    //                        return;
    //                    }
    //                }
    //                if (this.zoomXEnable)
    //                {
    //                    transform.SetScaleX(this.zoomMinLocation.GetX(), this.zoomMaxLocation.GetX());
    //                    nroundmodeX = this.zoomXRoundMode;
    //                }
    //                else
    //                {
    //                    nroundmodeX = 0;
    //                }
    //                if (this.zoomYEnable)
    //                {
    //                    transform.SetScaleY(this.zoomMinLocation.GetY(), this.zoomMaxLocation.GetY());
    //                    nroundmodeY = this.zoomYRoundMode;
    //                }
    //                else
    //                {
    //                    nroundmodeY = 0;
    //                }
    //                transform.AutoScale(nroundmodeX, nroundmodeY);
    //                this.RescaleAxesToTransform(transform);
    //            }
    //        }
    //        base.chartObjComponent.UpdateDraw();
    //    }

    //    private int PushChartScale(PhysicalCoordinates chartscale)
    //    {
    //        int count = 0;
    //        if (this.zoomStackEnable)
    //        {
    //            this.zoomStack.Push(chartscale.Clone());
    //            count = this.zoomStack.Count;
    //        }
    //        return count;
    //    }

    //    private int PushSimpleZoomStack()
    //    {
    //        int num = 0;
    //        if (this.zoomStackEnable)
    //        {
    //            num = this.PushChartScale(base.chartObjScale);
    //        }
    //        return num;
    //    }

    //    private int PushSuperZoomStack()
    //    {
    //        int num = 0;
    //        if (this.zoomStackEnable)
    //        {
    //            for (int i = 0; i < this.numberSuperZoomScales; i++)
    //            {
    //                PhysicalCoordinates chartscale = (PhysicalCoordinates) this.superZoomScales[(this.numberSuperZoomScales - i) - 1];
    //                num = this.PushChartScale(chartscale);
    //            }
    //        }
    //        return num;
    //    }

    //    public int PushZoomStack()
    //    {
    //        if (!this.superZoomFlag)
    //        {
    //            return this.PushSimpleZoomStack();
    //        }
    //        return this.PushSuperZoomStack();
    //    }

    //    private void RescaleAxesToTransform(PhysicalCoordinates transform)
    //    {
    //        int count = base.chartObjComponent.GetChartObjectsArrayList().Count;
    //        GraphObj obj2 = null;
    //        new Point2D();
    //        new Point2D();
    //        for (int i = 0; i < count; i++)
    //        {
    //            obj2 = (GraphObj) base.chartObjComponent.GetChartObjectsArrayList()[i];
    //            if (((obj2 != null) && (((obj2.GetChartObjType() == 100) || (obj2.GetChartObjType() == 0x65)) || ((obj2.GetChartObjType() == 0x66) || (obj2.GetChartObjType() == 0x67)))) && (obj2.GetChartObjScale() == transform))
    //            {
    //                Axis axis = (Axis) obj2;
    //                if ((axis.AxisType == 0) && this.zoomXEnable)
    //                {
    //                    axis.CalcAutoAxis();
    //                }
    //                else if ((axis.AxisType == 1) && this.zoomYEnable)
    //                {
    //                    axis.CalcAutoAxis();
    //                }
    //                axis.RestoreAxisNormIntercept();
    //            }
    //        }
    //    }

    //    private void SaveAxesIntercepts(PhysicalCoordinates transform)
    //    {
    //        int count = base.chartObjComponent.GetChartObjectsArrayList().Count;
    //        GraphObj obj2 = null;
    //        for (int i = 0; i < count; i++)
    //        {
    //            obj2 = (GraphObj) base.chartObjComponent.GetChartObjectsArrayList()[i];
    //            if (((obj2 != null) && (((obj2.GetChartObjType() == 100) || (obj2.GetChartObjType() == 0x65)) || ((obj2.GetChartObjType() == 0x66) || (obj2.GetChartObjType() == 0x67)))) && (obj2.GetChartObjScale() == transform))
    //            {
    //                ((Axis) obj2).CalcAxisNormIntercept();
    //            }
    //        }
    //    }

    //    private bool SetPhysSuperZoomLocations(PhysicalCoordinates atransform)
    //    {
    //        double num5;
    //        double x = this.zoomDevStartLoc.GetX();
    //        double px = this.zoomDevEndLoc.GetX();
    //        double y = this.zoomDevStartLoc.GetY();
    //        double py = this.zoomDevEndLoc.GetY();
    //        if ((Math.Abs((double) (px - x)) < 4.0) || (Math.Abs((double) (y - py)) < 4.0))
    //        {
    //            return false;
    //        }
    //        if (x > px)
    //        {
    //            num5 = x;
    //            x = px;
    //            px = num5;
    //        }
    //        if (y < py)
    //        {
    //            num5 = y;
    //            y = py;
    //            py = num5;
    //        }
    //        Point2D source = new Point2D(x, y);
    //        Point2D pointd2 = new Point2D(px, py);
    //        atransform.ConvertCoord(this.zoomMinLocation, 1, source, 0);
    //        atransform.ConvertCoord(this.zoomMaxLocation, 1, pointd2, 0);
    //        return true;
    //    }

    //    private bool SetPhysZoomLocations()
    //    {
    //        double num5;
    //        double x = this.zoomDevStartLoc.GetX();
    //        double px = this.zoomDevEndLoc.GetX();
    //        double y = this.zoomDevStartLoc.GetY();
    //        double py = this.zoomDevEndLoc.GetY();
    //        if ((Math.Abs((double) (px - x)) < 4.0) || (Math.Abs((double) (y - py)) < 4.0))
    //        {
    //            return false;
    //        }
    //        if (x > px)
    //        {
    //            num5 = x;
    //            x = px;
    //            px = num5;
    //        }
    //        if (y < py)
    //        {
    //            num5 = y;
    //            y = py;
    //            py = num5;
    //        }
    //        Point2D source = new Point2D(x, y);
    //        Point2D pointd2 = new Point2D(px, py);
    //        base.chartObjScale.ConvertCoord(this.zoomMinLocation, 1, source, 0);
    //        base.chartObjScale.ConvertCoord(this.zoomMaxLocation, 1, pointd2, 0);
    //        return true;
    //    }

    //    public void SetZoomRangeLimits(Dimension limits)
    //    {
    //        this.zoomRangeLimits = (Dimension) limits.Clone();
    //    }

    //    public void SetZoomRangeLimitsRatio(Dimension ratio)
    //    {
    //        this.zoomRangeLimitsRatio = (Dimension) ratio.Clone();
    //        this.CalcZoomRangeLimits(this.initialZoomCoordinates);
    //    }

    //    public void SetZoomStackEnable(bool on)
    //    {
    //        this.zoomStackEnable = on;
    //    }

    //    public void SetZoomXEnable(bool bzoomx)
    //    {
    //        this.zoomXEnable = bzoomx;
    //    }

    //    public void SetZoomXRoundMode(int nzoomx)
    //    {
    //        this.zoomXRoundMode = nzoomx;
    //    }

    //    public void SetZoomYEnable(bool bzoomy)
    //    {
    //        this.zoomYEnable = bzoomy;
    //    }

    //    public void SetZoomYRoundMode(int nzoomy)
    //    {
    //        this.zoomYRoundMode = nzoomy;
    //    }

    //    public bool ZoomObjActive
    //    {
    //        get
    //        {
    //            return this.zoomObjActive;
    //        }
    //    }

    //    public bool ZoomStackEnable
    //    {
    //        get
    //        {
    //            return this.zoomStackEnable;
    //        }
    //        set
    //        {
    //            this.zoomStackEnable = value;
    //        }
    //    }

    //    public bool ZoomXEnable
    //    {
    //        get
    //        {
    //            return this.zoomXEnable;
    //        }
    //        set
    //        {
    //            this.zoomXEnable = value;
    //        }
    //    }

    //    public int ZoomXRoundMode
    //    {
    //        get
    //        {
    //            return this.zoomXRoundMode;
    //        }
    //        set
    //        {
    //            this.zoomXRoundMode = value;
    //        }
    //    }

    //    public bool ZoomYEnable
    //    {
    //        get
    //        {
    //            return this.zoomYEnable;
    //        }
    //        set
    //        {
    //            this.zoomYEnable = value;
    //        }
    //    }

    //    public int ZoomYRoundMode
    //    {
    //        get
    //        {
    //            return this.zoomYRoundMode;
    //        }
    //        set
    //        {
    //            this.zoomYRoundMode = value;
    //        }
    //    }
    //}
    {
        internal int arCorrectionMode;
        private PhysicalCoordinates initialZoomCoordinates;
        internal bool internalZoomStackProcesssing;
        internal int numberSuperZoomScales;
        internal Point2D PrevDevLocation;
        internal bool rescaleFlag;
        internal bool superZoomFlag;
        private ArrayList superZoomScales;
        internal Point2D zoomCurrentLocation;
        internal Point2D zoomDevEndLoc;
        internal Point2D zoomDevLocation;
        internal Point2D zoomDevStartLoc;
        internal Point2D zoomMaxLocation;
        internal Point2D zoomMinLocation;
        private bool zoomObjActive;
        internal Dimension zoomRangeLimits;
        internal Dimension zoomRangeLimitsRatio;
        private Stack zoomStack;
        internal MouseButtons zoomStackButtonMask;
        internal bool zoomStackEnable;
        internal bool zoomXEnable;
        internal int zoomXRoundMode;
        internal bool zoomYEnable;
        internal int zoomYRoundMode;

        public ChartZoom()
        {
            this.zoomMinLocation = new Point2D();
            this.zoomMaxLocation = new Point2D();
            this.zoomCurrentLocation = new Point2D();
            this.zoomDevLocation = new Point2D();
            this.PrevDevLocation = new Point2D();
            this.zoomDevStartLoc = new Point2D();
            this.zoomDevEndLoc = new Point2D();
            this.superZoomScales = null;
            this.initialZoomCoordinates = new CartesianCoordinates();
            this.numberSuperZoomScales = 0;
            this.superZoomFlag = false;
            this.rescaleFlag = false;
            this.zoomXEnable = true;
            this.zoomYEnable = true;
            this.zoomXRoundMode = 2;
            this.zoomYRoundMode = 2;
            this.zoomStack = new Stack();
            this.zoomStackEnable = false;
            this.internalZoomStackProcesssing = false;
            this.zoomRangeLimits = new Dimension(0.01, 0.01);
            this.zoomRangeLimitsRatio = new Dimension(1E-07, 1E-07);
            this.zoomObjActive = false;
            this.zoomStackButtonMask = MouseButtons.Right;
            this.arCorrectionMode = 0;
            this.InitZoomDefaults();
            base.tempGraphics = null;
        }

        public ChartZoom(ChartView component, PhysicalCoordinates transform, bool brescale)
        {
            this.zoomMinLocation = new Point2D();
            this.zoomMaxLocation = new Point2D();
            this.zoomCurrentLocation = new Point2D();
            this.zoomDevLocation = new Point2D();
            this.PrevDevLocation = new Point2D();
            this.zoomDevStartLoc = new Point2D();
            this.zoomDevEndLoc = new Point2D();
            this.superZoomScales = null;
            this.initialZoomCoordinates = new CartesianCoordinates();
            this.numberSuperZoomScales = 0;
            this.superZoomFlag = false;
            this.rescaleFlag = false;
            this.zoomXEnable = true;
            this.zoomYEnable = true;
            this.zoomXRoundMode = 2;
            this.zoomYRoundMode = 2;
            this.zoomStack = new Stack();
            this.zoomStackEnable = false;
            this.internalZoomStackProcesssing = false;
            this.zoomRangeLimits = new Dimension(0.01, 0.01);
            this.zoomRangeLimitsRatio = new Dimension(1E-07, 1E-07);
            this.zoomObjActive = false;
            this.zoomStackButtonMask = MouseButtons.Right;
            this.arCorrectionMode = 0;
            this.InitSimpleZoom(component, transform, brescale);
        }

        public ChartZoom(ChartView component, PhysicalCoordinates[] transforms, bool brescale)
        {
            this.zoomMinLocation = new Point2D();
            this.zoomMaxLocation = new Point2D();
            this.zoomCurrentLocation = new Point2D();
            this.zoomDevLocation = new Point2D();
            this.PrevDevLocation = new Point2D();
            this.zoomDevStartLoc = new Point2D();
            this.zoomDevEndLoc = new Point2D();
            this.superZoomScales = null;
            this.initialZoomCoordinates = new CartesianCoordinates();
            this.numberSuperZoomScales = 0;
            this.superZoomFlag = false;
            this.rescaleFlag = false;
            this.zoomXEnable = true;
            this.zoomYEnable = true;
            this.zoomXRoundMode = 2;
            this.zoomYRoundMode = 2;
            this.zoomStack = new Stack();
            this.zoomStackEnable = false;
            this.internalZoomStackProcesssing = false;
            this.zoomRangeLimits = new Dimension(0.01, 0.01);
            this.zoomRangeLimitsRatio = new Dimension(1E-07, 1E-07);
            this.zoomObjActive = false;
            this.zoomStackButtonMask = MouseButtons.Right;
            this.arCorrectionMode = 0;
            this.InitSuperZoom(component, transforms, brescale);
        }

        private void CalcZoomRangeLimits(PhysicalCoordinates chartscale)
        {
            double w = Math.Abs((double)(chartscale.GetScaleStopX() - chartscale.GetScaleStartX()));
            double h = Math.Abs((double)(chartscale.GetScaleStopY() - chartscale.GetScaleStartY()));
            w *= this.zoomRangeLimitsRatio.GetWidth();
            h *= this.zoomRangeLimitsRatio.GetHeight();
            this.zoomRangeLimits.SetSize(w, h);
        }

        public override object Clone()
        {
            ChartZoom zoom = new ChartZoom();
            zoom.Copy(this);
            return zoom;
        }

        public void Copy(ChartZoom source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.numberSuperZoomScales = source.numberSuperZoomScales;
                this.superZoomScales.Clear();
                for (int i = 0; i < this.numberSuperZoomScales; i++)
                {
                    this.superZoomScales.Add(source.superZoomScales[i]);
                }
                this.superZoomFlag = source.superZoomFlag;
                base.chartObjComponent = source.chartObjComponent;
                this.zoomObjActive = source.zoomObjActive;
                this.rescaleFlag = source.rescaleFlag;
                this.zoomXEnable = source.zoomXEnable;
                this.zoomYEnable = source.zoomYEnable;
                this.zoomXRoundMode = source.zoomXRoundMode;
                this.zoomYRoundMode = source.zoomYRoundMode;
                this.zoomStackEnable = false;
                this.zoomStackButtonMask = source.zoomStackButtonMask;
                this.internalZoomStackProcesssing = source.internalZoomStackProcesssing;
                if (source.zoomRangeLimits != null)
                {
                    this.zoomRangeLimits = (Dimension)source.zoomRangeLimits.Clone();
                }
                if (source.zoomRangeLimitsRatio != null)
                {
                    this.zoomRangeLimitsRatio = (Dimension)source.zoomRangeLimitsRatio.Clone();
                }
            }
        }

        public override void Draw(Graphics g2)
        {
            double num7;
            if (this.superZoomFlag)
            {
                ((PhysicalCoordinates)this.superZoomScales[0]).SetCurrentAttributes(base.chartObjAttributes);
            }
            else
            {
                base.chartObjScale.SetCurrentAttributes(base.chartObjAttributes);
            }
            double x = this.zoomDevStartLoc.GetX();
            double num3 = this.zoomCurrentLocation.GetX();
            double y = this.zoomDevStartLoc.GetY();
            double num4 = this.zoomCurrentLocation.GetY();
            if (x > num3)
            {
                num7 = x;
                x = num3;
                num3 = num7;
            }
            if (y > num4)
            {
                num7 = y;
                y = num4;
                num4 = num7;
            }
            double num6 = num3 - x;
            double num5 = num4 - y;
            if (this.arCorrectionMode == 1)
            {
                num5 = (int)(num6 * base.chartObjScale.GetGraphAspectRatio());
            }
            else if (this.arCorrectionMode == 2)
            {
                num6 = (int)(num5 / base.chartObjScale.GetGraphAspectRatio());
            }
            Rectangle r = new Rectangle((int)x, (int)y, (int)num6, (int)num5);
            r = base.chartObjComponent.RectangleToScreen(r);
            base.chartObjScale.DrawReversibleFrame(r, Color.White, FrameStyle.Dashed);
        }

        public override int ErrorCheck(int nerror)
        {
            if (base.chartObjComponent == null)
            {
                nerror = 50;
            }
            if (this.superZoomFlag)
            {
                for (int i = 0; i < this.numberSuperZoomScales; i++)
                {
                    if (this.superZoomScales[i] == null)
                    {
                        nerror = 20;
                    }
                }
            }
            else if (this.GetChartObjScale() == null)
            {
                nerror = 20;
            }
            return base.ErrorCheck(nerror);
        }

        public MouseButtons GetZoomButtonMask()
        {
            return this.zoomStackButtonMask;
        }

        public Point2D GetZoomMax(int nmode)
        {
            Point2D dest = new Point2D();
            base.chartObjScale.ConvertCoord(dest, nmode, this.zoomDevEndLoc, 0);
            return dest;
        }

        public Point2D GetZoomMin(int nmode)
        {
            Point2D dest = new Point2D();
            base.chartObjScale.ConvertCoord(dest, nmode, this.zoomDevStartLoc, 0);
            return dest;
        }

        public Dimension GetZoomRangeLimits()
        {
            return (Dimension)this.zoomRangeLimits.Clone();
        }

        public Dimension GetZoomRangeLimitsRatio()
        {
            return (Dimension)this.zoomRangeLimitsRatio.Clone();
        }

        public bool GetZoomStackEnable()
        {
            return this.zoomStackEnable;
        }

        public bool GetZoomXEnable()
        {
            return this.zoomXEnable;
        }

        public int GetZoomXRoundMode()
        {
            return this.zoomXRoundMode;
        }

        public bool GetZoomYEnable()
        {
            return this.zoomYEnable;
        }

        public int GetZoomYRoundMode()
        {
            return this.zoomYRoundMode;
        }

        private void InitSimpleZoom(ChartView component, PhysicalCoordinates transform, bool brescale)
        {
            this.InitZoomDefaults();
            base.chartObjComponent = component;
            this.initialZoomCoordinates = transform;
            this.SetChartObjScale(transform);
            this.CalcZoomRangeLimits(this.initialZoomCoordinates);
            this.rescaleFlag = brescale;
            this.superZoomFlag = false;
            base.enabled = true;
        }

        private void InitSuperZoom(ChartView component, PhysicalCoordinates[] transforms, bool brescale)
        {
            int length = transforms.Length;
            this.InitZoomDefaults();
            if (length >= 1)
            {
                base.chartObjComponent = component;
                this.SetChartObjScale(transforms[0]);
                this.initialZoomCoordinates = transforms[0];
                this.CalcZoomRangeLimits(this.initialZoomCoordinates);
                this.numberSuperZoomScales = length;
                for (int i = 0; i < this.numberSuperZoomScales; i++)
                {
                    this.superZoomScales.Add(transforms[i]);
                }
                this.superZoomFlag = true;
                this.rescaleFlag = brescale;
                base.enabled = true;
            }
        }

        private void InitZoomDefaults()
        {
            base.chartObjType = 900;
            this.zoomObjActive = false;
            this.rescaleFlag = false;
            this.SetLineStyle(DashStyle.Dash);
            base.LineColor = Color.White;
            this.superZoomScales = new ArrayList(20);
        }

        public override void OnClick(EventArgs mouseevent)
        {
        }

        public override void OnDoubleClick(EventArgs mouseevent)
        {
        }

        public override void OnMouseDown(MouseEventArgs mouseevent)
        {
            if ((this.ErrorCheck(0) == 0) && base.enabled)
            {
                if ((mouseevent.Button & base.buttonMask) != MouseButtons.None)
                {
                    if (this.superZoomFlag)
                    {
                        ((PhysicalCoordinates)this.superZoomScales[0]).ChartTransform(base.tempGraphics);
                    }
                    else
                    {
                        base.chartObjScale.ChartTransform(base.tempGraphics);
                    }
                    this.zoomObjActive = true;
                    this.zoomDevLocation.SetLocation((double)mouseevent.X, (double)mouseevent.Y);
                    this.zoomDevStartLoc.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                    this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                    base.chartObjScale.StartXORMode(base.chartObjComponent, base.chartObjAttributes.PrimaryColor, 0);
                    this.Draw(base.tempGraphics);
                    this.PrevDevLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                }
                else if ((mouseevent.Button & this.zoomStackButtonMask) != MouseButtons.None)
                {
                    this.PopZoomStack();
                }
            }
        }

        public override void OnMouseMove(MouseEventArgs mouseevent)
        {
            MouseButtons button = mouseevent.Button;
            if (this.zoomObjActive && (button == base.buttonMask))
            {
                this.zoomDevLocation.SetLocation((double)mouseevent.X, (double)mouseevent.Y);
                this.zoomCurrentLocation.SetLocation(this.PrevDevLocation.GetX(), this.PrevDevLocation.GetY());
                this.Draw(base.tempGraphics);
                this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                this.Draw(base.tempGraphics);
                this.PrevDevLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
            }
        }

        public override void OnMouseUp(MouseEventArgs mouseevent)
        {
            if ((mouseevent.Button == base.buttonMask) & this.zoomObjActive)
            {
                this.zoomDevLocation.SetLocation((double)mouseevent.X, (double)mouseevent.Y);
                this.zoomCurrentLocation.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                this.Draw(base.tempGraphics);
                base.chartObjScale.EndXORMode(base.chartObjComponent);
                this.zoomDevEndLoc.SetLocation(this.zoomDevLocation.GetX(), this.zoomDevLocation.GetY());
                this.PushZoomStack();
                base.chartObjComponent.ResetPreviousChartObjectList();
                if (!this.superZoomFlag)
                {
                    this.ProcessSimpleZoom();
                }
                else
                {
                    this.ProcessSuperZoom();
                }
                this.zoomObjActive = false;
                base.tempGraphics = null;
            }
        }

        private int PopSimpleZoomStack()
        {
            int count = 0;
            if (this.zoomStackEnable)
            {
                if (this.zoomStack.Count == 0)
                {
                    return 0;
                }
                PhysicalCoordinates source = (PhysicalCoordinates)this.zoomStack.Pop();
                base.chartObjScale.Copy(source);
                this.RescaleAxesToTransform(base.chartObjScale);
                base.chartObjComponent.UpdateDraw();
                count = this.zoomStack.Count;
            }
            return count;
        }

        private int PopSuperZoomStack()
        {
            int count = 0;
            if (this.zoomStackEnable)
            {
                for (int i = 0; i < this.numberSuperZoomScales; i++)
                {
                    if (this.zoomStack.Count == 0)
                    {
                        return 0;
                    }
                    PhysicalCoordinates transform = (PhysicalCoordinates)this.superZoomScales[i];
                    PhysicalCoordinates source = (PhysicalCoordinates)this.zoomStack.Pop();
                    transform.Copy(source);
                    this.RescaleAxesToTransform(transform);
                }
                base.chartObjComponent.UpdateDraw();
                count = this.zoomStack.Count;
            }
            return count;
        }

        public int PopZoomStack()
        {
            base.chartObjComponent.ResetPreviousChartObjectList();
            if (!this.superZoomFlag)
            {
                return this.PopSimpleZoomStack();
            }
            return this.PopSuperZoomStack();
        }

        private void ProcessSimpleZoom()
        {
            int nroundmodeX = 1;
            int nroundmodeY = 1;
            this.SaveAxesIntercepts(base.chartObjScale);
            if (this.SetPhysZoomLocations() && this.rescaleFlag)
            {
                double num3 = Math.Abs((double)(this.zoomMaxLocation.GetX() - this.zoomMinLocation.GetX()));
                double num4 = Math.Abs((double)(this.zoomMaxLocation.GetY() - this.zoomMinLocation.GetY()));
                if ((num3 >= this.zoomRangeLimits.GetWidth()) && (num4 >= this.zoomRangeLimits.GetHeight()))
                {
                    if (this.zoomXEnable)
                    {
                        base.chartObjScale.SetScaleX(this.zoomMinLocation.GetX(), this.zoomMaxLocation.GetX());
                        nroundmodeX = this.zoomXRoundMode;
                    }
                    else
                    {
                        nroundmodeX = 0;
                    }
                    if (this.zoomYEnable)
                    {
                        base.chartObjScale.SetScaleY(this.zoomMinLocation.GetY(), this.zoomMaxLocation.GetY());
                        nroundmodeY = this.zoomYRoundMode;
                    }
                    else
                    {
                        nroundmodeY = 0;
                    }
                    base.chartObjScale.AutoScale(nroundmodeX, nroundmodeY);
                    this.RescaleAxesToTransform(base.chartObjScale);
                    base.chartObjComponent.UpdateDraw();
                }
            }
        }

        private void ProcessSuperZoom()
        {
            for (int i = 0; i < this.numberSuperZoomScales; i++)
            {
                PhysicalCoordinates transform = (PhysicalCoordinates)this.superZoomScales[i];
                this.SaveAxesIntercepts(transform);
                int nroundmodeX = 1;
                int nroundmodeY = 1;
                if (this.SetPhysSuperZoomLocations(transform) && this.rescaleFlag)
                {
                    if (i == 0)
                    {
                        double num4 = Math.Abs((double)(this.zoomMaxLocation.GetX() - this.zoomMinLocation.GetX()));
                        double num5 = Math.Abs((double)(this.zoomMaxLocation.GetY() - this.zoomMinLocation.GetY()));
                        if ((num4 < this.zoomRangeLimits.GetWidth()) || (num5 < this.zoomRangeLimits.GetHeight()))
                        {
                            return;
                        }
                    }
                    if (this.zoomXEnable)
                    {
                        transform.SetScaleX(this.zoomMinLocation.GetX(), this.zoomMaxLocation.GetX());
                        nroundmodeX = this.zoomXRoundMode;
                    }
                    else
                    {
                        nroundmodeX = 0;
                    }
                    if (this.zoomYEnable)
                    {
                        transform.SetScaleY(this.zoomMinLocation.GetY(), this.zoomMaxLocation.GetY());
                        nroundmodeY = this.zoomYRoundMode;
                    }
                    else
                    {
                        nroundmodeY = 0;
                    }
                    transform.AutoScale(nroundmodeX, nroundmodeY);
                    this.RescaleAxesToTransform(transform);
                }
            }
            base.chartObjComponent.UpdateDraw();
        }

        private int PushChartScale(PhysicalCoordinates chartscale)
        {
            int count = 0;
            if (this.zoomStackEnable)
            {
                this.zoomStack.Push(chartscale.Clone());
                count = this.zoomStack.Count;
            }
            return count;
        }

        private int PushSimpleZoomStack()
        {
            int num = 0;
            if (this.zoomStackEnable)
            {
                num = this.PushChartScale(base.chartObjScale);
            }
            return num;
        }

        private int PushSuperZoomStack()
        {
            int num = 0;
            if (this.zoomStackEnable)
            {
                for (int i = 0; i < this.numberSuperZoomScales; i++)
                {
                    PhysicalCoordinates chartscale = (PhysicalCoordinates)this.superZoomScales[(this.numberSuperZoomScales - i) - 1];
                    num = this.PushChartScale(chartscale);
                }
            }
            return num;
        }

        public int PushZoomStack()
        {
            if (!this.superZoomFlag)
            {
                return this.PushSimpleZoomStack();
            }
            return this.PushSuperZoomStack();
        }

        private void RescaleAxesToTransform(PhysicalCoordinates transform)
        {
            int count = base.chartObjComponent.GetChartObjectsArrayList().Count;
            GraphObj obj2 = null;
            Point2D pointd = new Point2D();
            Point2D pointd2 = new Point2D();
            for (int i = 0; i < count; i++)
            {
                obj2 = (GraphObj)base.chartObjComponent.GetChartObjectsArrayList()[i];
                if (((obj2 != null) && ((((obj2.GetChartObjType() == 100) || (obj2.GetChartObjType() == 0x65)) || ((obj2.GetChartObjType() == 0x68) || (obj2.GetChartObjType() == 0x66))) || (obj2.GetChartObjType() == 0x67))) && (obj2.GetChartObjScale() == transform))
                {
                    Axis axis = (Axis)obj2;
                    if ((axis.AxisType == 0) && this.zoomXEnable)
                    {
                        axis.CalcAutoAxis();
                    }
                    else if ((axis.AxisType == 1) && this.zoomYEnable)
                    {
                        axis.CalcAutoAxis();
                    }
                    axis.RestoreAxisNormIntercept();
                }
            }
        }

        private void SaveAxesIntercepts(PhysicalCoordinates transform)
        {
            int count = base.chartObjComponent.GetChartObjectsArrayList().Count;
            GraphObj obj2 = null;
            for (int i = 0; i < count; i++)
            {
                obj2 = (GraphObj)base.chartObjComponent.GetChartObjectsArrayList()[i];
                if (((obj2 != null) && ((((obj2.GetChartObjType() == 100) || (obj2.GetChartObjType() == 0x65)) || ((obj2.GetChartObjType() == 0x68) || (obj2.GetChartObjType() == 0x66))) || (obj2.GetChartObjType() == 0x67))) && (obj2.GetChartObjScale() == transform))
                {
                    ((Axis)obj2).CalcAxisNormIntercept();
                }
            }
        }

        private bool SetPhysSuperZoomLocations(PhysicalCoordinates atransform)
        {
            double num5;
            double x = this.zoomDevStartLoc.GetX();
            double px = this.zoomDevEndLoc.GetX();
            double y = this.zoomDevStartLoc.GetY();
            double py = this.zoomDevEndLoc.GetY();
            if ((Math.Abs((double)(px - x)) < 4.0) || (Math.Abs((double)(y - py)) < 4.0))
            {
                return false;
            }
            if (x > px)
            {
                num5 = x;
                x = px;
                px = num5;
            }
            if (y < py)
            {
                num5 = y;
                y = py;
                py = num5;
            }
            Point2D source = new Point2D(x, y);
            Point2D pointd2 = new Point2D(px, py);
            atransform.ConvertCoord(this.zoomMinLocation, 1, source, 0);
            atransform.ConvertCoord(this.zoomMaxLocation, 1, pointd2, 0);
            return true;
        }

        private bool SetPhysZoomLocations()
        {
            double num5;
            double x = this.zoomDevStartLoc.GetX();
            double px = this.zoomDevEndLoc.GetX();
            double y = this.zoomDevStartLoc.GetY();
            double py = this.zoomDevEndLoc.GetY();
            if ((Math.Abs((double)(px - x)) < 4.0) || (Math.Abs((double)(y - py)) < 4.0))
            {
                return false;
            }
            if (x > px)
            {
                num5 = x;
                x = px;
                px = num5;
            }
            if (y < py)
            {
                num5 = y;
                y = py;
                py = num5;
            }
            Point2D source = new Point2D(x, y);
            Point2D pointd2 = new Point2D(px, py);
            base.chartObjScale.ConvertCoord(this.zoomMinLocation, 1, source, 0);
            base.chartObjScale.ConvertCoord(this.zoomMaxLocation, 1, pointd2, 0);
            return true;
        }

        public void SetZoomButtonMask(MouseButtons buttonmask)
        {
            this.zoomStackButtonMask = buttonmask;
        }

        public void SetZoomRangeLimits(Dimension limits)
        {
            this.zoomRangeLimits = (Dimension)limits.Clone();
        }

        public void SetZoomRangeLimitsRatio(Dimension ratio)
        {
            this.zoomRangeLimitsRatio = (Dimension)ratio.Clone();
            this.CalcZoomRangeLimits(this.initialZoomCoordinates);
        }

        public void SetZoomStackEnable(bool on)
        {
            this.zoomStackEnable = on;
        }

        public void SetZoomXEnable(bool bzoomx)
        {
            this.zoomXEnable = bzoomx;
        }

        public void SetZoomXRoundMode(int nzoomx)
        {
            this.zoomXRoundMode = nzoomx;
        }

        public void SetZoomYEnable(bool bzoomy)
        {
            this.zoomYEnable = bzoomy;
        }

        public void SetZoomYRoundMode(int nzoomy)
        {
            this.zoomYRoundMode = nzoomy;
        }

        public int ArCorrectionMode
        {
            get
            {
                return this.arCorrectionMode;
            }
            set
            {
                this.arCorrectionMode = value;
            }
        }

        public bool InternalZoomStackProcesssing
        {
            get
            {
                return this.internalZoomStackProcesssing;
            }
            set
            {
                this.internalZoomStackProcesssing = value;
                this.zoomStackEnable = value;
            }
        }

        public bool ZoomObjActive
        {
            get
            {
                return this.zoomObjActive;
            }
        }

        public MouseButtons ZoomStackButtonMask
        {
            get
            {
                return this.zoomStackButtonMask;
            }
            set
            {
                this.zoomStackButtonMask = value;
            }
        }

        public bool ZoomStackEnable
        {
            get
            {
                return this.zoomStackEnable;
            }
            set
            {
                this.zoomStackEnable = value;
            }
        }

        public bool ZoomXEnable
        {
            get
            {
                return this.zoomXEnable;
            }
            set
            {
                this.zoomXEnable = value;
            }
        }

        public int ZoomXRoundMode
        {
            get
            {
                return this.zoomXRoundMode;
            }
            set
            {
                this.zoomXRoundMode = value;
            }
        }

        public bool ZoomYEnable
        {
            get
            {
                return this.zoomYEnable;
            }
            set
            {
                this.zoomYEnable = value;
            }
        }

        public int ZoomYRoundMode
        {
            get
            {
                return this.zoomYRoundMode;
            }
            set
            {
                this.zoomYRoundMode = value;
            }
        }
    }
}

