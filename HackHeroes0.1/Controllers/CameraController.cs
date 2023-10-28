using AutoMapper;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudent;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudentImage;
using HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackHeroes0._1.Controllers
{
    public class CameraController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _enivortment;
        public CameraController(IWebHostEnvironment enivortment, IMediator mediator, IMapper mapper)       
        {
            _enivortment = enivortment;
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [Route("/Camera/{studentKey}/EditStudentImage")]
        public async Task<IActionResult> EditStudentImage(string studentKey)
        {
            var dto = await _mediator.Send(new GetStudentByStudentKeyQuery() { StudentKey = studentKey });

            var model = _mapper.Map<EditStudentImageCommand>(dto);

            return View(model);
        }


        [Authorize]
        [Route("/Camera/{studentKey}/EditStudentImage")]
        [HttpPost]
        public async Task<IActionResult> EditStudentImage(string name, EditStudentImageCommand command)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //var fileName = file.FileName;

                            //var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //var fileExtension = Path.GetExtension(fileName);

                            //var newFileName = string.Concat(myUniqueFileName, fileExtension);

                            //var filePath = Path.Combine(_enivortment.WebRootPath, "CameraPhotos") + $@"\{newFileName}";

                            //if (!string.IsNullOrEmpty(filePath))
                            //{
                            //    StoreInFolder(file, filePath);
                            //}

                            //var imageBytes = System.IO.File.ReadAllBytes(filePath);

                            //command.Image = imageBytes;

                            //System.IO.File.Delete(filePath);
                            var fileName = file.FileName;

                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            var fileExtension = Path.GetExtension(fileName);

                            var newFileName = string.Concat(myUniqueFileName, fileExtension);

                            // Zamiast zapisywać plik na dysku, wczytujemy go do MemoryStream
                            using var memoryStream = new MemoryStream();
                            await file.CopyToAsync(memoryStream);

                            // Konwersja MemoryStream do tablicy bajtów
                            var imageBytes = memoryStream.ToArray();

                            command.Image = imageBytes;

                        }

                        await _mediator.Send(command);

                        
                    }

                    return RedirectToAction("Index", "HackHeroes");

                }
                else
                {
                   return RedirectToAction("Index", "HackHeroes");

                }
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();

            }
        }
    }
}
