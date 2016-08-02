using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;

namespace Framework.Common.Win.CustomProviders
{
    public class CustomRadMessageLocalizationProvider : RadMessageLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadMessageStringID.AbortButton: return "Abortar";
                case RadMessageStringID.CancelButton: return "Cancelar";
                case RadMessageStringID.IgnoreButton: return "Ignorar";
                case RadMessageStringID.NoButton: return "No";
                case RadMessageStringID.OKButton: return "OK";
                case RadMessageStringID.RetryButton: return "Reintentar";
                case RadMessageStringID.YesButton: return "Si";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}
