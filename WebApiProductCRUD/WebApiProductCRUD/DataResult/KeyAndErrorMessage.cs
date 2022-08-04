namespace WebApiProductCRUD.DataResult
{
    public class KeyAndErrorMessage
    {
        public string Key { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public KeyAndErrorMessage() { }

        public KeyAndErrorMessage(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
