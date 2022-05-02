using Kibernetik.Data;
using Kibernetik.Data.DataShedule;
using Kibernetik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly AppEFContext _context;

        public SheduleController(AppEFContext context)
        {
            _context = context;
        }




        [HttpPost("addShedules")]
        public async Task<IActionResult> addShedule([FromForm] AddGroupModel model)
        {
            if (ModelState.IsValid)
            {
                Shedule shedule = new Shedule
                {
                    
                };

                

                await _context.shedule.AddAsync(shedule);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();
        }




        [HttpPost("addGroupShedules")]
        public async Task<IActionResult> addGroupShedules([FromForm] AddGroupModel model)
        {
            if (ModelState.IsValid)
            {
                Shedule shedule = new Shedule
                {
                    name_group = model.nameGroup
                };

                await _context.shedule.AddAsync(shedule);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGroupShedule(int id)
        {
            try
            {
                var shedule = _context.shedule.SingleOrDefault(x => x.id == id);
                if (shedule == null)
                    return StatusCode(404);

                if (shedule.lessons.Count > 0)
                {
                    foreach (var lesson in shedule.lessons)
                    {
                        _context.lesson.Remove(lesson);
                    }
                }


                _context.shedule.Remove(shedule);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [HttpGet("showGroup")]
        public IActionResult ShowGroup()
        {
            List<ShowGroupModel> groups = new List<ShowGroupModel>();
            foreach (var item in _context.shedule)
            {
                groups.Add(new ShowGroupModel
                {
                    label = item.name_group,
                    key = item.id,
                });
            }
            return Ok(groups);
        }
    }
}
