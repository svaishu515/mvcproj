using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using mvcproj.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace mvcproj.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            List<HomeModel> employee = new List<HomeModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            employee = JsonConvert.DeserializeObject<List<HomeModel>>(readWrite.Read("employee.json", "data"));
            return View(employee);

        }


        public IActionResult Add(HomeModel personModel)
            {
                List<HomeModel> employee = new List<HomeModel>();
                JSONReadWrite readWrite = new JSONReadWrite();
                employee = JsonConvert.DeserializeObject<List<HomeModel>>(readWrite.Read("employee.json", "data"));
                HomeModel person = employee.FirstOrDefault(x => x.Id == personModel.Id);
                if (person == null)
                {
                    employee.Add(personModel);
                }
                else
                {
                    ViewData["Message"] = "Modified data of employee.";
                    int index = employee.FindIndex(x => x.Id == personModel.Id);
                    employee[index] = personModel;
                }
                string jSONString = JsonConvert.SerializeObject(employee);
                readWrite.Write("employee.json", "data", jSONString);
                return RedirectToAction("Index", "Home");
            }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
            List<HomeModel> employee = new List<HomeModel>();
            JSONReadWrite readWrite = new JSONReadWrite();
            employee = JsonConvert.DeserializeObject<List<HomeModel>>(readWrite.Read("employee.json", "data"));
            int index = employee.FindIndex(x => x.Id == id);
            employee.RemoveAt(index);
            string jSONString = JsonConvert.SerializeObject(employee);
            readWrite.Write("employee.json", "data", jSONString);
            return RedirectToAction("Index", "Home");
        }


        public class JSONReadWrite
        {
            public JSONReadWrite() { }

            public string Read(string fileName, string location)
            {
                string root = "wwwroot";
                var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

                string jsonResult;

                using (StreamReader streamReader = new StreamReader(path))
                {
                    jsonResult = streamReader.ReadToEnd();
                }
                return jsonResult;
            }

            public void Write(string fileName, string location, string jSONString)
            {
                string root = "wwwroot";
                var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(jSONString);
                }
            }
        }
    }
}
