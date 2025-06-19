namespace Core.Filters
{
    public interface IFilterBase
    {
        DateTime? DateTimeFrom { get; set; }
        DateTime? DateTimeTo { get; set; }
        string? IpAddress { get; set; }
        string? Login {  get; set; }
        string? SearchString { get; set; }
        void ClearFilterBase();
    }
}
