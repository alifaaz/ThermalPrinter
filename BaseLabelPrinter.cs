using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace BarcodeLabels
{
    public abstract class BaseLabelPrinter : ILabelPrinter
    {
        protected abstract int WidthMm { get; }
        protected abstract int HeightMm { get; }
        protected abstract int BarcodeHeight { get; }
        protected abstract int FontSizeTitle { get; }
        protected abstract int FontSizeText { get; }
        protected virtual bool ShowPriceAndItemSameLine => true;

        public void Print(string businessName, string barcodeText, string price, string itemName, bool preview = false)
        {
            int dpi = 4;
            int widthPx = WidthMm * dpi;
            int heightPx = HeightMm * dpi;

            PrintDocument pd = new PrintDocument
            {
                DefaultPageSettings =
            {
                PaperSize = new PaperSize(GetType().Name, widthPx, heightPx),
                Margins = new Margins(0, 0, 0, 0)
            }
            };

            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.PageUnit = GraphicsUnit.Display;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                var boldFont = new Font("Tahoma", FontSizeTitle, FontStyle.Bold);
                var regFont = new Font("Tahoma", FontSizeText);

                int pageWidth = e.PageBounds.Width;
                int y = 5;

                var centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                var rtlFormat = new StringFormat { Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.DirectionRightToLeft };
                var leftFormat = new StringFormat { Alignment = StringAlignment.Near };

                // 1. اسم البزنس
                g.DrawString(businessName, boldFont, Brushes.Black, new RectangleF(0, y, pageWidth, 14), centerFormat);
                y += FontSizeTitle + 6;

                // 2. الباركود
                var barcodeWriter = new ZXing.BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = 300,
                        Height = BarcodeHeight,
                        PureBarcode = true,
                        Margin = 0,

                    },
                    Renderer = new BitmapRenderer()
                };

                var barcodeBitmap = barcodeWriter.Write(barcodeText);
                g.DrawImage(barcodeBitmap, new Rectangle(0, y, pageWidth, BarcodeHeight));
                y += BarcodeHeight;

                // 3. رقم الباركود
                g.DrawString(barcodeText, regFont, Brushes.Black, new RectangleF(0, y, pageWidth, 14), centerFormat);
                y += FontSizeText + 6;

                // 4. السعر واسم المادة
                if (ShowPriceAndItemSameLine)
                {
                    int halfWidth = pageWidth / 2;
                    g.DrawString($"السعر: {price}", regFont, Brushes.Black, new RectangleF(halfWidth, y, halfWidth - 5, 14), rtlFormat);
                    g.DrawString(itemName, regFont, Brushes.Black, new RectangleF(5, y, halfWidth - 10, 14), leftFormat);
                }
                else
                {
                    g.DrawString($"السعر: {price}", regFont, Brushes.Black, new RectangleF(0, y, pageWidth, 14), rtlFormat);
                    y += FontSizeText + 4;
                    g.DrawString(itemName, regFont, Brushes.Black, new RectangleF(0, y, pageWidth, 14), centerFormat);
                }
            };

            if (preview)
            {
                var previewDialog = new PrintPreviewDialog { Document = pd };
                previewDialog.ShowDialog();
            }
            else
            {
                pd.Print();
            }
        }
    }

}
