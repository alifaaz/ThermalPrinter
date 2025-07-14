using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLabels
{
    internal interface ILabelPrinter
    {
        void Print(string businessName, string barcodeText, string price, string itemName, bool preview = false);

    }
}
