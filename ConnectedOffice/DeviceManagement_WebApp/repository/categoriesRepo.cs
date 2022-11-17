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
    public class categoriesRepo
    {
        private readonly ConnectedOfficeContext _context;

        public categoriesRepo(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public Task<List<Category>> getAll(){
            return _context.Category.ToListAsync();
        }

        public Task<Category> getCategory(Guid? id){
            var category = _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
            return category;
        }

        public Task<Category> getDetails(Guid? id){
            var category =  _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            return category;
        }

        public async void addCategory(Category category){
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async void editcategory(Guid id, Category category){
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        public async void deleteCategory(Guid? id){
             var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}