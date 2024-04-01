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
        if (request.Name is null)
        {
            return BadRequest(new { Erro = "É necessário que informe um nome para o livro."});
        }

        if (request.Quantity == 0)
        {
            return BadRequest(new { Erro = "É necessário que informe a quantidade de livros" });
        }

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
            Message = "Você não possui livros cadastrados."
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

    [HttpPatch("{id:int}")]
    public IActionResult UpdateBook(int id, RequestUpdateBookJson request)
    {

        var currentBook = books.FirstOrDefault(book => book.Id == id);
        currentBook.Title = request.Name ?? currentBook.Title;
        currentBook.Author = request.Author ?? currentBook.Author;
        currentBook.Gender = request.Gender ?? currentBook.Gender;
        currentBook.Price = request.Price != 0 ? request.Price : currentBook.Price;
        currentBook.Quantity = request.Quantity != 0 ? request.Quantity : currentBook.Quantity;

        return Ok(MapToResponse(currentBook));
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
