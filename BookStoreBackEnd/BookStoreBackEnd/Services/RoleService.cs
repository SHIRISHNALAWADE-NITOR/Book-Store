using AutoMapper;
using Microsoft.EntityFrameworkCore;
public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    public RoleService(IMapper mapper,ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RoleDTO> AddRole(RoleDTO roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return _mapper.Map<RoleDTO>(role);
    }

    public async Task DeleteRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<RoleDTO>> GetAllRoles()
    {
        var roles = await _context.Roles.ToListAsync();
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }

    public async Task<RoleDTO> GetRoleById(int id)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        return _mapper.Map<RoleDTO>(role);
    }

    public async Task<RoleDTO> UpdateRole(RoleDTO roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return _mapper.Map<RoleDTO>(role);
    }

}
