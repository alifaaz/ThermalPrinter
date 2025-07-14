using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLabels
{
    internal class LabelPrinterFactory
    {
        public static ILabelPrinter Create(LabelSize size)
        {
            switch (size)
            {
                case LabelSize.Label60x40:
                    return new Label60_40();
                case LabelSize.Label38x25:
                    return new Label38_25();
                default:
                    throw new NotSupportedException("Unsupported label size");
            }
        }
    }
}
