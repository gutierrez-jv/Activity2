using Activity2.Models.Database.Models;
using Activity2.Services;
using Activity2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Activity2.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentsController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        private void PopulateDropdowns(object? selectedStudentId = null, object? selectedCourseId = null)
        {
            var students = _studentService.GetAllStudents()
                .Select(s => new { s.Id, FullName = s.FirstName + " " + s.LastName });

            var courses = _courseService.GetAllCourses()
                .Select(c => new { c.Id, Label = c.CourseCode + " - " + c.CourseName });

            ViewBag.StudentId = new SelectList(students, "Id", "FullName", selectedStudentId);
            ViewBag.CourseId = new SelectList(courses, "Id", "Label", selectedCourseId);
        }

        public IActionResult Index()
        {
            var enrollments = new List<EnrollmentViewModel>();

            foreach (var e in _enrollmentService.GetAllEnrollments())
            {
                var student = _studentService.GetById(e.StudentId);
                var course = _courseService.GetById(e.CourseId);

                enrollments.Add(new EnrollmentViewModel
                {
                    Id = e.Id,
                    StudentName = student != null ? student.FirstName + " " + student.LastName : "Unknown",
                    CourseName = course != null ? course.CourseCode + " - " + course.CourseName : "Unknown",
                    EnrollmentDate = e.EnrollmentDate
                });
            }

            return View(enrollments);
        }

        public IActionResult Details(int id)
        {
            var enrollment = _enrollmentService.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById(enrollment.StudentId);
            var course = _courseService.GetById(enrollment.CourseId);

            var viewModel = new EnrollmentDetailsViewModel
            {
                Id = enrollment.Id,
                StudentName = student != null ? student.FirstName + " " + student.LastName : "Unknown",
                CourseName = course != null ? course.CourseCode + " - " + course.CourseName : "Unknown",
                EnrollmentDate = enrollment.EnrollmentDate
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EnrollmentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(viewModel.StudentId, viewModel.CourseId);
                return View(viewModel);
            }

            var enrollment = new Enrollment
            {
                StudentId = viewModel.StudentId,
                CourseId = viewModel.CourseId,
                EnrollmentDate = viewModel.EnrollmentDate
            };

            try
            {
                _enrollmentService.AddEnrollment(enrollment);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                PopulateDropdowns(viewModel.StudentId, viewModel.CourseId);
                return View(viewModel);
            }
        }

        public IActionResult Edit(int id)
        {
            var enrollment = _enrollmentService.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var viewModel = new EnrollmentEditViewModel
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = enrollment.EnrollmentDate
            };

            PopulateDropdowns(enrollment.StudentId, enrollment.CourseId);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EnrollmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(viewModel.StudentId, viewModel.CourseId);
                return View(viewModel);
            }

            var enrollment = new Enrollment
            {
                Id = viewModel.Id,
                StudentId = viewModel.StudentId,
                CourseId = viewModel.CourseId,
                EnrollmentDate = viewModel.EnrollmentDate
            };

            try
            {
                _enrollmentService.UpdateEnrollment(enrollment);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                PopulateDropdowns(viewModel.StudentId, viewModel.CourseId);
                return View(viewModel);
            }
        }

        public IActionResult Delete(int id)
        {
            var enrollment = _enrollmentService.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById(enrollment.StudentId);
            var course = _courseService.GetById(enrollment.CourseId);

            var viewModel = new EnrollmentDetailsViewModel
            {
                Id = enrollment.Id,
                StudentName = student != null ? student.FirstName + " " + student.LastName : "Unknown",
                CourseName = course != null ? course.CourseCode + " - " + course.CourseName : "Unknown",
                EnrollmentDate = enrollment.EnrollmentDate
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _enrollmentService.DeleteEnrollment(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Delete", _enrollmentService.GetById(id));
            }
        }
    }
}