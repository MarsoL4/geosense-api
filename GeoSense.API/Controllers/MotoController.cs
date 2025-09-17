using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.DTOs;
using GeoSense.API.Helpers;

namespace GeoSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly GeoSenseContext _context;

        public MotoController(GeoSenseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista paginada de motos cadastradas.
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
        /// <returns>Página de motos</returns>
        [HttpGet]
        public async Task<ActionResult<PagedHateoasDTO<Moto>>> GetMotos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Motos.Include(m => m.Vaga).AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var links = HateoasHelper.GetPagedLinks(Url, "Motos", page, pageSize, totalCount);

            var result = new PagedHateoasDTO<Moto>
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
        /// Retorna os dados de uma moto por ID.
        /// </summary>
        /// <param name="id">ID da moto</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetMoto(long id)
        {
            var moto = await _context.Motos
                .Include(m => m.Vaga)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto == null)
            {
                return NotFound();
            }

            return moto;
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="dto">Dados para atualização</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(long id, MotoDTO dto)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            moto.Modelo = dto.Modelo;
            moto.Placa = dto.Placa;
            moto.Chassi = dto.Chassi;
            moto.ProblemaIdentificado = dto.ProblemaIdentificado;
            moto.VagaId = dto.VagaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Cadastra uma nova moto.
        /// </summary>
        /// <param name="dto">Dados da nova moto</param>
        [HttpPost]
        public async Task<ActionResult<Moto>> PostMoto(MotoDTO dto)
        {
            var novaMoto = new Moto
            {
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                Chassi = dto.Chassi,
                ProblemaIdentificado = dto.ProblemaIdentificado,
                VagaId = dto.VagaId
            };

            _context.Motos.Add(novaMoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMoto), new { id = novaMoto.Id }, novaMoto);
        }

        /// <summary>
        /// Exclui uma moto do sistema.
        /// </summary>
        /// <param name="id">ID da moto</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(long id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoExists(long id)
        {
            return _context.Motos.Any(e => e.Id == id);
        }
    }
}