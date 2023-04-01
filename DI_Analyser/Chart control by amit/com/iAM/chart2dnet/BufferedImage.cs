namespace com.iAM.chart2dnet
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class BufferedImage : ChartObj
    {
        internal Bitmap bufferedImage;
        internal Metafile bufferedImageMetafile;
        internal ImageFormat chartImageFormat;
        internal ChartView chartViewComponent;
        internal EmfType imageEmfType;
        internal int jpegImageQuality;

        public BufferedImage()
        {
            this.bufferedImageMetafile = null;
            this.imageEmfType = EmfType.EmfOnly;
            this.chartImageFormat = ImageFormat.Jpeg;
            this.jpegImageQuality = 100;
            this.chartViewComponent = null;
            this.bufferedImage = null;
            this.InitDefaults();
        }

        public BufferedImage(ChartView component)
        {
            this.bufferedImageMetafile = null;
            this.imageEmfType = EmfType.EmfOnly;
            this.chartImageFormat = ImageFormat.Jpeg;
            this.jpegImageQuality = 100;
            this.chartViewComponent = null;
            this.bufferedImage = null;
            this.InitDefaults();
            this.chartViewComponent = component;
        }

        public BufferedImage(ChartView component, ImageFormat imgformat)
        {
            this.bufferedImageMetafile = null;
            this.imageEmfType = EmfType.EmfOnly;
            this.chartImageFormat = ImageFormat.Jpeg;
            this.jpegImageQuality = 100;
            this.chartViewComponent = null;
            this.bufferedImage = null;
            this.InitDefaults();
            this.chartViewComponent = component;
            this.chartImageFormat = imgformat;
        }

        public override object Clone()
        {
            BufferedImage image = new BufferedImage();
            image.Copy(this);
            return image;
        }

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();
        public void Copy(BufferedImage source)
        {
            if (source != null)
            {
                base.Copy(source);
                this.chartViewComponent = source.chartViewComponent;
                this.bufferedImage = source.bufferedImage;
                this.chartImageFormat = source.chartImageFormat;
            }
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteEnhMetaFile(IntPtr hemf);
        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();
        public override int ErrorCheck(int nerror)
        {
            if (this.chartViewComponent == null)
            {
                nerror = 50;
            }
            return base.ErrorCheck(nerror);
        }

        public Image GetBufferedImage()
        {
            if (this.bufferedImage == null)
            {
                this.Render();
            }
            return this.bufferedImage;
        }

        public Metafile GetBufferedImageMetafile()
        {
            if (this.bufferedImageMetafile == null)
            {
                this.Render();
            }
            return this.bufferedImageMetafile;
        }

        public ImageFormat GetChartImageFormat()
        {
            return this.chartImageFormat;
        }

        public ChartView GetChartViewComponent()
        {
            return this.chartViewComponent;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }

        public int GetJpegImageQuality()
        {
            return this.jpegImageQuality;
        }

        private void InitDefaults()
        {
            base.chartObjType = 510;
        }

        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);
        public static bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
        {
            bool flag = false;
            IntPtr henhmetafile = mf.GetHenhmetafile();
            if (!henhmetafile.Equals(new IntPtr(0)))
            {
                IntPtr ptr2 = CopyEnhMetaFile(henhmetafile, new IntPtr(0));
                if ((!ptr2.Equals(new IntPtr(0)) && OpenClipboard(hWnd)) && EmptyClipboard())
                {
                    flag = SetClipboardData(14, ptr2).Equals(ptr2);
                    CloseClipboard();
                }
                DeleteEnhMetaFile(henhmetafile);
            }
            return flag;
        }

        public void PutImageOnClipboard()
        {
            this.PutImageOnClipboard(IntPtr.Zero);
        }

        public void PutImageOnClipboard(IntPtr hWnd)
        {
            this.Render();
            if (this.chartImageFormat.Equals(ImageFormat.Emf) || this.chartImageFormat.Equals(ImageFormat.Wmf))
            {
                PutEnhMetafileOnClipboard(hWnd, this.bufferedImageMetafile);
            }
            else
            {
                Clipboard.SetDataObject(this.bufferedImage, true);
            }
        }

        public void Render()
        {
            if (this.chartImageFormat.Equals(ImageFormat.Emf) || this.chartImageFormat.Equals(ImageFormat.Wmf))
            {
                this.RenderMetafile();
            }
            else
            {
                this.RenderBitmap();
            }
        }

        public void RenderBitmap()
        {
            int width = this.chartViewComponent.Width;
            int height = this.chartViewComponent.Height;
            Graphics g = this.chartViewComponent.CreateGraphics();
            if (g != null)
            {
                this.bufferedImage = new Bitmap(width, height, g);
                if (this.bufferedImage != null)
                {
                    Graphics graphics2 = Graphics.FromImage(this.bufferedImage);
                    if (graphics2 != null)
                    {
                        this.chartViewComponent.ResetPreviousChartObjectList();
                        this.chartViewComponent.RenderingMode = 1;
                        this.chartViewComponent.Draw(graphics2);
                        this.chartViewComponent.RenderingMode = 0;
                        graphics2.Dispose();
                    }
                }
                g.Dispose();
            }
        }

        public void RenderMetafile()
        {
            int width = this.chartViewComponent.Width;
            Graphics graphics = this.chartViewComponent.CreateGraphics();
            IntPtr hdc = graphics.GetHdc();
            this.bufferedImageMetafile = new Metafile(hdc, this.imageEmfType);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
            graphics = Graphics.FromImage(this.bufferedImageMetafile);
            if (graphics != null)
            {
                this.chartViewComponent.ResetPreviousChartObjectList();
                this.chartViewComponent.RenderingMode = 1;
                this.chartViewComponent.Draw(graphics);
                this.chartViewComponent.RenderingMode = 0;
                graphics.Dispose();
            }
        }

        public void SaveImage(Stream s)
        {
            this.Render();
            if (this.chartImageFormat.Equals(ImageFormat.Emf) || this.chartImageFormat.Equals(ImageFormat.Wmf))
            {
                this.bufferedImageMetafile.Save(s, this.chartImageFormat);
            }
            else if (this.jpegImageQuality == 100)
            {
                this.bufferedImage.Save(s, this.chartImageFormat);
            }
            else
            {
                this.SaveJPGWithCompressionSetting(this.bufferedImage, s, (long) this.jpegImageQuality);
            }
        }

        public void SaveImage(string s)
        {
            this.Render();
            if (this.chartImageFormat.Equals(ImageFormat.Emf) || this.chartImageFormat.Equals(ImageFormat.Wmf))
            {
                this.bufferedImageMetafile.Save(s, this.chartImageFormat);
            }
            else if (this.jpegImageQuality == 100)
            {
                this.bufferedImage.Save(s, this.chartImageFormat);
            }
            else
            {
                this.SaveJPGWithCompressionSetting(this.bufferedImage, s, (long) this.jpegImageQuality);
            }
        }

        private void SaveJPGWithCompressionSetting(Image image, Stream stream, long lCompression)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, lCompression);
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            image.Save(stream, encoderInfo, encoderParams);
        }

        private void SaveJPGWithCompressionSetting(Image image, string filename, long lCompression)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, lCompression);
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            image.Save(filename, encoderInfo, encoderParams);
        }

        public void SetChartImageFormat(ImageFormat imgformat)
        {
            this.chartImageFormat = imgformat;
        }

        public void SetChartViewComponent(ChartView component)
        {
            this.chartViewComponent = component;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
        public void SetJpegImageQuality(int quality)
        {
            quality = Math.Max(1, quality);
            this.jpegImageQuality = Math.Min(100, quality);
        }

        public ImageFormat ChartImageFormat
        {
            get
            {
                return this.chartImageFormat;
            }
            set
            {
                this.chartImageFormat = value;
            }
        }

        public EmfType ImageEmfType
        {
            get
            {
                return this.imageEmfType;
            }
            set
            {
                this.imageEmfType = value;
            }
        }

        public int JpegImageQuality
        {
            get
            {
                return this.jpegImageQuality;
            }
            set
            {
                this.SetJpegImageQuality(value);
            }
        }
    }
}

