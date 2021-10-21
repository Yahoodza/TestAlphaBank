using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using UsersAPI.Model;

namespace UsersAPI.CreateExcel
{
    public class Excel : Controller
    {
        public FileResult Create(List<User> userList)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Выгрузка данных");
                var currentRow = 1;
                worksheet.Cell("A" + 1).Value = "Идентификатор";
                worksheet.Cell("B" + 1).Value = "ФИО";
                worksheet.Cell("C" + 1).Value = "Логин";
                worksheet.Cell("D" + 1).Value = "Дата добавления";
                foreach (var user in userList)
                {
                    currentRow++;
                    worksheet.Cell("A" + currentRow).Value = user.Id;
                    worksheet.Cell("B" + currentRow).Value = user.FIO;
                    worksheet.Cell("C" + currentRow).Value = user.UserLogin;
                    worksheet.Cell("D" + currentRow).Value = user.DateAddUser;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Пользователи.xlsx");
                }
            }
        }
    }
}
