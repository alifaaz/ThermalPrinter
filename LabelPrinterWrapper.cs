using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace BarcodeLabels
{
    public static  class LabelPrinterWrapper
    {

        public static void PrintSmallLabel50_25(string businessName, string barcodeText, string price, string itemName,bool preview = false)
        {
            PrintDocument pd = new PrintDocument();

            pd.DefaultPageSettings.PaperSize = new PaperSize("Label50x25", 197, 98);
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.PageUnit = GraphicsUnit.Display;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                var boldFont = new Font("Tahoma", 8, FontStyle.Bold);
                var regFont = new Font("Tahoma", 7);

                int pageWidth = e.PageBounds.Width;
                int y = 5;

                var centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                var rtlFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };

                var leftFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };

                // 1. اسم البزنس
                g.DrawString(businessName, boldFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 14), centerFormat);
                y += 16;

                // 2. الباركود
                int barcodeHeight = 40;
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = 300,
                        Height = barcodeHeight,
                        Margin = 0,
                        PureBarcode = true
                    },
                    Renderer = new BitmapRenderer()
                };

                var barcodeBitmap = barcodeWriter.Write(barcodeText);
                Rectangle destRect = new Rectangle(0, y, pageWidth, barcodeHeight);
                Rectangle srcRect = new Rectangle(0, 0, barcodeBitmap.Width, barcodeBitmap.Height);
                g.DrawImage(barcodeBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                y += barcodeHeight;

                // 3. رقم الباركود
                g.DrawString(barcodeText, regFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 14), centerFormat);
                y += 16;

                // 4. السعر واسم المادة في نفس السطر
                int halfWidth = pageWidth / 2;

                // السعر - يمين
                g.DrawString($"السعر: {price}", regFont, Brushes.Black,
                    new RectangleF(halfWidth, y, halfWidth - 5, 14), rtlFormat);

                // اسم المادة - يسار
                g.DrawString(itemName, regFont, Brushes.Black,
                    new RectangleF(5, y, halfWidth - 10, 14), leftFormat);
            };

            //PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            //previewDialog.Document = pd;
            //previewDialog.ShowDialog();
            if (preview)
                Preview(pd);
            else
                pd.Print(); // ← للطباعة المباشرة

        }

        public static void PrintSmallLabel60_40(string businessName, string barcodeText, string price, string itemName, bool preview = false)
        {
            PrintDocument pd = new PrintDocument();

            // ← نغير أبعاد الورقة إلى 60x40 mm
            pd.DefaultPageSettings.PaperSize = new PaperSize("Label60x40", 236, 157);
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.PageUnit = GraphicsUnit.Display;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                var boldFont = new Font("Tahoma", 10, FontStyle.Bold);  // ← زيدنا الخط شوي
                var regFont = new Font("Tahoma", 8);

                int pageWidth = e.PageBounds.Width;
                int y = 5;

                var centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                var rtlFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };

                var leftFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };

                // 1. اسم البزنس
                g.DrawString(businessName, boldFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 18), centerFormat);
                y += 20;

                // 2. الباركود
                int barcodeHeight = 50;
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = 300,
                        Height = barcodeHeight,
                        Margin = 0,
                        PureBarcode = true
                    },
                    Renderer = new BitmapRenderer()
                };

                var barcodeBitmap = barcodeWriter.Write(barcodeText);
                Rectangle destRect = new Rectangle(0, y, pageWidth, barcodeHeight);
                Rectangle srcRect = new Rectangle(0, 0, barcodeBitmap.Width, barcodeBitmap.Height);
                g.DrawImage(barcodeBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                y += barcodeHeight;

                // 3. رقم الباركود
                g.DrawString(barcodeText, regFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 14), centerFormat);
                y += 18;

                // 4. السعر واسم المادة في نفس السطر
                int halfWidth = pageWidth / 2;

                g.DrawString($"السعر: {price}", regFont, Brushes.Black,
                    new RectangleF(halfWidth, y, halfWidth - 5, 16), rtlFormat);

                g.DrawString(itemName, regFont, Brushes.Black,
                    new RectangleF(5, y, halfWidth - 10, 16), leftFormat);
            };

            // معاينة قبل الطباعة (اختياري)
            // PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            // previewDialog.Document = pd;
            // previewDialog.ShowDialog();
            if (preview)
                Preview(pd);
            else
                pd.Print(); // ← للطباعة المباشرة
        }


        public static void PrintSmallLabel38_25(string businessName, string barcodeText, string price, string itemName, bool preview = false)
        {
            PrintDocument pd = new PrintDocument();

            // ✅ حجم الملصق: 38x25 mm
            pd.DefaultPageSettings.PaperSize = new PaperSize("Label38x25", 149, 98);
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.PageUnit = GraphicsUnit.Display;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                var boldFont = new Font("Tahoma", 8, FontStyle.Bold);
                var regFont = new Font("Tahoma", 7);

                int pageWidth = e.PageBounds.Width;
                int y = 2;

                var centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                var rtlFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };

                var leftFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };

                // 1. اسم البزنس
                g.DrawString(businessName, boldFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 12), centerFormat);
                y += 14;

                // 2. الباركود
                int barcodeHeight = 30;
                var barcodeWriter = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = 250,
                        Height = barcodeHeight,
                        Margin = 0,
                        PureBarcode = true
                    },
                    Renderer = new BitmapRenderer()
                };

                var barcodeBitmap = barcodeWriter.Write(barcodeText);
                Rectangle destRect = new Rectangle(0, y, pageWidth, barcodeHeight);
                Rectangle srcRect = new Rectangle(0, 0, barcodeBitmap.Width, barcodeBitmap.Height);
                g.DrawImage(barcodeBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                y += barcodeHeight;

                // 3. رقم الباركود
                g.DrawString(barcodeText, regFont, Brushes.Black,
                    new RectangleF(0, y, pageWidth, 12), centerFormat);
                y += 14;

                // 4. السعر واسم المادة
                int halfWidth = pageWidth / 2;

                g.DrawString($"السعر: {price}", regFont, Brushes.Black,
                    new RectangleF(halfWidth, y, halfWidth - 5, 12), rtlFormat);

                g.DrawString(itemName, regFont, Brushes.Black,
                    new RectangleF(2, y, halfWidth - 4, 12), leftFormat);
            };

            // معاينة قبل الطباعة
            // PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            // previewDialog.Document = pd;
            // previewDialog.ShowDialog();

            if (preview)
                Preview(pd);
            else
                pd.Print();  // ← طباعة مباشرة
        }

        public static void Preview(PrintDocument pd)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = pd;
            previewDialog.ShowDialog();
        }
    }
}
