using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NLog;
using System;
using UsersAPI.Model;
using UsersAPI.Provider;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static DbProvider dbProvider;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public UserController(IConfiguration configuration)
        {
            dbProvider = new DbProvider(configuration);
        }

        [HttpGet]
        [Route("UserGet")]
        public IActionResult Get()
        {
            try
            {
                return Ok(dbProvider.SelectUsers());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("UserPost")]
        public IActionResult Post([FromBody] User userValue)
        {
            try
            {
                dbProvider.Insert(userValue);

                return Ok(Json("Данные были успешно добавлены"));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UserPut")]
        public IActionResult Put([FromBody] User userValue)
        {
            try
            {
                dbProvider.Update(userValue);

                return Ok(Json("Данные были успешно изменены"));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("UserDelete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                dbProvider.Delete(id);
                return Ok(Json("Данные были успешно удалены"));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("UserDeleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                dbProvider.DeleteAll();
                return Ok(Json("Данные были успешно удалены"));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }
    }
}
