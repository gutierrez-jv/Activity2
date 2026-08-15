using Activity2.Models.Database.Models;
using Activity2.Services;
using Activity2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Activity2.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var courses = new List<CourseViewModel>();

            foreach (var c in _courseService.GetAllCourses())
            {
                courses.Add(new CourseViewModel
                {
                    Id = c.Id,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    Units = c.Units
                });
            }

            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Units = course.Units
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var course = new Course
            {
                CourseCode = viewModel.CourseCode,
                CourseName = viewModel.CourseName,
                Units = viewModel.Units
            };

            try
            {
                _courseService.AddCourse(course);
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
            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseEditViewModel
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Units = course.Units
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CourseEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var course = new Course
            {
                Id = viewModel.Id,
                CourseCode = viewModel.CourseCode,
                CourseName = viewModel.CourseName,
                Units = viewModel.Units
            };

            try
            {
                _courseService.UpdateCourse(course);
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
            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Units = course.Units
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _courseService.DeleteCourse(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Delete", _courseService.GetById(id));
            }
        }
    }
}