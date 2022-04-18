using Kibernetik.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public IActionResult all()
        {
            //List<ItemNewsModel> news = new List<ItemNewsModel>();
            //foreach (var item in _context.news)
            //{
            //    DateTime dateOnly = item.CreateDate.Date;
            //    news.Add(new ItemNewsModel
            //    {
            //        id = item.id,
            //        name = item.name,
            //        description = item.description,
            //        image = item.image,
            //        date = dateOnly.ToString("dd.MM.yyyy")
            //    });
            //}
            //news.Reverse();
            //return Ok(news);
            return Ok();
        }
    }
}
