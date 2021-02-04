using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public string template = @"
                <ul style=""border: 1px solid pink;"">
                  {{#each doc}}
                    <li>{{docname}} - {{docpath}}</li>
                  {{/each}}
                </ul>
            ";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetJson()
        {
            return Json(new { doc = Enumerable.Range(1, 4).Select(i => new { docname = $"name{i}", docpath = $"http://somepath{i}" }) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplate()
        {

            return Json(new { template = template }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Rzr()
        {
            //if you go this way, you can save yourself an ajax call but the downsize is using razr.  if you arent relying on an in memory cache and are using something 
            //like a share or something else to store the templates, this may be the way to go.
            return View("rzr", new SomeModel() { Markup = template });
        }
        public ActionResult AllAjax()
        {
            //this implies that the client will access the template via javascript
            //i think if you had clients access a "template service" directly, that would be complicated and introduce security concerns
            //i would recommend if you go this approach, you have a template cache that gets refreshed via tempalate service and ajax call goes into
            //controller that reads the cache (local?) or if it (bxaccess web server) has a cache miss will access the template service
            return View("allajax");
        }
    }
}/*
example data model
{groups:
{GroupA:{{docname="1", doclink="2"},{docname="1", doclink="2"},{docname="1", doclink="2"}},
 GroupB:{{docname="1", doclink="2"},{docname="1", doclink="2"}}
}}
<table style=""border: 1px solid pink;"">
<tr>
                  {{#each groups}}
                  <td>
                    <ul>
                        <li>{{docname}} - {{docpath}}</li>
                    </ul>
                  </td>
                  {{/each}}
</tr>
                </ul>


