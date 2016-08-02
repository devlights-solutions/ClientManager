using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Framework.Common.Win.Controls
{
    public class NumericTextbox : RadTextBox
    {
        protected override void OnTextChanging(TextChangingEventArgs e)
        {
            e.Cancel = !IsNumber(e.NewValue);
            base.OnTextChanging(e);
        }

        private bool IsNumber(string text)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(text) && ((text.Length != 1) || (text != "-")))
                {
                    Decimal d = decimal.Parse(text, CultureInfo.CurrentCulture);
                }
            }
            catch
            {
                res = false;
            }
            return res;
        }
    }
}
