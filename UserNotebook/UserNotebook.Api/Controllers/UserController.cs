using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserNotebook.Domain.Interfaces;
using UserNotebook.Domain.Models.Entities;
using UserNotebook.Domain.Models.Enums;
using UserNotebook.Service.Dtos;

namespace UserNotebook.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly INotebookService _notebookService;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;

        public UserController(INotebookService notebookService, IMapper mapper, IValidator<User> validator)
        {
            _notebookService = notebookService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var task = await _notebookService.GetAllUsersAsync();

            var dto = _mapper.Map<List<UserDto>>(task);

            return Ok(dto);
        }

        [HttpGet("{guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Get([FromRoute] Guid guid)
        {
            var task = await _notebookService.GetUserByIdAsync(guid);
            if (task == null)
            {
                return NotFound("User not found");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Add([FromBody] User user)
        {
            var validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            User task;
            try
            {
                task = await _notebookService.AddUserAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] User user)
        {
            var validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var task = await _notebookService.UpdateUserAsync(user);
            return Ok(task.Id);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateReport()
        {
            var workbook = await _notebookService.GenerateWorkbook();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }

                var fileStreamResult = new FileStreamResult(new FileStream(tempFilePath, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                fileStreamResult.FileDownloadName = fileName;

                Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");
                Response.Headers.Add("File-Name", fileName);

                return fileStreamResult;
            }
        }

        [HttpGet("getGenderEnum")]
        public IActionResult PobierzEnum()
        {
            Gender[] enumValues = (Gender[])Enum.GetValues(typeof(Gender));

            var enumInfoList = new List<object>();
            foreach (var enumValue in enumValues)
            {
                var enumInfo = new
                {
                    Name = enumValue.GetDescription(),
                    Value = (int)enumValue
                };

                enumInfoList.Add(enumInfo);
            }

            string jsonEnum = JsonConvert.SerializeObject(enumInfoList);

            return Ok(jsonEnum);
        }
    }
}
