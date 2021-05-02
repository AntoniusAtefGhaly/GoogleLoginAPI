using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GoogleLoginAPI.Models;
namespace GoogleLoginAPI.Controllers
{
    public class usersController : ApiController
    {
        db_a7394e_googleloginsqlEntities1 context = new db_a7394e_googleloginsqlEntities1();

        public List<user> GetAll()
        {
            List<user> users = context.users.ToList();
            return users;
        }

        
        public IHttpActionResult Getone(int id)
        {
            user user = context.users.FirstOrDefault(u => u.id == id);
            if (user != null)
            {
                return Ok<user>(user);
            }
            else
            {
                return BadRequest("not found");
            }
        }

        public IHttpActionResult PostUser(user U)
        {
            try
            {
                user user = context.users.FirstOrDefault(u => u.email == U.email);
                if (user == null)
                {
                    context.users.Add(U);
                    context.SaveChanges();
                    return Ok("signup");
                }
                else
                {
                    return Ok("login");
                }

            }
            catch
            {
                return BadRequest("not added");
            }
        }

        public IHttpActionResult PutUser([FromUri] int id, [FromBody] user user)
        {
            try
            {
                user useredit = context.users.FirstOrDefault(emp => emp.id == id);
                useredit.name = user.name;
                useredit.id = user.id;
                useredit.idToken = user.idToken;
                useredit.photoUrl = user.photoUrl;
                useredit.provider = user.provider;
                useredit.user_id = user.user_id;
                useredit.authToken = user.authToken;
                useredit.email = user.email;

                context.SaveChanges();
                return Ok("edit");
            }
            catch
            {
                return BadRequest("not edite");
            }
        }


        public IHttpActionResult DeleteUser([FromUri] int id)
        {
            try
            {
                user user = context.users.FirstOrDefault(u => u.id == id);
                context.users.Remove(user);
                context.SaveChanges();
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest("not Deleted");
            }
        }
    }
}