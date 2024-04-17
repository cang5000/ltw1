using Microsoft.EntityFrameworkCore;
using WebBanQuanAo.Data;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Repository
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(x => x.Products).ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
