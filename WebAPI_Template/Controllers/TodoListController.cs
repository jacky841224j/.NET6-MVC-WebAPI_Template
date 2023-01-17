using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebAPI_Template.Dtos;
using WebAPI_Template.Models;
using WebAPI_Template.Parameter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoListContext _TodoListContext;
        public TodoListController(TodoListContext todoListContext)
        {
            _TodoListContext = todoListContext;
        }

        //使用IActionResult、LINQ、父子表關聯
        // GET: api/<TodoListController>
        [HttpGet]
        public IActionResult Get([FromQuery] TodoSelectParameter value)
        {
            var result = _TodoListContext.TodoLists
                            .Include(a => a.UploadFiles)
                            .Select(a => new TodoListDto
                            {
                                Id = a.Id,
                                Event = a.Event,
                                Enable = a.Enable,
                                InsertEmployeeId = a.InsertEmployeeId,
                                UpdateEmployeeId = a.UpdateEmployeeId,
                                UploadFiles = _TodoListContext.UploadFiles
                                                .Where(b => b.ListId == a.Id)
                                                .Select(b => new UploadFileDto
                                                {
                                                    Id = b.Id,
                                                    ListId = b.ListId,
                                                    FileName = b.FileName,
                                                    Src = b.Src,
                                                    EmpId = b.EmpId
                                                }).ToList()
                            });

            if (value.Id != null)
            {
                result = result.Where(a => a.Id == value.Id);
            }

            if (value.Event != null)
            {
                result = result.Where(a => a.Event == value.Event);
            }

            if (value.Enable != null)
            {
                result = result.Where(a => a.Enable == value.Enable);
            }

            if (value.minOrder != null && value.maxOrder != null)
            {
                result = result.Where(a => a.Id >= value.minOrder && a.Id <= value.maxOrder);
            }

            if (!result.Any()) return NotFound("找不到資料");

            return Ok(result);
        }

        // GET api/<TodoListController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _TodoListContext.TodoLists
                              .Where(x => x.Id == id)
                              .Select(a => new TodoListDto
                              {
                                  Id = a.Id,
                                  Event = a.Event,
                                  Enable = a.Enable,
                                  InsertEmployeeId = a.InsertEmployeeId,
                                  UpdateEmployeeId = a.UpdateEmployeeId,
                                  UploadFiles = _TodoListContext.UploadFiles
                                                  .Where(b => b.ListId == a.Id)
                                                  .Select(b => new UploadFileDto
                                                  {
                                                      Id = b.Id,
                                                      ListId = b.ListId,
                                                      FileName = b.FileName,
                                                      Src = b.Src,
                                                      EmpId = _TodoListContext.Employees.Where(c => c.EmpId == b.EmpId)
                                                                                        .Select(c => c.Name).SingleOrDefault()
                                                  }).ToList()
                              }).SingleOrDefault();

            if (result == null) return NotFound("找不到資料");

            return Ok(result);
        }

        // POST api/<TodoListController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoList value)
        {
            TodoList insert = new TodoList
            {
                Id = value.Id,
                Enable = value.Enable,
                Event = value.Event,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                InsertEmployeeId = "TW001",
                UpdateEmployeeId = "TW001",
            };
            _TodoListContext.TodoLists.Add(insert);
            _TodoListContext.SaveChanges();
            return Ok("新增TodoList成功");
        }

        // PUT api/<TodoListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<TodoListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
