namespace MyBookStore.Communication.Responses;

public class ResponseBookJson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Gender { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
