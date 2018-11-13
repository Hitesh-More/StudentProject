using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentProject.Models;
using StudentProject.ModelStudent;
using System.Text;

namespace StudentProject.Controllers
{
    public class TeacherController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        /// <summary>
        /// This is for to check that email id is already is available or not
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
       

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



        /// <summary>
        /// here teacher can see his list of students
        /// </summary>
        /// <returns></returns>
        ///
        public ActionResult MyStudent(Guid id)
        {
            //for that list to show
                List<UserRegistrationViewModel> objUserRegistrationViewModelList = new List<UserRegistrationViewModel>();
                try
                {
                var testSubjectId = db.TeacherInSubjects.Where(b =>b.UserId == id).Select(a => a.SubjectId).FirstOrDefault();
                var testCourseId = db.SubjectInCourses.Where(s=>s.SubjectId == testSubjectId).Select(a => a.CourseId).ToList();
                var result = (from item in db.UserRegistrations
                                  join c in db.Addresses on item.AddressId equals c.AddressId
                                  join ct in db.UserInRoles on item.UserId equals ct.UserId
                                  join cy in db.Roles on ct.RoleId equals cy.RoleId
                                  join cx in db.Courses on item.CourseId equals cx.CourseId
                                  join cz in db.Countries on c.CountryId equals cz.CountryId
                                  join ca in db.States on c.StateId equals ca.StateId
                                  join cb in db.Cities on c.CityId equals cb.CityId
                                  join ts in db.TeacherInSubjects on item.UserId equals ts.UserId
                                  where cy.RoleName == "Student"
                                  where testCourseId.Contains(item.CourseId.Value)
                                  select new UserRegistrationViewModel
                                  {
                                      UserId = item.UserId,
                                      Fname = item.Fname,
                                      Lname = item.Lname,
                                      DateOfBirth = item.DateOfBirth,
                                      Hobbies = item.Hobbies,
                                      Email = item.Email,
                                      Password = item.Password,
                                      Gender = item.Gender,
                                      CourseName = cx.CourseName,
                                      CourseId=cx.CourseId,
                                      IsActive = item.IsActive,
                                      IsVerified = item.IsVerified,
                                      AddressId = item.AddressId,
                                      CurrentAddress = item.Address.CurrentAddress,
                                      PermanentAddress = item.Address.PermanentAddress,
                                      ZipCode = item.Address.ZipCode,
                                      CountryName = cz.CountryName,
                                      StateName = ca.StateName,
                                      CityName = cb.CityName,
                                      RoleName = cy.RoleName
                                  }).ToList();
               // var result = testStudent.Where(s => s.CourseId == testCourseId.ToList());

                    return View(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
                }

                // return View("C:\Users\Hitesh_More\source\repos\StudentProject\StudentProject\Views\GetAllUser\AllAdmin.cshtml", UserRegistrationViewModel);
                return View();
            
        }
       
    }
}