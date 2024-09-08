namespace Infrastructure.DTOs.Common
{
    public class BaseResponse<T>
    {
        public List<T> Results { get; set; } = new List<T>();
        public List<string> Errors { get; set; } = new List<string>();
        public int Total { get; set; } = new int();
        public int Page { get; set; } = new int();
        public int PageSize { get; set; } = new int();

    }
}
