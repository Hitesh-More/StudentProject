using StudentProject.Models;
using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace StudentProject.Controllers
{
    public class GetAllUserController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();
        // GET: GetAllUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllUser()

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
                                  RoleName = cy.RoleName
                              }).ToList();


                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }

            return View();
        }

        public ActionResult AllSuperAdmin()

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
                              where cy.RoleName == "SuperAdmin"
                              where cy.RoleId==4
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
                                  RoleName = cy.RoleName
                              }).ToList();


                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }

            return View();
        }

        public ActionResult AllAdmin()

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
                              where cy.RoleName=="Admin"
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
                                  RoleName = cy.RoleName
                              }).ToList();

              
                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }
           
            return View();
        }

        public ActionResult AllTeacher()

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
                              where cy.RoleName == "Teacher"
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
                                  RoleName = cy.RoleName
                              }).ToList();


                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }

            // return View("C:\Users\Hitesh_More\source\repos\StudentProject\StudentProject\Views\GetAllUser\AllAdmin.cshtml", UserRegistrationViewModel);
            return View();
        }

        public ActionResult AllStudent()

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
                              where cy.RoleName == "Student"
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
                                  RoleName = cy.RoleName
                              }).ToList();


                return View(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }

            // return View("C:\Users\Hitesh_More\source\repos\StudentProject\StudentProject\Views\GetAllUser\AllAdmin.cshtml", UserRegistrationViewModel);
            return View();
        }

        //public ActionResult AllTeacher(int id)

        //{
        //    //for that list to show
        //    List<UserRegistrationViewModel> objUserRegistrationViewModelList = new List<UserRegistrationViewModel>();
        //    try
        //    {

        //        //var query = (from c in db.UserRegistrations
        //        //             select c).ToList();
        //        var result = (from item in db.UserRegistrations
        //                      join c in db.Addresses on item.AddressId equals c.AddressId
        //                      join ct in db.UserInRoles on item.UserId equals ct.UserId
        //                      join cy in db.Roles on ct.RoleId equals cy.RoleId
        //                      join cx in db.Courses on item.CourseId equals cx.CourseId
        //                      join cz in db.Countries on c.CountryId equals cz.CountryId
        //                      join ca in db.States on c.StateId equals ca.StateId
        //                      join cb in db.Cities on c.CityId equals cb.CityId
        //                      where cy.RoleName == "Teacher"
                              
        //                      select new UserRegistrationViewModel
        //                      {
        //                          UserId = item.UserId,
        //                          Fname = item.Fname,
        //                          Lname = item.Lname,
        //                          DateOfBirth = item.DateOfBirth,
        //                          Hobbies = item.Hobbies,
        //                          Email = item.Email,
        //                          Password = item.Password,
        //                          Gender = item.Gender,
        //                          CourseName = cx.CourseName,
        //                          IsActive = item.IsActive,
        //                          IsVerified = item.IsVerified,
        //                          AddressId = item.AddressId,
        //                          CurrentAddress = item.Address.CurrentAddress,
        //                          PermanentAddress = item.Address.PermanentAddress,
        //                          ZipCode = item.Address.ZipCode,
        //                          CountryName = cz.CountryName,
        //                          StateName = ca.StateName,
        //                          CityName = cb.CityName,
        //                          RoleName = cy.RoleName
        //                      }).ToList();
        //        var resultStudent = (from item in db.UserRegistrations
        //                      join c in db.Addresses on item.AddressId equals c.AddressId
        //                      join ct in db.UserInRoles on item.UserId equals ct.UserId
        //                      join cy in db.Roles on ct.RoleId equals cy.RoleId
        //                      join cx in db.Courses on item.CourseId equals cx.CourseId
        //                      join cz in db.Countries on c.CountryId equals cz.CountryId
        //                      join ca in db.States on c.StateId equals ca.StateId
        //                      join cb in db.Cities on c.CityId equals cb.CityId
        //                      where cy.RoleName == "Student"

        //                      select new UserRegistrationViewModel
        //                      {
        //                          UserId = item.UserId,
        //                          Fname = item.Fname,
        //                          Lname = item.Lname,
        //                          DateOfBirth = item.DateOfBirth,
        //                          Hobbies = item.Hobbies,
        //                          Email = item.Email,
        //                          Password = item.Password,
        //                          Gender = item.Gender,
        //                          CourseName = cx.CourseName,
        //                          IsActive = item.IsActive,
        //                          IsVerified = item.IsVerified,
        //                          AddressId = item.AddressId,
        //                          CurrentAddress = item.Address.CurrentAddress,
        //                          PermanentAddress = item.Address.PermanentAddress,
        //                          ZipCode = item.Address.ZipCode,
        //                          CountryName = cz.CountryName,
        //                          StateName = ca.StateName,
        //                          CityName = cb.CityName,
        //                          RoleName = cy.RoleName
        //                      }).ToList();

        //        //var temp1 = resultStudent.Select(m => m.CourseId).ToList();
        //        //var temp = resultTeacher.Select(m => m.CourseId).ToList();
        //        //var t= db.UserRegistrations.Where(temp1.Where(a=>a.))

        //        return View(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
        //    }

        //    // return View("C:\Users\Hitesh_More\source\repos\StudentProject\StudentProject\Views\GetAllUser\AllAdmin.cshtml", UserRegistrationViewModel);
        //    return View();
        //}

        
       public ActionResult MyProfile(String Email)
        {
            var result = (from item in db.UserRegistrations
                          join c in db.Addresses on item.AddressId equals c.AddressId
                          join ct in db.UserInRoles on item.UserId equals ct.UserId
                          join cy in db.Roles on ct.RoleId equals cy.RoleId
                          join cx in db.Courses on item.CourseId equals cx.CourseId
                          join cz in db.Countries on c.CountryId equals cz.CountryId
                          join ca in db.States on c.StateId equals ca.StateId
                          join cb in db.Cities on c.CityId equals cb.CityId
                          where item.Email == Email
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
                              RoleName = cy.RoleName
                          }).ToList();


            return View(result);
        
            
        }

    }
}