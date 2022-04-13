using AutoMapper;
using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Dto;
using Fnyo.Learn.Jenkins.Entity;
using Fnyo.Learn.Jenkins.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TmsDbContext _dbContext;
        public BookController(IMapper mapper,
            TmsDbContext dbContext)
        {
            _mapper = mapper;   
            _dbContext = dbContext; 
        }



        [HttpPost("pagination")]
        public async Task<ApiResult> GetPagination(QueryBookPagedDto queryBookPagedDto)
        {


            var total = await _dbContext.Books.CountAsync(book => book.BookName.Contains(queryBookPagedDto.Obscure)
                || book.Author.Contains(queryBookPagedDto.Obscure)
                || book.Date.ToString().Contains(queryBookPagedDto.Obscure)
                || book.Press.Contains(queryBookPagedDto.Obscure)
                || book.Types.Contains(queryBookPagedDto.Obscure));
            var books = await _dbContext.Books.Where(book => book.BookName.Contains(queryBookPagedDto.Obscure)
                || book.Author.Contains(queryBookPagedDto.Obscure)
                || book.Date.ToString().Contains(queryBookPagedDto.Obscure)
                || book.Press.Contains(queryBookPagedDto.Obscure)
                || book.Types.Contains(queryBookPagedDto.Obscure))
                .Skip(((queryBookPagedDto.PageIndex - 1) * queryBookPagedDto.PageSize)).Take(queryBookPagedDto.PageSize).ToListAsync();


            var result = new ApiResult();
            result.Total = total;   
            result.Ok(_mapper.Map<List<BookDto>>(books));
            return result;

        }

        [HttpGet("obscure")]
        public async Task<IEnumerable<BookDto>> Obscure(string obscure)
        {
            var books = await _dbContext.Books.Where(book=>book.BookName
            .Contains(obscure) ||
            book.Author.Contains(obscure)||
            book.Date.ToString().Contains(obscure)||
            book.Press.Contains(obscure)||
            book.Types.Contains(obscure)).ToListAsync();

            return _mapper.Map<List<BookDto>>(books);

        }

        [HttpGet("list")]
        public async Task<IEnumerable<BookDto>>  Get()
        {
            var books =await _dbContext.Books.AsQueryable().ToListAsync();
            return _mapper.Map<List<BookDto>>(books);
        }


        [HttpPost("add")]
        public async Task<ApiResult> Add(BookDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            await _dbContext.Books.AddAsync(bookEntity);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Message = "添加成功",
                Success = true
            };
        }


        [HttpPut("update")]
        public async Task<ApiResult> Update(BookDto book)
        {
            var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id.Equals(book.Id));
            if(bookEntity == null)
            {
                return new ApiResult()
                {
                    Success = false,
                    Message = "更新失败"
                };
            }
            _mapper.Map<BookDto, Book>(book, bookEntity);
            _dbContext.Update(bookEntity);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = "更新成功"
            };
        }

        [HttpDelete("delete/{id}")]
        public async Task<ApiResult> Delete(int id)
        {
            var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (bookEntity == null)
            {
                return new ApiResult()
                {
                    Success = false,
                    Message = "删除失败"
                };
            }
            _dbContext.Books.Remove(bookEntity);
            await _dbContext.SaveChangesAsync();
            return new ApiResult()
            {
                Success = true,
                Message = "删除成功"
            };
        }
    }
}
