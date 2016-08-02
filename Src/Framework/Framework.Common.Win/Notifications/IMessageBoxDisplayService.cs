using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Framework.WinForm.Comun.Notification
{
    public interface IMessageBoxDisplayService
    {
        void Show(string message,string title);
        void Show(IWin32Window owner, string message, string title);
        void ShowError(string message);
        void ShowWarning(string message);
        void ShowSuccess(string message);
        void ShowInfo(string message);
        void ShowConfirmationDialog(string message, string title, Action okAction);
        void ShowConfirmationDialog(string message, string title, Action okAction, Action cancelAction);
    }
}
