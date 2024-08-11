public static class DataInitializer
{
    public static void Run(ApplicationDbContext db)
    {
        if (db.Users.Any()) return;

        // create role
        var adminRole = new Role
        {
            RoleName = "ADMIN"
        };
        db.Roles.Add(adminRole);

        var userRole = new Role
        {
            RoleName = "USER"
        };
        db.Roles.Add(userRole);

        db.SaveChanges();


        // create privileges
        db.RolePrivilege.Add(new RolePrivilege
        {
            RoleId = adminRole.RoleId,
            Privilege = PrivilegeConst.ReadUser
        });

        db.RolePrivilege.Add(new RolePrivilege
        {
            RoleId = adminRole.RoleId,
            Privilege = PrivilegeConst.CreateUser
        });

        db.RolePrivilege.Add(new RolePrivilege
        {
            RoleId = adminRole.RoleId,
            Privilege = PrivilegeConst.DeleteBook
        });

        db.RolePrivilege.Add(new RolePrivilege
        {
            RoleId = userRole.RoleId,
            Privilege = PrivilegeConst.ReadUser
        });

        db.SaveChanges();


        // create user
        //var joanna = new User
        //{
        //    Username = "joanna",
        //    Password = "joanna",
        //    RoleId = adminRole.Id
        //};
        //db.User.Add(joanna);

        //var natasha = new User
        //{
        //    Username = "natasha",
        //    Password = "natasha",
        //    RoleId = operatorRole.Id
        //};
        //db.User.Add(natasha);

        db.SaveChanges();
    }
}
