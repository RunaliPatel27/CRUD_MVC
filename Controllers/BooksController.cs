using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Sample_CRUD.Data;
using Sample_CRUD.Models;


namespace Sample_CRUD.Controllers
{
    public class BooksController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public BooksController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookslist = await mvcDemoDbContext.Books.ToListAsync();
            return View(bookslist);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //creating functionality to add a new employee
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookRequest)
        {
            var book = new BookModel()
            {
                Id = Guid.NewGuid(),
                 BookTitle = addBookRequest.BookTitle,
                BookAuthor = addBookRequest.BookAuthor,
                BookPages = addBookRequest.BookPages,
            };
           await mvcDemoDbContext.Books.AddAsync(book);
           await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
             var book = await mvcDemoDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(book !=null)
            {
                var viewModel = new UpdateBooksModel()
                {
                    Id = book.Id,
                    BookTitle = book.BookTitle,
                    BookAuthor = book.BookAuthor,
                    BookPages = book.BookPages,
                };
                return await Task.Run(() => View("View" , viewModel));

            }
           

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateBooksModel model)
        {
            var book = await mvcDemoDbContext.Books.FindAsync(model.Id);
            if(book != null)
            {
                book.BookTitle = model.BookTitle;
                book.BookAuthor = model.BookAuthor;
                book.BookPages = model.BookPages;

               await mvcDemoDbContext.SaveChangesAsync();

                 return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateBooksModel model)
        {

            var book = await mvcDemoDbContext.Books.FindAsync(model.Id);

            if(book != null)
            {
                mvcDemoDbContext.Books.Remove(book);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
