namespace WebApiProductCRUD.DataResult
{
    public class GetListResult<T>
    {
        public bool Success { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public List<string> Messages { get; set; } = new List<string>();

        public static GetListResult<T> SuccessWithItems(IEnumerable<T> items)
        {
            return new GetListResult<T>
            {
                Success = true,
                Items = items
            };
        }

        public static GetListResult<T> ErrorWithMessages(params string[] messages)
        {
            var result = new GetListResult<T>();
            result.Messages.AddRange(messages);
            return result;
        }

    }
}
