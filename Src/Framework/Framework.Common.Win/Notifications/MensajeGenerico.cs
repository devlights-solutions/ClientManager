namespace Framework.WinForm.Comun.Notification
{
    public class GenericMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public GenericMessageType Type { get; set; }
    }

    public enum GenericMessageType
    {
        Error,
        Warning,
        Success,
        Info
    }
}
