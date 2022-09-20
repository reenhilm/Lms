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
using Lms.Common.Dto;
using Lms.Data.Data.Repositories;

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
        public async Task<ActionResult<ModuleDto>> PutModule(int id, ModuleDto moduleDto)
        {
            if (id != moduleDto.Id)
                return BadRequest();

            var moduleEntity = await uow.ModuleRepository.FindAsync(moduleDto.Id);
            var updateEntity = mapper.Map<Module>(moduleDto);
            updateEntity.CourseId = moduleEntity.CourseId;

            uow.ModuleRepository.Update(updateEntity);
            ModuleDto dto;
            try
            {
                await uow.CompleteAsync();
                dto = mapper.Map<ModuleDto>(updateEntity);

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

            return Ok(dto);
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleInsertDto>> PostModule(ModuleInsertDto insertModule)
        {
            if (await uow.CourseRepository.FindAsync(insertModule.CourseId) is null)
                return NotFound();

            var enitityModule = mapper.Map<Module>(insertModule);
            uow.ModuleRepository.Add(enitityModule);
            await uow.CompleteAsync();

            //halvonödig mappning tillbaka? kan bara skicka tillbaka insertModule, men skulle kunna vara om ModuleRepository ändrar mer än Id i framtiden
            var dto = mapper.Map<ModuleInsertDto>(enitityModule);

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
