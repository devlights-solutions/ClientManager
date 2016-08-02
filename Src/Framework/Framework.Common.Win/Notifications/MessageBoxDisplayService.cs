using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework.Common.Win.Notifications;
using Telerik.WinControls;

namespace Framework.WinForm.Comun.Notification
{
    public class MessageBoxDisplayService : IMessageBoxDisplayService
    {
        public MessageBoxDisplayService()
        {

        }

        public void Show(string message, string title)
        {
            this.Show(Form.ActiveForm, message, title);
        }

        public void Show(IWin32Window owner, string message, string title)
        {
            RadMessageBox.Instance.ThemeName = "TelerikMetroBlue";
            RadMessageBox.Instance.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RadMessageBox.Show(owner, message, title);
        }

        public void ShowError(string message)
        {
            var msjerror = new GenericMessage
            {
                Message = message,
                Type = GenericMessageType.Error,
                Title = "Error"
            };
            using (var form = new FrmGenericMessage(msjerror))
            {
                form.ShowDialog();
            }
        }

        public void ShowWarning(string message)
        {
            var msjWarning = new GenericMessage
            {
                Message = message,
                Type = GenericMessageType.Warning,
                Title = "Advertencia"
            };
            using (var form = new FrmGenericMessage(msjWarning))
            {
                form.ShowDialog();
            }
        }

        public void ShowSuccess(string message)
        {
            var msjsuccess = new GenericMessage
            {
                Message = message,
                Type = GenericMessageType.Success,
                Title = "Éxito"
            };
            using (var form = new FrmGenericMessage(msjsuccess))
            {
                form.ShowDialog();
            }
        }

        public void ShowConfirmationDialog(string message, string title, Action okAction)
        {
            ShowConfirmationDialog(message, title, okAction, null);
        }

        public void ShowConfirmationDialog(string message, string title, Action okAction, Action cancelAction)
        {
            RadMessageBox.Instance.ThemeName = "TelerikMetroBlue";
            RadMessageBox.Instance.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            var result = RadMessageBox.Show(Form.ActiveForm, message, title, MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                if (okAction != null)
                {
                    okAction();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                if (cancelAction != null)
                {
                    cancelAction();
                }
            }
        }

        public void ShowInfo(string message)
        {
            var msjInfo = new GenericMessage
            {
                Message = message,
                Type = GenericMessageType.Info,
                Title = "Información"
            };
            using (var form = new FrmGenericMessage(msjInfo))
            {
                form.ShowDialog();
            }
        }
    }
}
