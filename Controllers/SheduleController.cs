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
        private Shedule shed;

        public SheduleController(AppEFContext context)
        {
            _context = context;
        }




        [HttpPost("addShedule")]
        public IActionResult addShedule([FromForm] AddSheduleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.shedule.Count() > 0)
                    {
                        var shedules = _context.shedule.SingleOrDefault(x => x.name_group.id == model.nameGroup && x.date == model.date);
                        return BadRequest(new { invalid = "Розклад з такою датою вже додано для цієї групи якщо бажаєте редагувати перейдіть на відповідну сторінку" });
                    }
                    else
                        return Ok();
                }
                else
                    return BadRequest(new { invalid = "Ви ввели не коректні дані" });
            }
            catch (InvalidOperationException)
            {
                Group group = _context.group.SingleOrDefault(x => x.id == model.nameGroup);
                shed = new Shedule
                {
                    date = model.date,
                    name_group = group
                };
                return Ok();
            }


            //var shed = _context.shedule.SingleOrDefault(x => x.id == model.nameGroup);

            //if (group == null)
            //return BadRequest(new { invalid = "Такої групи не існує" });


            //var lessons = _context.lesson.Where(x => x.shedule == shed && x.date == model.date);
            //if (lessons.Count() >= 1)
            //return BadRequest(new { invalid = "Розклад з такою датою вже додано для цієї групи якщо бажаєте редагувати перейдіть на відповідну сторінку" });



            //return Ok();

            //    List<ItemLessonsModel> less = (List<ItemLessonsModel>)model.lessons;
            //if (less.Count() >= 1)
            //{
            //    foreach (var item in less)
            //    {
            //        await _context.lesson.AddAsync(new Lesson
            //        {

            //            shedule = shed,
            //            date = model.date,
            //            classroom = item.classRoom,
            //            teacher = item.nameTeacher,
            //            type = item.typeLesson,
            //            name_lesson = item.nameLesson,
            //            time = item.time,
            //        });
            //        await _context.SaveChangesAsync();
            //    }
            //}
            //    else
            //        return BadRequest(new { invalid = "Будь-ласка заповніть всі поля" });

        }

        [HttpPost("addSheduleLessons")]
        public async Task<IActionResult> addSheduleLessons([FromForm] AddSheduleLessonsModel model)
        {
            if (ModelState.IsValid)
            {
                List<ItemLessonsModel> leson = (List<ItemLessonsModel>)model.lessons;

                if (leson != null)
                {
                    if (leson.Count() >= 1)
                    {
                        shed.lessons = new List<Lesson>();
                        foreach (var item in leson)
                        {
                            //if (item.time == )
                            Lesson tmp = new Lesson
                            {
                                classroom = item.classRoom,
                                teacher = item.nameTeacher,
                                type = item.typeLesson,
                                name_lesson = item.nameLesson,
                                time = DateTime.Parse(item.time),
                                shedule = shed
                            };
                            await _context.lesson.AddAsync(tmp);
                            shed.lessons.Add(tmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }




            /////////
            List<ItemLessonsModel> less = (List<ItemLessonsModel>)model.lessons;


            if (less != null)
            {
                if (less.Count() >= 1)
                {
                    foreach (var item in less)
                    {
                        await _context.lesson.AddAsync(new Lesson
                        {
                            classroom = item.classRoom,
                            teacher = item.nameTeacher,
                            type = item.typeLesson,
                            name_lesson = item.nameLesson,
                            //time = item.time,
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                else
                    return BadRequest(new { invalid = "Будь-ласка заповніть всі поля" });
            }


            return Ok();
        }



        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult EditShedule(int id)
        {
            ///////// e pitanya
            var shedule = _context.shedule.SingleOrDefault(x => x.id == id);
            return Ok(shedule);
        }



        [HttpPut("saveShedual")]
        public async Task<IActionResult> Save([FromForm] EditSheduleModel model)
        {
            if (ModelState.IsValid)
            {
                ///////// e pitanya

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
                Group group = new Group
                {
                    name_group = model.nameGroup
                };
                await _context.group.AddAsync(group); 
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
                Group group = _context.group.SingleOrDefault(x => x.id == id);
                //Shedule shedule = _context.shedule.SingleOrDefault(x => x.id == id);
                //if (shedule == null)
                //return StatusCode(404);
                if (group == null)
                    return BadRequest(new { invalid = "Ми не знайшли такої групи" });

                //if (shedule.lessons != null && shedule.lessons.Count > 0)
                //{
                //    foreach (var lesson in shedule.lessons)
                //    {
                //        _context.lesson.Remove(lesson);
                //        await _context.SaveChangesAsync();
                //    }
                //}\



                /////////////////////////////////////////////////pitanya


                //_context.shedule.Remove(shedule);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [HttpPost("editGroupShedule")]
        public async Task<IActionResult> EditGroupShedule([FromForm] EditGroupModel model)
        {
            if (ModelState.IsValid)
            {
                Group group = _context.group.SingleOrDefault(x => x.id == model.nameGroup);
                group.name_group = model.newnameGroup;

                _context.group.Update(group);
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
            foreach (var item in _context.group)
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
