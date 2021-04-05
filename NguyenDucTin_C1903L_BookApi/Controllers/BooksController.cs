using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NguyenDucTin_C1903L_BookApi.DTO;
using NguyenDucTin_C1903L_BookApi.Entities;
using NguyenDucTin_C1903L_BookApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDucTin_C1903L_BookApi.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public BooksController(IBookRepository bookRepository, IMapper mapper, IPhotoService photoService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();

            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [AllowAnonymous]
        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBookByCategory(string category)
        {
            var books = await _bookRepository.GetBooksByCategoryAsync(category);
            if (books == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [Authorize(Policy = "ModerateAdminRole")]
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookUpsertDto createBookDto)
        {
            var book = new Book();

            _mapper.Map(createBookDto, book);

            _bookRepository.AddBook(book);

            if (await _bookRepository.SaveAllAsync())

                return Ok(createBookDto);

            return BadRequest("Failed to create a new Book");
        }

    }
}
