using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Api.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/Modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public ModulesController(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
        {
            var modules = await uow.ModuleRepository.GetAllModules();
            var dto = mapper.Map<IEnumerable<ModuleDto>>(modules);
            return Ok(dto);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var @module = await uow.ModuleRepository.FindAsync(id);
            var dto = mapper.Map<ModuleDto>(@module);
            if (@module == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            uow.ModuleRepository.Update(module);

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ModuleExists(id))
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

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(Module insertModule)
        {
            var enitityModule = mapper.Map<Module>(insertModule);
            uow.ModuleRepository.Add(enitityModule);
            await uow.CompleteAsync();

            //halvonödig mappning tillbaka? skulle vara om ModuleRepository ändrat mer än Id
            var dto = mapper.Map<ModuleDto>(enitityModule);

            return CreatedAtAction("GetModule", new { id = enitityModule.Id }, dto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await uow.ModuleRepository.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            uow.ModuleRepository.Remove(@module);
            await uow.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> ModuleExists(int id)
        {
            return await uow.ModuleRepository.AnyAsync(id);
        }
    }
}
