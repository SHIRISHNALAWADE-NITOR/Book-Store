using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;

    public RoleService(IMapper mapper, ApplicationDbContext context, INotificationService notificationService)
    {
        _mapper = mapper;
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<RoleDTO> AddRole(RoleDTO roleDto)
    {
        try
        {
            var role = _mapper.Map<Role>(roleDto);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(role);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the role.", ex);
        }
    }

    public async Task<bool> DeleteRole(int id)
    {
        try
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the role.", ex);
        }
    }

    public async Task<IEnumerable<RoleDTO>> GetAllRoles()
    {
        try
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving roles.", ex);
        }
    }

    public async Task<RoleDTO> GetRoleById(int id)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            return _mapper.Map<RoleDTO>(role);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the role.", ex);
        }
    }

    public async Task<Role> RoleById(int id)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            return role;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the role.", ex);
        }
    }

    public async Task<RoleDTO> UpdateRole(RoleDTO roleDto)
    {
        try
        {
            var role = _mapper.Map<Role>(roleDto);
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(role);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the role.", ex);
        }
    }
}
