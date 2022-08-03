namespace WebApiProductCRUD.DataResult
{
    public class StatusResult<T>
    {
        public StatusResult(params string[] messages)
        {
            foreach (var message in messages)
                Messages.Add(message);
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T? Model { get; set; }

        public StatusResult<T> WithMessage(string message)
        {
            Messages.Add(message);
            return this;
        }
    }
}
