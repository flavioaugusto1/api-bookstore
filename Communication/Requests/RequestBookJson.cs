namespace MyBookStore.Communication.Requests;

public class RequestBookJson
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Gender { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}
