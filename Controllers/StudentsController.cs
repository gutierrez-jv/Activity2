using Activity2.Models.Database.Models;
using Activity2.Services;
using Activity2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Activity2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var students = new List<StudentViewModel>();

            foreach (var s in _studentService.GetAllStudents())
            {
                students.Add(new StudentViewModel
                {
                    Id = s.Id,
                    StudentNumber = s.StudentNumber,
                    FullName = s.FirstName + " " + s.LastName,
                    Email = s.Email
                });
            }

            return View(students);
        }

        public IActionResult Details(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentDetailsViewModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FullName = student.FirstName + " " + student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student
            {
                StudentNumber = viewModel.StudentNumber,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                DateOfBirth = viewModel.DateOfBirth
            };

            try
            {
                _studentService.AddStudent(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentEditViewModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student
            {
                Id = viewModel.Id,
                StudentNumber = viewModel.StudentNumber,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                DateOfBirth = viewModel.DateOfBirth
            };

            try
            {
                _studentService.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        public IActionResult Delete(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentDetailsViewModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                FullName = student.FirstName + " " + student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Delete", _studentService.GetById(id));
            }
        }
    }
}