﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> CreateBook(BookUpsertDto createBookDto)
        {
            var book = new Book();

            _mapper.Map(createBookDto, book);

            _bookRepository.AddBook(book);

            if (await _bookRepository.SaveAllAsync())

                return Ok(createBookDto);

            return BadRequest("Failed to create a new Book");
        }

        [Authorize(Policy = "ModerateAdminRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookUpsertDto updateBookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            _mapper.Map(updateBookDto, book);

            _bookRepository.UpdateBook(book);

            if (await _bookRepository.SaveAllAsync())
                return NoContent();

            return BadRequest("Failed to update book");
        }

        [Authorize(Policy = "ModerateAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var photos = book.Photos.ToList();

            foreach (var photo in photos)
            {
                if (photo.PublicId != null)
                {
                    var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                    if (result.Error != null) return BadRequest(result.Error.Message);
                }
                book.Photos.Remove(photo);
            }

            _bookRepository.DeleteBook(book);

            if (await _bookRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to delete book");
        }

        [Authorize(Policy = "ModerateAdminRole")]
        [HttpPost("add-photo/{bookId}")]
        public async Task<ActionResult<PhotoBookDto>> AddPhoto(IFormFile file, int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new PhotoBook
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (book.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            book.Photos.Add(photo);

            if (await _bookRepository.SaveAllAsync())
            {
                return _mapper.Map<PhotoBookDto>(photo);
            }

            return BadRequest("Failed to add photo");
        }

    }
}
