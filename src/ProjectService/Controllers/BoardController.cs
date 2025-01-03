using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.DTOs;
using ProjectService.Entities;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/boards")]
    public class BoardController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;

        public BoardController(ProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BoardDto>>> GetAllBoards()
        {
            var boards = await _context.Boards
                .OrderBy(s => s.BoardName)
                .ToListAsync();
            return _mapper.Map<List<BoardDto>>(boards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDto>> GetBoardById(Guid id)
        {
            var board = await _context.Boards
                .FirstOrDefaultAsync(s => s.Id == id);
            if (board == null)
            {
                return NotFound();
            }
            var boardDto = _mapper.Map<BoardDto>(board);
            return Ok(boardDto);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardDto createBoardDto)
        {
            var board = _mapper.Map<Board>(createBoardDto);
            _context.Boards.Add(board);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not create board");
            }
            return CreatedAtAction(nameof(GetBoardById), new { id = board.Id }, _mapper.Map<BoardDto>(board));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BoardDto>> UpdateBoard(Guid id, UpdateBoardDto updateBoardDto)
        {
            var board = await _context.Boards
                .FirstOrDefaultAsync(s => s.Id == id);
            if (board == null)
            {
                return NotFound();
            }
            board.BoardName = updateBoardDto.BoardName ?? board.BoardName;
            board.BoardType = Enum.Parse<BoardType>(updateBoardDto.BoardType ?? board.BoardType.ToString());
            board.ProjectId = updateBoardDto.ProjectId ?? board.ProjectId;


            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not update board");
            }
            return Ok(_mapper.Map<BoardDto>(board));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoard(Guid id)
        {
            var board = await _context.Boards
                .FirstOrDefaultAsync(s => s.Id == id);
            if (board == null)
            {
                return NotFound();
            }
            _context.Boards.Remove(board);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return BadRequest("Could not delete board");
            }
            return NoContent();
        }   
    }
}