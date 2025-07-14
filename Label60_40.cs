using BarcodeLabels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thermaPrinter.OOP
{
    internal class Label60_40: BaseLabelPrinter
    {
        protected override int WidthMm => 60;
        protected override int HeightMm => 40;
        protected override int BarcodeHeight => 50;
        protected override int FontSizeTitle => 10;
        protected override int FontSizeText => 8;
    }
}
