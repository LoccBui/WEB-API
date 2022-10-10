using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        //Get
        [Route("user")]
        [HttpGet]
        public IHttpActionResult GetAllUser()
        {
            IList<UsersViewModel> us = null;

            using (var ctx = new QLBanHangDBEntities())
            {
                us = ctx.Users.Include("Users")
                            .Select(s => new UsersViewModel()
                            {
                                username = s.username,
                                password = s.passwords,
                            }).ToList<UsersViewModel>();
            }
            if (us == null)
            {
                return NotFound();
            }

            return Ok(us);
           
        }


        //Get By Id
        [Route("user/{name}/{password}")]
        [HttpGet]
        public IHttpActionResult GetByUsername(string name, string password)
        {
            IList<UsersViewModel> us = null;
            using (var ctx = new QLBanHangDBEntities())
            {
                us = ctx.Users.Include("Users")
                    .Where(s => s.username.ToLower() == name.ToLower() && s.passwords.ToLower() == password.ToLower())
                    .Select(s => new UsersViewModel()
                    {
                        username = s.username,
                        password = s.passwords,
                    }).ToList<UsersViewModel>();
            }

            if (us == null)
            {
                return NotFound();
            }

            return Ok(us);
        }


        [Route("user")]
        [HttpPost]
        public IHttpActionResult PostNewUser(UsersViewModel us)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new QLBanHangDBEntities())
            {
                ctx.Users.Add(new User()
                {
                    username = us.username,
                    passwords = us.password,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
