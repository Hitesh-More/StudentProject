using StudentProject.Models;
using StudentProject.ModelStudent;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentProject.Controllers
{
    public class SuperAdminController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult CreateNewUser()
        //{
        //    try
        //    {
        //     //   DefaultController defaultController = new DefaultController();
        //     ////   UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();
        //     //   defaultController.CreateUser();//to call get method 
        //      // var result = defaultController.CreateUser(userRegistrationViewModel);
        //        return View("CreateUser", "UserRegistrationViewModel");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
        //    }
        //    return View();
        //}

        //public ActionResult DeleteConfirmed(Guid UserId)
        //{
        //    try
        //    {
        //        return View("../Default/CreateUser", "UserRegistrationViewModel");
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
        //    }
        //    return View("../Default/Index");
        //}

        public ActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// This is for to check that email id is already is available or not
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckEmailExistence(string Email)
        {

            try
            {
                bool isValid = !db.UserRegistrations.ToList().Exists(p => p.Email.Equals(Email, StringComparison.CurrentCultureIgnoreCase));
                return Json(isValid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
                return null;
            }

        }


       

        /// <summary>
        /// this for binding as well as get list of selected states
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public JsonResult GetStates(int CountryId)
        {
            //State dropdown
            List<State> stateslist = new List<State>();
            try
            {
                //data from db is filled in the data variable which is in the form of list
                var newTemp = db.States.Where(val => val.CountryId == CountryId).Select(val => new { val.StateName, val.StateId }).ToList();

                //using loop putting each value in the countriesList
                foreach (var item in newTemp)
                {
                    State testState = new State
                    {
                        StateId = Convert.ToInt32(item.StateId),
                        StateName = item.StateName.ToString()
                    };
                    stateslist.Add(testState);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
            }
            return Json(stateslist, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// this for binding as well as get list of selected cities
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public JsonResult GetCities(int StateId)
        {
            //City dropdown
            List<City> citieslist = new List<City>();
            try
            {
                //data from db is filled in the data variable which is in the form of list
                var newTemp = db.Cities.Where(val => val.StateId == StateId).Select(val => new { val.CityName, val.CityId }).ToList();

                //using loop putting each value in the countriesList
                foreach (var item in newTemp)
                {
                    City testCity = new City
                    {
                        CityId = Convert.ToInt32(item.CityId),
                        CityName = item.CityName.ToString()
                    };
                    citieslist.Add(testCity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
            }

            return Json(citieslist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(AddSubjectViewModel objAddSubjectViewModel)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                Subject subject = new Subject();
            try
                {
                    if (ModelState.IsValid)
                    {
                        subject.SubjectName = objAddSubjectViewModel.Subject;
                        db.Subjects.Add(subject);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        
                        return RedirectToAction("Dashboard","SuperAdmin",Session["RoleId"]);
                    }
                    return View(objAddSubjectViewModel);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
                    dbTransaction.Rollback();
                }
            }
            return RedirectToAction( "Dashboard", "SuperAdmin");
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(AddCourseViewModel objAddCourseViewModel)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                Course course = new Course();
                try
                {
                    if (ModelState.IsValid)
                    {
                        course.CourseName = objAddCourseViewModel.Course;
                        db.Courses.Add(course);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        return RedirectToAction("Dashboard");
                    }
                    return View(objAddCourseViewModel);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
                    dbTransaction.Rollback();
                }
            }
            return RedirectToAction("Dashboard", "SuperAdmin");
        }
        [HttpGet]
        public ActionResult MapCourseAndSubject()
        {
            MapCourseAndSubjectViewModel objMapCourseAndSubjectViewModel = new MapCourseAndSubjectViewModel();

            var subjectsList = db.Subjects.ToList();
            objMapCourseAndSubjectViewModel.SubjectList = subjectsList;
            var coursesList = db.Courses.ToList();
            objMapCourseAndSubjectViewModel.CourseList = coursesList;

            return View(objMapCourseAndSubjectViewModel);
        }
        [HttpPost]
        public ActionResult MapCourseAndSubject(MapCourseAndSubjectViewModel objMapCourseAndSubjectViewModel)
        {
            
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                SubjectInCourse subjectInCourse = db.SubjectInCourses.Create();
                
                try
                {
                    if (ModelState.IsValid)
                    {

                        subjectInCourse.CourseId = objMapCourseAndSubjectViewModel.CourseId;
                        subjectInCourse.SubjectId = objMapCourseAndSubjectViewModel.SubjectId;

                        db.SubjectInCourses.Add(subjectInCourse);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        return RedirectToAction("Dashboard");
                    }
                    return View(objMapCourseAndSubjectViewModel);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
                    dbTransaction.Rollback();
                }
            }
            return RedirectToAction("Dashboard", "SuperAdmin");
        }

        /// <summary>
        /// here edit first get values from user model and remember to take object in the form of the db.tablename ok 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Edit(Guid id)
        {
            UserRegistrationViewModel objUserRegistrationViewModel = new UserRegistrationViewModel();
            try
            {
                var objUserRegistration = db.UserRegistrations.FirstOrDefault(s => s.UserId == id);
                var countriesList = db.Countries.ToList();
                var statesList = db.States.ToList();
                var citiesList = db.Cities.ToList();
                var coursesList = db.Courses.ToList();
                var rolesList = db.Roles.ToList();
                objUserRegistrationViewModel.CountryList = countriesList;
                objUserRegistrationViewModel.StateList = statesList;
                objUserRegistrationViewModel.CityList = citiesList;
                objUserRegistrationViewModel.CourseList = coursesList;
                objUserRegistrationViewModel.RoleList = rolesList;
                {
                    objUserRegistrationViewModel.Fname = objUserRegistration.Fname;
                    objUserRegistrationViewModel.Lname = objUserRegistration.Lname;
                    objUserRegistrationViewModel.DateOfBirth = objUserRegistration.DateOfBirth;
                    objUserRegistrationViewModel.Hobbies = objUserRegistration.Hobbies;
                    objUserRegistrationViewModel.Email = objUserRegistration.Email;

                    objUserRegistrationViewModel.Gender = objUserRegistration.Lname;
                    objUserRegistrationViewModel.IsActive = objUserRegistration.IsActive;
                    objUserRegistrationViewModel.IsVerified = objUserRegistration.IsVerified;
                    objUserRegistrationViewModel.UserId = id;
                    objUserRegistrationViewModel.CityId = objUserRegistration.Address.CityId;
                    objUserRegistrationViewModel.StateId = objUserRegistration.Address.StateId;
                    objUserRegistrationViewModel.CountryId = objUserRegistration.Address.CountryId;
                    objUserRegistrationViewModel.PermanentAddress = objUserRegistration.Address.PermanentAddress;
                    objUserRegistrationViewModel.CurrentAddress = objUserRegistration.Address.CurrentAddress;
                    objUserRegistrationViewModel.ZipCode = objUserRegistration.Address.ZipCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} user is failed to register", ex.Source);
            }

            return View(objUserRegistrationViewModel);
        }

        /// <summary>
        /// here first take vaue from that object and assign it to db
        /// </summary>
        /// <param name="objUserRegistrationViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Edit(UserRegistrationViewModel objUserRegistrationViewModel)
        {
            var objUserRegistration = db.UserRegistrations.Where(val => val.UserId == objUserRegistrationViewModel.UserId).SingleOrDefault();
            try
            {
                if (objUserRegistration != null)
                {
                    objUserRegistration.Fname = objUserRegistrationViewModel.Fname;
                    objUserRegistration.Lname = objUserRegistrationViewModel.Lname;
                    objUserRegistration.DateOfBirth = objUserRegistrationViewModel.DateOfBirth;
                    objUserRegistration.Hobbies = objUserRegistrationViewModel.Hobbies;
                    objUserRegistration.Email = objUserRegistrationViewModel.Email;

                    objUserRegistration.Gender = objUserRegistrationViewModel.Lname;
                    objUserRegistration.IsActive = objUserRegistrationViewModel.IsActive;
                    objUserRegistration.IsVerified = objUserRegistrationViewModel.IsVerified;
                    objUserRegistration.DateModified = DateTime.Now;
                    objUserRegistration.Address.CityId = objUserRegistrationViewModel.CityId;
                    objUserRegistration.Address.StateId = objUserRegistrationViewModel.StateId;
                    objUserRegistration.Address.CountryId = objUserRegistrationViewModel.CountryId;
                    objUserRegistration.Address.PermanentAddress = objUserRegistrationViewModel.PermanentAddress;
                    objUserRegistration.Address.CurrentAddress = objUserRegistrationViewModel.CurrentAddress;
                    objUserRegistration.Address.ZipCode = objUserRegistrationViewModel.ZipCode;

                    //oonce course is selected then then student can not change without permission of the Admin 
                    //objUserRegistration.CourseId = userRegistrationViewModel.CourseId;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Isverified(Guid id)
        {
            IsVerifiedByAdmin isVerifiedByAdmin = new IsVerifiedByAdmin();
            var objUserRegistration = db.UserRegistrations.FirstOrDefault(s => s.UserId == id);
            {
                isVerifiedByAdmin.IsVerified = objUserRegistration.IsVerified;
                isVerifiedByAdmin.Email = objUserRegistration.Email;
            }

            return View(isVerifiedByAdmin);
        }
        [HttpPost]
        public ActionResult Isverified(IsVerifiedByAdmin isVerifiedByAdmin)
        {
            try
            {

                var objUserRegistration = db.UserRegistrations.Where(val => val.Email == isVerifiedByAdmin.Email).SingleOrDefault();
                if (objUserRegistration != null)
                {
                    objUserRegistration.IsVerified = isVerifiedByAdmin.IsVerified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult IsActive(Guid id)
        {
            IsActivatededByAdmin isActivatededByAdmin = new IsActivatededByAdmin();
            var objUserRegistration = db.UserRegistrations.FirstOrDefault(s => s.UserId == id);
            {
                isActivatededByAdmin.IsActivated = objUserRegistration.IsActive;
                isActivatededByAdmin.Email = objUserRegistration.Email;
            }

            return View(isActivatededByAdmin);
        }
        [HttpPost]
        public ActionResult IsActive(IsActivatededByAdmin isActivatededByAdmin)
        {
            try
            {

                var objUserRegistration = db.UserRegistrations.Where(val => val.Email == isActivatededByAdmin.Email).SingleOrDefault();
                if (objUserRegistration != null)
                {
                    objUserRegistration.IsActive = isActivatededByAdmin.IsActivated;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Assign subject to teacher 
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public ActionResult AssignSubjectToTeacher(Guid id)
        {
            try
            {
                var subjectList = db.Subjects.ToList();
                AssignSubjectToTeacher assignSubjectToTeacher = new AssignSubjectToTeacher
                {
                    SubjectList = subjectList,
                    UserId = id
                };
                return View(assignSubjectToTeacher);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
                
            }
            return View();

        }
        [HttpPost]
        public ActionResult AssignSubjectToTeacher(AssignSubjectToTeacher assignSubjectToTeacher)
        {
            try
            {
                TeacherInSubject teacherInSubject = new TeacherInSubject();
                var objUserRegistration = db.UserRegistrations.Where(val => val.UserId == assignSubjectToTeacher.UserId).FirstOrDefault();
                if(objUserRegistration!=null)
                {
                    teacherInSubject.UserId = assignSubjectToTeacher.UserId;
                    teacherInSubject.SubjectId = assignSubjectToTeacher.SubjectId;
                    db.TeacherInSubjects.Add(teacherInSubject);
                    db.SaveChanges();
                    return RedirectToAction("Dashboard", "SuperAdmin");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
                return View("Dashboard", null);
            }

            return View("SubjectTeacher");
        }
        /// <summary>
        ///list of all subject teachers with their subject
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectTeacher()
        {
            try
            {
                // to show list
                var result = (from item in db.UserRegistrations
                              join ct in db.TeacherInSubjects on item.UserId equals ct.UserId
                              join cy in db.Subjects on ct.SubjectId equals cy.SubjectId
                              select new AssignSubjectToTeacher
                              {
                                  Email = item.Email,
                                  SubjectName = cy.SubjectName
                              }).ToList();
                return View(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }
            return View();
        }
    }
}