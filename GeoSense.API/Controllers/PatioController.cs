using AutoMapper;
using GeoSense.API.DTOs;
using GeoSense.API.Helpers;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly GeoSenseContext _context;
        private readonly IMapper _mapper;

        public PatioController(GeoSenseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista paginada de pátios cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorna uma lista de pátios, podendo utilizar paginação via parâmetros <b>page</b> e <b>pageSize</b>.
        /// </remarks>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
        /// <response code="200">Lista paginada de pátios</response>
        [HttpGet]
        [SwaggerResponse(200, "Lista paginada de pátios cadastrados", typeof(PagedHateoasDTO<PatioDTO>))]
        public async Task<ActionResult<PagedHateoasDTO<PatioDTO>>> GetPatios([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Patios.AsQueryable();
            var totalCount = await query.CountAsync();
            var patios = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = _mapper.Map<List<PatioDTO>>(patios);

            var links = HateoasHelper.GetPagedLinks(Url, "Patios", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<PatioDTO>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Links = links
            };

            return Ok(result);
        }

        /// <summary>
        /// Retorna os dados de um pátio por ID.
        /// </summary>
        /// <remarks>
        /// Retorna os detalhes de um pátio específico a partir do seu identificador.
        /// </remarks>
        /// <param name="id">Identificador único do pátio</param>
        /// <response code="200">Pátio encontrado</response>
        /// <response code="404">Pátio não encontrado</response>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Pátio encontrado", typeof(PatioDTO))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<ActionResult<PatioDTO>> GetPatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            var dto = _mapper.Map<PatioDTO>(patio);
            return Ok(dto);
        }

        /// <summary>
        /// Cadastra um novo pátio.
        /// </summary>
        /// <remarks>
        /// Cadastra um novo pátio no sistema. O corpo da requisição deve conter o modelo <see cref="PatioDTO"/>.
        /// </remarks>
        /// <param name="dto">Dados do novo pátio</param>
        /// <response code="201">Pátio criado com sucesso</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        [SwaggerResponse(201, "Pátio criado com sucesso", typeof(PatioDTO))]
        public async Task<ActionResult<PatioDTO>> PostPatio(PatioDTO dto)
        {
            var novoPatio = new Patio();
            _context.Patios.Add(novoPatio);
            await _context.SaveChangesAsync();

            var patioCompleto = await _context.Patios.FindAsync(novoPatio.Id);
            var resultDto = _mapper.Map<PatioDTO>(patioCompleto);

            return CreatedAtAction(nameof(GetPatio), new { id = novoPatio.Id }, resultDto);
        }

        /// <summary>
        /// Atualiza os dados de um pátio existente.
        /// </summary>
        /// <remarks>
        /// Atualiza os dados do pátio informado pelo ID. O corpo da requisição deve conter o modelo <see cref="PatioDTO"/>.
        /// </remarks>
        /// <param name="id">Identificador único do pátio</param>
        /// <param name="dto">Dados do pátio a serem atualizados</param>
        /// <response code="204">Pátio atualizado com sucesso</response>
        /// <response code="404">Pátio não encontrado</response>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(PatioDTO), typeof(GeoSense.API.Examples.PatioDTOExample))]
        [SwaggerResponse(204, "Pátio atualizado com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> PutPatio(long id, PatioDTO dto)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um pátio do sistema.
        /// </summary>
        /// <remarks>
        /// Remove o pátio informado pelo ID.
        /// </remarks>
        /// <param name="id">Identificador único do pátio</param>
        /// <response code="204">Pátio removido</response>
        /// <response code="404">Pátio não encontrado</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Pátio removido com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> DeletePatio(long id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}