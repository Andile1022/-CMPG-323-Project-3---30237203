using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.repository
{
    public class zonesRepo
    {
        private readonly ConnectedOfficeContext _context;

        public zonesRepo(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public Task<List<Zone>> getAll(){
            return _context.Zone.ToListAsync();
        }

        public Task<Zone> getZone(Guid? id){
            var category = _context.Zone.FirstOrDefaultAsync(m => m.ZoneId == id);
            return category;
        }

        public Task<Zone> getDetails(Guid? id){
            var category =  _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
            return category;
        }

        public async void addZone(Zone category){
            category.ZoneId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async void editZone(Guid id, Zone category){
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(category.ZoneId))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        public async void deleteZone(Guid? id){
             var category = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(category);
            await _context.SaveChangesAsync();
        }

        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }
    }
}