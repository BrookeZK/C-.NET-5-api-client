using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CretaceousClient.Models;

namespace CretaceousClient.Controllers
{
  public class LocationsController : Controller
  {
    public IActionResult Index()
    {
      var allLocations = Location.GetLocations();
      return View(allLocations);
    }

    [HttpPost]
    public IActionResult Index(Location location)
    {
      Location.Post(location);
      return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
      var location = Location.GetDetails(id);
      return View(location);
    }

    public IActionResult Edit(int id)
    {
      var location = Location.GetDetails(id);
      return View(location);
    }

    [HttpPost]
    public IActionResult Details(int id, Location location)
    {
      location.LocationId = id;
      Location.Put(location);
      return RedirectToAction("Details", id);
    }

    public IActionResult Delete(int id)
    {
      Animal.Delete(id);
      return RedirectToAction("Index");
    }
  }
}