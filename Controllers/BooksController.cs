using BoocStore.Core.Models;
using BookStore.Api.Contract;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase

    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService) 
        {
            _booksService = booksService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookResponce>>>GetBooks()
        {
            var books = await _booksService.GetAllBooks();

            var responce = books.Select(b => new BookResponce(b.Id, b.Title, b.Description, b.Price));
            return Ok(responce);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BookRequest request)
        {
            var (book, error) = Book.Create(
               Guid.NewGuid(),
               request.Title,
               request.description,
               request.Price);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
             var bookId = await _booksService.CreateBook(book);
            return Ok(bookId);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BookRequest request)
        {
            var bookId = await _booksService.UpdateBook(id, request.Title, request.description, request.Price);
            return Ok(bookId);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }

    }
}
