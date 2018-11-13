using StudentProject.Models;
using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace StudentProject.Controllers
{
    /// <summary>
    /// This custom is used to check that only those people who are authenticated can access it only
    /// </summary>
    //[SessionMaintainController]
    public class DefaultController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();

        // GET: Default
        public ActionResult Index()

        {
            //for that list to show
            List<UserRegistrationViewModel> objUserRegistrationViewModelList = new List<UserRegistrationViewModel>();
            try
            {

                //var query = (from c in db.UserRegistrations
                //             select c).ToList();
                var result = (from item in db.UserRegistrations
                              join c in db.Addresses on item.AddressId equals c.AddressId
                              join ct in db.UserInRoles on item.UserId equals ct.UserId
                              join cy in db.Roles on ct.RoleId equals cy.RoleId
                              join cx in db.Courses on item.CourseId equals cx.CourseId
                              join cz in db.Countries on c.CountryId equals cz.CountryId
                              join ca in db.States on c.StateId equals ca.StateId
                              join cb in db.Cities on c.CityId equals cb.CityId
                              orderby item.DateCreated
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
                                 IsActive = item.IsActive,
                                 IsVerified = item.IsVerified,
                                 AddressId = item.AddressId,
                                 CurrentAddress = item.Address.CurrentAddress,
                                 PermanentAddress = item.Address.PermanentAddress,
                                 ZipCode = item.Address.ZipCode,
                                 CountryName = cz.CountryName,
                                 StateName = ca.StateName,
                                 CityName = cb.CityName,
                                 RoleName= cy.RoleName
                             }).ToList();

                //db.UserRegistrations.SingleOrDefault(a => a.CourseId == 10);
                // Diffent way to implement the same logicas above
                //foreach (var item in result)
                //{
                //    UserRegistrationViewModel objUserReg = new UserRegistrationViewModel
                //    {
                //        UserId = item.UserRegistrations.UserId,
                //        Fname = item.UserRegistrations.Fname,
                //        Lname = item.UserRegistrations.Lname,
                //        DateOfBirth = item.UserRegistrations.DateOfBirth,
                //        Hobbies = item.UserRegistrations.Hobbies,
                //        Email = item.UserRegistrations.Email,
                //        Password = item.UserRegistrations.Password,
                //        Gender = item.UserRegistrations.Gender,
                //        CourseId = item.UserRegistrations.CourseId,
                //        IsActive = item.UserRegistrations.IsActive,
                //        IsVerified=item.UserRegistrations.IsVerified,
                //        AddressId = item.UserRegistrations.AddressId,
                //        CurrentAddress=item.Addresses.CurrentAddress,
                //        PermanentAddress = item.Addresses.PermanentAddress,
                //        ZipCode = item.Addresses.ZipCode,
                //        CountryId = item.Addresses.CountryId,
                //        StateId = item.Addresses.StateId,
                //        CityId = item.Addresses.CityId
                //    };
                //    objUserRegistrationViewModelList.Add(objUserReg);

                //}
                return View(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }
            //return View(objUserRegistrationViewModelList);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string searching)
        {
            //var users = from s in db.UserRegistrations where s.RoleId != 1 && s.RoleId != 2 select s;
            var result = (from item in db.UserRegistrations
                          join c in db.Addresses on item.AddressId equals c.AddressId
                          join ct in db.UserInRoles on item.UserId equals ct.UserId
                          join cy in db.Roles on ct.RoleId equals cy.RoleId
                          join cx in db.Courses on item.CourseId equals cx.CourseId
                          join cz in db.Countries on c.CountryId equals cz.CountryId
                          join ca in db.States on c.StateId equals ca.StateId
                          join cb in db.Cities on c.CityId equals cb.CityId
                          join s in db.TeacherInSubjects on item.UserId equals s.UserId
                          join sn in db.Subjects on s.SubjectId equals sn.SubjectId
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
                              IsActive = item.IsActive,
                              IsVerified = item.IsVerified,
                              AddressId = item.AddressId,
                              CurrentAddress = item.Address.CurrentAddress,
                              PermanentAddress = item.Address.PermanentAddress,
                              ZipCode = item.Address.ZipCode,
                              CountryName = cz.CountryName,
                              StateName = ca.StateName,
                              CityName = cb.CityName,
                              RoleName = cy.RoleName,
                              SubjectName=sn.SubjectName
                          }).DefaultIfEmpty().ToList();
            if (!String.IsNullOrEmpty(searching))
            {
               var newResult = result.Where(s => s.RoleName.Contains(searching) ||
                                            s.Fname.Contains(searching) ||
                                            s.Lname.Contains(searching) ||
                                            s.CourseName.Contains(searching)||
                                            s.Gender.Contains(searching)||
                                            s.CityName.Contains(searching)||
                                            s.StateName.Contains(searching)||
                                            s.CountryName.Contains(searching)||
                                            s.Email.Contains(searching)||
                                            s.SubjectName.Contains(searching)
                                            ).DefaultIfEmpty();
                return View(newResult.ToList());
            }

            return View(result);
            // var returnedResult = db.Users.Where(x => x.RoleId != 1 && x.RoleId != 2).ToList();
            // return View(returnedResult);
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
                return Json(isValid,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
                return null;
            }
            
        }
        

        /// <summary>
        ///post method for create method and in this we send our data which is in view model to our db connected edmx model
        /// </summary>
        /// <param name="userRegistrationViewModel"></param>
        /// <returns></returns>
        /// 

        [HttpGet]
        public ActionResult CreateUser()
        {
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();
            //Country dropdown, 
            //just remember all the method that you have use dwith milind all are right
            //List<Country> countriesList = new List<Country>();
            //List<Course> coursesList = new List<Course>();
            //List<Role> rolesList = new List<Role>();

            try
            {
                //data from db is filled in the data variable which is in the form of list
                var countriesList = db.Countries.ToList();
                var coursesList = db.Courses.ToList();
                var rolesList = db.Roles.ToList();
                var subjectList = db.Subjects.ToList();
                //using loop putting each value in the countriesList
                //foreach (var item in countryData)
                //{
                //    Country testCountry = new Country
                //    {
                //        CountryId = Convert.ToInt32(item.Value),
                //        CountryName = item.Text.ToString()
                //    };
                //    countriesList.Add(testCountry);
                //}
                //foreach (var item in courseData)
                //{
                //    Course testCourse = new Course
                //    {
                //        CourseId = Convert.ToInt32(item.Value),
                //        CourseName = item.Text.ToString()
                //    };
                //    coursesList.Add(testCourse);
                //}
                //foreach (var item in roleData)
                //{
                //    Role testRole = new Role
                //    {
                //        RoleId = Convert.ToInt32(item.Value),
                //        RoleName = item.Text.ToString()
                //    };
                //    rolesList.Add(testRole);
                //}

                //Assigning that list to that viewmodel
                userRegistrationViewModel.CountryList = countriesList;
                userRegistrationViewModel.CourseList = coursesList;
                userRegistrationViewModel.RoleList = rolesList;
                userRegistrationViewModel.SubjectList = subjectList;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }
            
            return View(userRegistrationViewModel);
        }
           
           
        [HttpPost]
        public ActionResult CreateUser(UserRegistrationViewModel userRegistrationViewModel)
        {
            //UserRegistration objUserRegistration = new UserRegistration();
            
            //  var transaction = objUserRegistration;

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                UserRegistration objUserRegistration = db.UserRegistrations.Create();
                Address address = db.Addresses.Create();
                Course course = db.Courses.Create();
                UserInRole userInRole = db.UserInRoles.Create();
                SecurityTable securityTable = db.SecurityTables.Create();
                try
                {

                    if (ModelState.IsValid)
                    {
                      
                                address.CurrentAddress = userRegistrationViewModel.CurrentAddress;
                                address.PermanentAddress = userRegistrationViewModel.PermanentAddress;
                                address.ZipCode = userRegistrationViewModel.ZipCode;
                                address.CountryId = userRegistrationViewModel.CountryId;
                                address.StateId = userRegistrationViewModel.StateId;
                                address.CityId = userRegistrationViewModel.CityId;

                                //Random random = new Random();
                                //int i = random.Next();
                                //userRegistrationViewModel.AddressId = i;

                                // for only taking what it return to check
                                db.Addresses.Add(address);
                                db.SaveChanges();// savepoint 1

                                var temp = address.AddressId;

                                
                                //not require because .add create new id
                                objUserRegistration.UserId = Guid.NewGuid();
                                objUserRegistration.Fname = userRegistrationViewModel.Fname;
                                objUserRegistration.Lname = userRegistrationViewModel.Lname;
                                objUserRegistration.DateOfBirth = userRegistrationViewModel.DateOfBirth;
                                objUserRegistration.Hobbies = userRegistrationViewModel.Hobbies;
                                objUserRegistration.Email = userRegistrationViewModel.Email;
                                //objUserRegistration.Password = userRegistrationViewModel.Password;
                                objUserRegistration.Gender = userRegistrationViewModel.Gender;
                                objUserRegistration.IsActive = userRegistrationViewModel.IsActive;
                                objUserRegistration.IsVerified = userRegistrationViewModel.IsVerified;
                                objUserRegistration.DateCreated = DateTime.Now;
                                objUserRegistration.DateModified = DateTime.Now;
                                objUserRegistration.CourseId = userRegistrationViewModel.CourseId;
                                
                                objUserRegistration.AddressId = temp;

                                ////for course
                                //var tempCourse = db.Courses.Where(s => s.CourseId == userRegistrationViewModel.CourseId).Select(s => s.CourseName);

                                //password Hashing passwords with MD5 or sha-256 C#
                                objUserRegistration.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                                        .ComputeHash(Encoding.UTF8.GetBytes(userRegistrationViewModel.Password)));

                                db.UserRegistrations.Add(objUserRegistration);
                                        db.SaveChanges();// savepoint 2
                                var tempUserId = db.UserRegistrations.Select(a => a.UserId);

                                //for Role in UserInRole

                                userInRole.RoleId = userRegistrationViewModel.RoleId;
                                userInRole.UserId = objUserRegistration.UserId;
                                db.UserInRoles.Add(userInRole);
                                db.SaveChanges();

                        // Security PIN
                        securityTable.UserId = objUserRegistration.UserId;
                        securityTable.SecurityQuestion = userRegistrationViewModel.SecurityPIN;
                        db.SecurityTables.Add(securityTable);
                        db.SaveChanges();

                        dbTransaction.Commit();
                      
                        Response.Write("<script>alert('Data inserted successfully')</script>");
                        return RedirectToAction("UserLogIn", "UserLogIn");
                    }

                    ModelState.AddModelError("", "please fill the form properly");
                    return View(userRegistrationViewModel);
                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
                    dbTransaction.Rollback();
                    ModelState.AddModelError("", "please fill the form properly");
                }
            }

            return RedirectToAction("Index", "Home");
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
            }
            
            return Json(citieslist, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// here you can also can do soft delete to show to admin just remember 
        /// this feature is waht you get learnt by pranav for this see notes for detailed explaination 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public ActionResult DeleteConfirmed(Guid UserId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var deleteRecordFromUserRegistration = db.UserRegistrations.FirstOrDefault(s => s.UserId == UserId);
                    var deleteRecordFromAddress = db.Addresses.FirstOrDefault(s => s.AddressId == deleteRecordFromUserRegistration.AddressId);
                    var deleteRecordFromUserInRole = db.UserInRoles.FirstOrDefault(s => s.UserId == UserId);
                    if ((deleteRecordFromUserRegistration != null && deleteRecordFromAddress != null) && deleteRecordFromUserInRole != null)
                    {
                        db.Addresses.Remove(deleteRecordFromAddress);
                        db.UserInRoles.Remove(deleteRecordFromUserInRole);
                        db.UserRegistrations.Remove(deleteRecordFromUserRegistration);
                        
                        db.SaveChanges();

                        dbTransaction.Commit();
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception source: {0} user is failed to register", ex.Message);
                    dbTransaction.Rollback();
                }
               
                return RedirectToAction("Index");
            }
            
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }
            
            return RedirectToAction("Index");
        }


        public ActionResult Dashboard()
        {
            return View();
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
            try {

                var objUserRegistration = db.UserRegistrations.Where(val => val.Email == isVerifiedByAdmin.Email).SingleOrDefault();
                if(objUserRegistration != null)
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

    }

}