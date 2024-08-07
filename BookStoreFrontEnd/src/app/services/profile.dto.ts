export class UserDTO {
    userId: number = 0;
    firstName?: string; // Optional fields, adjust as needed
    lastName?: string;
    dateOfBirth?: string;
    phoneNumber?: string;
    username?: string;
    email?: string;
    roleId: number = 2;
    passwordHash?: string;
    passwordConfirm?: string;
    address?: {
      street: string;
      city: string;
      state: string;
      postalCode: string;
      country: string;
    };
    photoUrl?: "https://www.istockphoto.com/photos/user-profile-image"; // Include if necessary
  }
  