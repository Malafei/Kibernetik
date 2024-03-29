﻿using Kibernetik.Data;
using Kibernetik.Data.DataUser;
using Kibernetik.Helper;
using Kibernetik.Models;
using Kibernetik.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AccountController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IJwtTocenService _tokenService;

        public AccountController(AppEFContext context, IJwtTocenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                 
                User user = await _context.users.FirstOrDefaultAsync(u => u.email == model.email); // шукаєм чи є такій логін в базі
                if (user != null) // якщо юзер не дорівнює налл це означає що в базі є такий користувач і наш емаіл не буде універсальний
                {
                    ModelState.AddModelError("email", "Користувач з таким емейлом вже зареєстрований");
                    return ValidationProblem();
                }
                string[] nameUser = model.email.Split(new char[] { '@' });
                user = new User
                {
                    email = model.email,
                    login = nameUser[0],
                };
                user.password = EncryptionHelper.HashedPassword(model.password, model.email);

                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
                return ValidationProblem();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.users.FirstOrDefaultAsync(x => x.email == model.email);
                if (user != null)
                {
                    if (user.password == EncryptionHelper.HashedPassword(model.password, user.email))
                    {
                        string token = _tokenService.CreateToken(user);
                        return Ok(
                            new { token }
                        );
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Невірний пароль");
                        return ValidationProblem(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "Користувача з такою електроною адресою не знайдено");
                    return ValidationProblem(ModelState);
                }
            }
            else
                return ValidationProblem(ModelState);
        }

    }
}
