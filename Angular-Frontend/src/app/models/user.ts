export class User {
  private _id: string;
  private _familyId: number;
  private _firstName: string;
  private _lastName: string;
  private _role: string;
  private _phone: string;
  private _password: string;

  set Id(value: string) {
    this._id = value;
  }
  get Id(): string {
    return this._id;
  }

  get FamilyID(): number {
    return this._familyId;
  }
  set FamilyID(value: number) {
    this._familyId = value;
  }

  get FirstName(): string {
    return this._firstName;
  }
  set FirstName(value: string) {
    this._firstName = value;
  }

  get LastName(): string {
    return this._lastName;
  }
  set LastName(surname: string) {
    this._lastName = surname;
  }

  get Role(): string {
    return this._role;
  }
  set Role(value: string) {
    this._role = value;
  }

  get Phone(): string {
    return this._phone;
  }
  set Phone(phoneNumber: string) {
    this._phone = phoneNumber;
  }

  get Password(): string {
    return this._password;
  }
  set Password(pwd: string) {
    this._password = pwd;
  }

  static mapResponseToUser(recordsSet: any, userId: string) {
    let user: User = new User();

    if (recordsSet) {
      recordsSet.forEach(record => {
        record.forEach(item => {
          if (item.Id === userId) {
            user.Id = item.Id;
            user.FamilyID = item.FamilyID;
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.Phone = item.Phone;
            user.Role = item.Role;
            user.Password = item.Password;
          }
        });
      });
    }
    return user;
  }

  static mapResponseToUsers(recordsSet: any[]): User[] {
    let usersResponse: User[] = [];
    if (recordsSet) {
      recordsSet.forEach(record => {
        let user: User = new User();
        user.Id = record.Id;
        user.FamilyID = record.FamilyID;
        user.FirstName = record.FirstName;
        user.LastName = record.LastName;
        user.Role = record.Role;
        user.Phone = record.phone;
        user.Password = record.Password;
        usersResponse.push(user);

      });
    }
    return usersResponse;
  }
}
