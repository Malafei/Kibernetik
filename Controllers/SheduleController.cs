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




        [HttpPost("addShedule")]
        public async Task<IActionResult> addShedule([FromForm] AddSheduleModel model)
        {
            if (ModelState.IsValid)
            {
                var shed = _context.shedule.SingleOrDefault(x => x.id.ToString() == model.key);
                if (shed == null)
                    return BadRequest(new { invalid = "Такої групи не існує" });

                var lessons = _context.lesson.Where(x => x.shedule == shed && x.date == model.date);
                if (lessons.Count() >= 1)
                    return BadRequest(new { invalid = "Розклад з такою датою вже додано для редагування перейдіть на відповідну сторінку" });

                if (model.lesons.Count() >= 1)
                {
                    foreach (var item in model.lesons)
                    {
                        await _context.lesson.AddAsync(new Lesson{ 

                            shedule = shed,
                            date = model.date,
                            classroom = item.classRoom,
                            teacher = item.nameTeacher,
                            type = item.typeLesson,
                            name_lesson = item.nameLesson,
                            time = item.time,
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                else
                    return BadRequest(new { invalid = "Будь-ласка заповніть всі поля" });

                return Ok();
            }
            else
                return ValidationProblem();
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult EditShedule(int id)
        {
            var shedule = _context.shedule.SingleOrDefault(x => x.id == id);
            return Ok(shedule);
        }



        [HttpPut("saveShedual")]
        public async Task<IActionResult> Save([FromForm] EditSheduleModel model)
        {
            if (ModelState.IsValid)
            {
                Shedule shedule = _context.shedule.SingleOrDefault(x => x.id == model.key);
                if ((model.lesons == null || model.lesons.Count < 1) || (shedule.lessons == null || shedule.lessons.Count < 1))
                    return BadRequest(new { invalid = "Упс схоже щось пішло не так" });
                
                shedule.lessons = model.lesons;

                _context.shedule.Update(shedule);
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
                        await _context.SaveChangesAsync();
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

        [HttpPut("editGroupShedule")]
        public async Task<IActionResult> EditGroupShedule([FromForm] EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                Shedule shedule = _context.shedule.SingleOrDefault(x => x.id == model.key);
                shedule.name_group = model.New_nameGroup;


                _context.shedule.Update(shedule);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();

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
