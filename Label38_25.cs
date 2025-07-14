using BarcodeLabels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thermaPrinter.OOP
{
    internal class Label38_25 : BaseLabelPrinter
    {
        protected override int WidthMm => 38;
        protected override int HeightMm => 25;
        protected override int BarcodeHeight => 30;
        protected override int FontSizeTitle => 8;
        protected override int FontSizeText => 7;

     

    }
}
