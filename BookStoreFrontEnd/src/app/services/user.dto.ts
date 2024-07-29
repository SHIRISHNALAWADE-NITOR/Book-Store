export class UserDTO {
  userId: number = 0; // Default to 0
  name?: string; // Ensure `name` is included
  dateOfBirth?: string;
  phoneNumber?: string;
  username?: string;
  email?: string;
  roleId: number = 2; // Default role id
  passwordHash?: string;
  passwordConfirm?: string;

  constructor() {
    this.roleId=2;
  }
}
