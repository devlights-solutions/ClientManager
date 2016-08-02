using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace Framework.Common.Win.CustomProviders
{
    public class CustomRadWizardLocalizationProvider : RadWizardLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadWizardStringId.BackButtonText: return "<   Atras";
                case RadWizardStringId.NextButtonText: return "Siguiente   >";
                case RadWizardStringId.CancelButtonText: return "Cancelar";
                case RadWizardStringId.FinishButtonText: return "Finalizar";
                case RadWizardStringId.HelpButtonText: return "<html><u>Ayuda</u></html>";
                default: return string.Empty;
            }
        }
    }
}
