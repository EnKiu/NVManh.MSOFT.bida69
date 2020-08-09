using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSOFT.DL;
using MSOFT.Entities.Models;

namespace MSOFT.bida69.core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefsController : ControllerBase
    {
        private readonly bida69Context _context;

        public RefsController(bida69Context context)
        {
            _context = context;
        }

        // GET: api/Refs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ref>>> GetRef()
        {
            return await _context.Ref.ToListAsync();
        }

        // GET: api/Refs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ref>> GetRef(Guid id)
        {
            var @ref = await _context.Ref.FindAsync(id);

            if (@ref == null)
            {
                return NotFound();
            }

            return @ref;
        }

        // PUT: api/Refs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRef(Guid id, Ref @ref)
        {
            if (id != @ref.RefId)
            {
                return BadRequest();
            }

            _context.Entry(@ref).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Refs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Ref>> PostRef(Ref @ref)
        {
            _context.Ref.Add(@ref);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRef", new { id = @ref.RefId }, @ref);
        }

        // DELETE: api/Refs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ref>> DeleteRef(Guid id)
        {
            var @ref = await _context.Ref.FindAsync(id);
            if (@ref == null)
            {
                return NotFound();
            }

            _context.Ref.Remove(@ref);
            await _context.SaveChangesAsync();

            return @ref;
        }

        private bool RefExists(Guid id)
        {
            return _context.Ref.Any(e => e.RefId == id);
        }
    }
}
