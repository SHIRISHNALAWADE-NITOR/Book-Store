using BookStoreBackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    private readonly EcommerceDbContext _context;

    public UserController(EcommerceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.Include(u => u.Addresses).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, user);
    }
}
