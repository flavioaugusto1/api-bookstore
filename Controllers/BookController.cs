using Microsoft.AspNetCore.Mvc;
using MyBookStore.Communication.Requests;
using MyBookStore.Communication.Responses;

namespace MyBookStore.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{

    private static readonly List<Book> books = [];

    [HttpPost]
    public IActionResult CreateBook([FromBody] RequestBookJson request)
    {
        var response = MapToEntity(request);

        books.Add(response);

        return CreatedAtAction(nameof(GetBookById), new { id = response.Id }, MapToResponse(response));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBookById(int id)
    {
        var book = books.FirstOrDefault(x => x.Id == id);

        return book is null ? NotFound() : Ok(MapToResponse(book));
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        return books.Count == 0 ? Ok(new
        {
            message = "Você não possui livros cadastrados."
        }) : Ok(books);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        var book = books.FirstOrDefault(item => item.Id == id);

        if(book is null)
        {
            return NotFound(new { Erro = "Não foi possível localizar esse livro." });
        }

        books.Remove(book);

        return Ok(new { Message = "Livro removido com sucesso." });

    }

    private static ResponseBookJson MapToResponse(Book book)
    {
        return new ResponseBookJson
        {
            Id = book.Id,
            Name = book.Title,
            Author = book.Author,
            Gender = book.Gender,
            Price = book.Price,
            Quantity = book.Quantity

        };
    }

    private static Book MapToEntity(RequestBookJson book)
    {
        return new Book()
        {
            Id = books.Count + 1,
            Title = book.Name,
            Author = book.Author,
            Gender = book.Gender,
            Price = book.Price,
            Quantity = book.Quantity

        };
    }

}
