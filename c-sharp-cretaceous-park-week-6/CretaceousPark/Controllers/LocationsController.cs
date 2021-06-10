using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CretaceousPark.Models;

namespace CretaceousPark.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationsController : ControllerBase
  {
    private readonly CretaceousParkContext _db;

    public LocationsController(CretaceousParkContext db)
    {
      _db = db;
    }

    // GET: api/Locations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> Get(string terrainType, string weather, string name)
    {
      var query = _db.Locations.AsQueryable();

      if (terrainType != null)
      {
        query = query.Where(entry => entry.TerrainType == terrainType);
      }

      if (weather != null)
      {
        query = query.Where(entry => entry.Weather == weather);
      }    

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }      

      return await query.ToListAsync();
    }

    // GET: api/Locations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocation(int id)
    {
        var location = await _db.Locations.FindAsync(id);

        if (location == null)
        {
            return NotFound();
        }

        return location;
    }

    // PUT: api/Locations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Location location)
    {
      if (id != location.LocationId)
      {
        return BadRequest();
      }

      _db.Entry(location).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Locations
    [HttpPost]
    public async Task<ActionResult<Location>> Post(Location location)
    {
      _db.Locations.Add(location);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetLocation), new { id = location.LocationId }, location);
    }

    // DELETE: api/Locations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
      var location = await _db.Locations.FindAsync(id);
      if (location == null)
      {
        return NotFound();
      }

      _db.Locations.Remove(location);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool LocationExists(int id)
    {
      return _db.Locations.Any(e => e.LocationId == id);
    }
  }
}