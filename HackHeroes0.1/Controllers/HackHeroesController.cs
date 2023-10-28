using AutoMapper;
using HackHeroes.Application.HackHeroes.Commands.Classes.CreateClass;
using HackHeroes.Application.HackHeroes.Commands.Classes.DeleteClass;
using HackHeroes.Application.HackHeroes.Commands.Classes.EditClass;
using HackHeroes.Application.HackHeroes.Queries.GetAllClasses;
using HackHeroes.Application.HackHeroes.Queries.GetClassByEncodedName;
using HackHeroes.Application.HackHeroes.Queries.Student;
using HackHeroes.Application.Mappings;
using HackHeroes.Domain.Entities;
using HackHeroes0._1.Models;
using HackHeroes.Application.HackHeroes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HackHeroes.Application.HackHeroes.Commands.Students.AddStudent;
using HackHeroes.Application.HackHeroes.Commands.Students;
using HackHeroes.Application.HackHeroes.Commands.Students.DeleteStudent;
using HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudent;
using HackHeroes0._1.Extensions;

namespace HackHeroes0._1.Controllers
{
    public class HackHeroesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HackHeroesController(IMediator mediator,IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ClassIndex()
        {
            var classes = await _mediator.Send(new GetAllClassesQuery());
            return View(classes);
        }


        [Authorize]
        [Route("HackHeroes/{encodedName}/StudentsIndex")]
        public async Task<IActionResult> StudentsIndex(string encodedName)
        {
            var students = await _mediator.Send(new GetAllStudentsQuery(encodedName));

            return View(students);
        }


        [Authorize]
        public ActionResult Create()
        {           
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateClassCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Created class: {command.Name}");

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [Route("HackHeroes/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName)
        {

            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));

            //if (!dto.IsEditable)
            //{
            //    return RedirectToAction("NoAccess", "Home");
            //}

            DeleteClassCommand model = _mapper.Map<DeleteClassCommand>(dto);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("HackHeroes/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName, DeleteClassCommand command)
        {
            

            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);            

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [Route("HackHeroes/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));

            //if (!dto.IsEditable)
            //{
            //    return RedirectToAction("NoAccess", "Home");
            //}

            return View(dto);
        }

        [Authorize]
        [Route("HackHeroes/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));          

            //if(!dto.IsEditable)
            //{
            //    return RedirectToAction("NoAccess","Home");
            //}

            EditClassCommand model = _mapper.Map<EditClassCommand>(dto);            //TO DO ACCESSY


            return View(model);
        }


        [Authorize]
        [HttpPost]
        [Route("HackHeroes/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditClassCommand command)
        {
            
            //if (!ModelState.IsValid)
            //{
            //    return View(command);

            //}

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index)); //TODO: Refactor
        }

        [Authorize]
        [Route("HackHeroes/{studentKey}/EditStudent")]
        public async Task<IActionResult> EditStudent(string studentKey)
        {
            var dto = await _mediator.Send(new GetStudentByStudentKeyQuery() {StudentKey = studentKey});

            var model = _mapper.Map<EditStudentCommand>(dto);

            return View(model);
        }
     

        [Authorize]
        [HttpPost]
        [Route("HackHeroes/{studentKey}/EditStudent")]
        public async Task<IActionResult> EditStudent(string studentKey, EditStudentCommand command)
        {

            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index)); 
        }


        [Authorize]
        [HttpPost]
        [Route("HackHeroes/Student")]
        public async Task<IActionResult> AddStudent(AddStudentCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            await _mediator.Send(command);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("HackHeroes/{studentKey}/Student")]
        public async Task<IActionResult> DeleteStudent(string studentKey,DeleteStudentCommand command)
        {
            command.StudentKey = studentKey;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            await _mediator.Send(command);

            return Ok();
        }


        [Authorize]
        [HttpGet]
        [Route("HackHeroes/{encodedName}/Student")]
        public async Task<IActionResult> GetAllStudents(string encodedName)
        {
             var data = await _mediator.Send(new GetAllStudentsQuery(encodedName));
            return Ok(data);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
