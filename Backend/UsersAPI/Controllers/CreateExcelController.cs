using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsersAPI.CreateExcel;
using UsersAPI.Model;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateExcelController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Excel excel = new Excel();

        [HttpPost]
        [Route("CreateExcel")]
        public IActionResult Excel([FromBody] List<User> userValue)
        {
            try
            {
                return Ok(excel.Create(userValue));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex);
            }
        }
    }
}
