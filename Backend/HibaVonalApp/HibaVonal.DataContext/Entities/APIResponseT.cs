namespace LibraryCommon.Models
{
    public class APIResponse<TData>
    {
        public int StatusCode { get; set; } = 0;
        public string? Message { get; set; }
        public TData Data { get; set; } = default;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
