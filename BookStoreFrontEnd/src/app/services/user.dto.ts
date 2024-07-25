export class UserDTO {
    userId: number | undefined;
    username: string | undefined;
    email: string | undefined;
    roleId: number;
    password: string | undefined;
    passwordConfirm: string | undefined;
  
    constructor() {
      this.roleId = 2; // Default role id
    }
  }