using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.repository;

namespace DeviceManagement_WebApp.repository
{
    public class devicesRepo
    {
        private readonly ConnectedOfficeContext _context;

        public devicesRepo(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public Task<List<Device>> getAll(){
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return connectedOfficeContext.ToListAsync();
        }

        public Task<Device> getDevice(Guid? id){
            var category = _context.Device.FirstOrDefaultAsync(m => m.DeviceId == id);
            return category;
        }

        public Task<Device> getDetails(Guid? id){
            var category =  _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            return category;
        }

        public async void addDevice(Device device){
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
        }

        public async void editDevice(Guid id, Device device){
            try
            {
                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        public Task<Device> getDeviceToDelete(Guid? id){
            return _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
        }

        public async void deleteDevice(Guid id){
             var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
   
    }
}