using Kibernetik.Data;
using Kibernetik.Data.DataNews;
using Kibernetik.Data.DataUser;
using Kibernetik.Helper;
using Kibernetik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly AppEFContext _context;

        public NewsController(AppEFContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult all()
        {
            List<ItemNewsModel> news = new List<ItemNewsModel>();
            foreach (var item in _context.news)
            {
                    DateTime dateOnly = item.CreateDate.Date;
                    news.Add(new ItemNewsModel
                    {
                        id = item.id,
                        name = item.name,
                        description = item.description,
                        image = item.image,
                        date = dateOnly.ToString("dd.MM.yyyy")
                    });
            }
            news.Reverse();
            return Ok(news);
        }


        [HttpPost("addNews")]
        public async Task<IActionResult> AddNews([FromForm] AddNewsModel model)
        {
            if (ModelState.IsValid)
            {
                News news = new News
                {
                    name = model.name,
                    description = model.description,
                    CreateDate = DateTime.Now
                };

                if (model.image == null)
                {
                    news.image = "LogoKibernetic.png";
                }
                else
                    news.image = PhotoHelper.AddPhoto(model.image);

                await _context.news.AddAsync(news);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                var News = _context.news.SingleOrDefault(x => x.id == id);
                if (News == null)
                    return StatusCode(404);

                if (News.image != "LogoKibernetic.png")
                {
                    PhotoHelper.DeletePhoto(News.image);
                }

                _context.news.Remove(News);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var news = _context.news.SingleOrDefault(x => x.id == id);
            return Ok(news);
        }


        [HttpPut("save")]
        public async Task<IActionResult> Save([FromForm] NewsSaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                News news = _context.news.SingleOrDefault(x => x.id == model.id);
                news.name = model.name;
                news.description = model.description;
                if (model.image == null)
                {
                    news.image = "LogoKibernetic.png";
                }
                else
                {
                    if (news.image != "LogoKibernetic.png")
                    {
                        PhotoHelper.DeletePhoto(news.image);
                    }
                    news.image = PhotoHelper.AddPhoto(model.image);
                }



                _context.news.Update(news);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();

        }

    }
}
