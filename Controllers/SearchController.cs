using StudentProject.Models;
using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentProject.Controllers
{
    public class SearchController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();

        [Authorize]
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchUserRecord()
        {
            var countriesList = db.Countries.ToList();
            var coursesList = db.Courses.ToList();
            var rolesList = db.Roles.ToList();
            SearchFilterViewModel searchFilterViewModel = new SearchFilterViewModel
            {
                CountryList = countriesList,
                CourseList=coursesList,
                RoleList=rolesList
            };


            return View(searchFilterViewModel);
        }
        [HttpPost]
        public ActionResult SearchUserRecord(SearchFilterViewModel objSearchFilterViewModel)
        {
            UserInRole userInRole =new UserInRole();
            SearchFilterViewModel searchFilterViewModel = new SearchFilterViewModel();
            try
            {
                //var tempResult = (db.UserRegistrations.Where((i =>
                //  i.Fname.Contains(objSearchFilterViewModel.Fname.Trim()) ||
                //  i.Lname.Contains(objSearchFilterViewModel.Lname.Trim()) ||
                //  i.Email.Contains(objSearchFilterViewModel.Email.Trim()) ||
                //  i.Hobbies.Contains(objSearchFilterViewModel.Hobbies.Trim()) ||
                //  i.Gender.Contains(objSearchFilterViewModel.Gender) ||
                //  i.Address.CurrentAddress.Contains(objSearchFilterViewModel.UserAddress.Trim()) ||
                //  i.Address.PermanentAddress.Contains(objSearchFilterViewModel.UserAddress.Trim()) ||
                //  i.CourseId.Equals(objSearchFilterViewModel.CourseId) ||
                //  i.Address.State.Equals(objSearchFilterViewModel.StateId) ||
                //  i.Address.CityId.Equals(objSearchFilterViewModel.CityId) ||
                //  i.Address.CountryId.Equals(objSearchFilterViewModel.CountryId) ||
                //  i.Address.ZipCode.Equals(objSearchFilterViewModel.ZipCode) ||
                //  i.DateOfBirth.Equals(objSearchFilterViewModel.DateOfBirth) ||
                //  i.DateCreated.Equals(objSearchFilterViewModel.DateCreated)))
                //  .Select(i => i.UserId));


                //using linq
               
                //List<SearchFilterViewModel> searchFilterViewModel = new List<SearchFilterViewModel>();
                //List<UserRegistrationViewModel> userList = db.UserRegistrations.Where(x => 
                //        x.Fname.Contains(searchFilterViewModel.Fname) || 
                //        x.Lname.Contains(searchFilterViewModel.Lname)||
                //        x.DateOfBirth.Equals(searchFilterViewModel.DateOfBirth) ||
                //        x.CourseId.Equals(searchFilterViewModel.CourseId) ||
                //        x.Email.Contains(searchFilterViewModel.Email) ||
                //        x.Fname.Contains(searchFilterViewModel.Fname) ||
                //        x.Gender.Contains(searchFilterViewModel.Gender) ||
                //        x.IsActive.Equals(searchFilterViewModel.IsActive) ||
                //        x.IsVerified.Equals(searchFilterViewModel.IsVerified) ||
                //        x.Address.PermanentAddress.Contains(searchFilterViewModel.UserAddress)||
                //        x.Address.CurrentAddress.Contains(searchFilterViewModel.UserAddress) ||
                //        x.Address.CountryId.Equals(searchFilterViewModel.CountryId) ||
                //        x.Address.StateId.Equals(searchFilterViewModel.StateId) ||
                //        x.Address.CityId.Equals(searchFilterViewModel.CityId) ||
                //        x.Address.ZipCode.Equals(searchFilterViewModel.ZipCode) ||
                //        x.Hobbies.Contains(searchFilterViewModel.Hobbies))
                //        .Select(x => new UserRegistrationViewModel
                //        {
                //            UserId = x.UserId,
                //            Fname = x.Fname,
                //            Lname = x.Lname,
                //            DateOfBirth = x.DateOfBirth,
                //            Hobbies = x.Hobbies,
                //            Email = x.Email,
                //            Gender = x.Gender,
                //            CourseId = x.CourseId,
                //            IsActive = x.IsActive,
                //            IsVerified = x.IsVerified,
                //            AddressId = x.AddressId,
                //            CurrentAddress = x.Address.CurrentAddress,
                //            PermanentAddress = x.Address.PermanentAddress,
                //            ZipCode = x.Address.ZipCode,
                //            CountryId = x.Address.CountryId,
                //            StateId = x.Address.StateId,
                //            CityId = x.Address.CityId,

                //        }).ToList();

                List<SearchFilterViewModel> temp  = new List<SearchFilterViewModel>();
                var result = (from pd in db.Addresses
                              join od in db.UserRegistrations on pd.AddressId equals od.AddressId
                              join ct in db.UserInRoles on od.UserId equals ct.UserId
                              where (pd.CountryId.Equals(searchFilterViewModel.CountryList.Select(m=>m.CountryId))|| pd.CountryId == 0 )&&
                                    (pd.StateId.Equals(searchFilterViewModel.StateList.Select(m => m.StateId)) || pd.StateId == 0) &&
                                    (pd.CityId.Equals(searchFilterViewModel.CityList.Select(m => m.CityId)) || pd.CityId == 0) &&
                                    (pd.CurrentAddress.Contains(searchFilterViewModel.UserAddress) || pd.CurrentAddress == null) &&
                                    (pd.PermanentAddress.Contains(searchFilterViewModel.UserAddress) || pd.PermanentAddress == null) &&
                                    (pd.ZipCode.Equals(searchFilterViewModel.ZipCode) || pd.ZipCode == null) &&
                                    (od.Fname.Contains(searchFilterViewModel.Fname) || od.Fname == null) &&
                                    (od.Lname.Contains(searchFilterViewModel.Lname) || od.Lname == null) &&
                                    (od.Gender.Contains(searchFilterViewModel.Gender) || od.Gender == null) &&
                                    (od.DateCreated.Equals(searchFilterViewModel.DateCreated) || od.DateCreated == null) &&
                                    (od.DateOfBirth.Equals(searchFilterViewModel.DateOfBirth) || od.DateOfBirth == null) &&
                                    (od.IsActive.Equals(searchFilterViewModel.IsActive) || od.IsActive == false) &&
                                    (od.IsVerified.Equals(searchFilterViewModel.IsVerified) || od.IsVerified == false) &&
                                    (od.Email.Contains(searchFilterViewModel.Email) || od.Email == null) &&
                                    (od.CourseId.Equals(searchFilterViewModel.CourseList.Select(m=>m.CourseId)) || od.CourseId == null) &&
                                    (od.Hobbies.Contains(searchFilterViewModel.Hobbies) || od.Hobbies == null)&&
                                    (ct.RoleId.Equals(searchFilterViewModel.RoleId) || ct.RoleId == null) 

                              select new SearchFilterViewModel
                              {
                                  //UserId = od.UserId,
                                  Fname = od.Fname,
                                  Lname = od.Lname,
                                  DateOfBirth = od.DateOfBirth,
                                  Hobbies = od.Hobbies,
                                  Email = od.Email,
                                  Gender = od.Gender,
                                  CourseId = od.CourseId,
                                  IsActive = od.IsActive,
                                  IsVerified = od.IsVerified,
                                  AddressId = od.AddressId,
                                  UserAddress = od.Address.CurrentAddress,
                                 // UserAddress = od.Address.PermanentAddress,
                                  ZipCode = od.Address.ZipCode,
                                  CountryId = od.Address.CountryId,
                                  StateId = od.Address.StateId,
                                  CityId = od.Address.CityId,

                              }).ToList();
                foreach (var item in result)
                {
                    SearchFilterViewModel tempList = new SearchFilterViewModel();
                   
                    temp.Add(tempList);
                }

                return View(temp);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }

            return View();

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
    }
}