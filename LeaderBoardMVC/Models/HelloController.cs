using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LeaderBoardMVC.Controllers;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaderBoardMVC.Models
{
    // RESTful service
    public class HelloController : ApiController                // is a API Controller
    {
        private ScoreDB db = new ScoreDB();

        // GET /api/Hello/Gary or /api/Hello?name=Gary
        [HttpGet]
        public IHttpActionResult GetHelloGreeting()               // GET
        {
           // return Ok("Hello there Ian, welcome to the ASP.Net Web API");      // 200 OK
            return Ok(db.Scores.OrderByDescending(s => s.Score).ToList());
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterViewModel model)
        {
            Debug.WriteLine("**************************Start Method****************************");
            Debug.WriteLine("EMAIL: " + model.Email + "PASSWORD: " + model.Password);
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var newUser = new ApplicationUser() { Email = model.Email, UserName = model.Email };

                Debug.WriteLine("**************************Model Valid****************************");
                //Debug.WriteLine("Name: " + model.Email + "\nPassword: " + model.Password);
                var createUser = UserManager.Create(newUser, model.Password);

                if (createUser.Succeeded)
                {
                    Debug.WriteLine("**************************SUCCESS!!!****************************");
                    return Ok("Success");
                }
                

            }

            /*foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var errorMessage = error.ErrorMessage;
                    var key = modelStateKey;
                    Debug.WriteLine(errorMessage);
                    Debug.WriteLine(key);
                }
            }*/

            var modelStateList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            Dictionary<string, string> errorList = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string[]> kvp in modelStateList)
            {
                Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value[0]);
                if (kvp.Key.Length > 0)
                {
                    int i = kvp.Key.IndexOf(".") + 1;
                    string key = kvp.Key.Substring(i);
                    Debug.WriteLine(key);
                    errorList.Add(key, kvp.Value[0]);
                    //modelStateList.Remove(kvp.Key);
                }
                Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value[0]);
            }

            //var errors = new ScoresController().MyDictionaryToJson(errorList);
            
            return Json(errorList);
        }

        public async System.Threading.Tasks.Task<IHttpActionResult> LoginAsync(FormModel form)
        {
            LoginViewModel login = new LoginViewModel();
            login.Email = form.Email;
            login.Password = form.Password;
            login.RememberMe = false;
            AccountController ac = new AccountController();
            bool valid = await ac.Login(login);
            if (valid == true)
            {
                ScoreModel scores = new ScoreModel();
                scores.Name = form.Email;
                scores.Score = form.Score;
                db.Scores.Add(scores);
                db.SaveChanges();
                ac.LogOff();

                return Ok("Score added");
            }

            return BadRequest("Invalid");
        }

        public IHttpActionResult AddScore()
        {


            return Ok();
        }

        // return data serialised as XML or JSON or GSON depending on Accept header
        // sample URLs to invoke
        // http://localhost:1107/api/Hello/Gary or http://localhost:1107/api/Hello?name=Gary

        // stateless, new controller instance constructed to handle each request
    }
}
