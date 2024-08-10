public interface IRoleService
{
    Task<IEnumerable<RoleDTO>> GetAllRoles();
    Task<RoleDTO> GetRoleById(int id);
    Task<Role> RoleById(int id);
    Task<RoleDTO> AddRole(RoleDTO roleDto);
    Task<RoleDTO> UpdateRole(RoleDTO roleDto);
    Task<bool> DeleteRole(int id);
}
