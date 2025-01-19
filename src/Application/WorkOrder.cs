namespace Application;

public class WorkOrder
{
    public string Id { get; set; }
    public string Specification { get; set; }
    public string Machine { get; set; }
    public string Status { get; set; } = "Released";
    public DateTime CreationTime { get; set; } = DateTime.Now;
}