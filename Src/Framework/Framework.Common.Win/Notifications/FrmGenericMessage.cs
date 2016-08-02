using System;
using System.Drawing;
using System.Windows.Forms;
using Framework.Common.Win.Resources;
using Framework.WinForm.Comun.Notification;

namespace Framework.Common.Win.Notifications
{
    public partial class FrmGenericMessage : Form
    {
        public GenericMessage Message { get; set; }

        public FrmGenericMessage(GenericMessage mensaje)
        {
            InitializeComponent();
            Message = mensaje;
        }

        private void FrmMensajeGenerico_Load(object sender, EventArgs e)
        {
            try
            {
                switch (Message.Type)
                {
                    case GenericMessageType.Warning :
                        picImagen.Image = Resource.Warning;
                        this.Icon = Icon.FromHandle(Resource.Warning.GetHicon());
                        break;
                    case GenericMessageType.Error:
                        picImagen.Image = Resource.Error;
                        this.Icon = Icon.FromHandle(Resource.Error.GetHicon());
                        break;
                    case GenericMessageType.Info:
                        picImagen.Image = Resource.Information;
                        this.Icon = Icon.FromHandle(Resource.Information.GetHicon());
                        break;
                    default:
                        picImagen.Image = Resource.Check;
                        this.Icon = Icon.FromHandle(Resource.Check.GetHicon());
                        break;
                }

                lblTitle.Text = Message.Title;
                lblMessage.Text = Message.Message;

                var alturaBotones = 75 + lblMessage.Size.Height;
                btnAceptar.Location = new Point(btnAceptar.Location.X, alturaBotones);
                this.Height = alturaBotones + 40;

                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
