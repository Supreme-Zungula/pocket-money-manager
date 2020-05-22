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

  get FamilyId(): number {
    return this._familyId;
  }
  set FamilyId(value: number) {
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

  static mapResponseToUser(record: any) {
    let user: User = new User();

    if (record) {
      user.Id = record.id;
      user.FamilyId = record.familyId;
      user.FirstName = record.firstName;
      user.LastName = record.lastName;
      user.Role = record.role;
      user.Phone = record.phone;
      user.Password = record.password;
    }
    return user;
  }

  static mapResponseToUsers(recordsSet: any[]): User[] {
    let usersResponse: User[] = [];
    if (recordsSet) {
      recordsSet.forEach(record => {
        let user: User = new User();
        user.Id = record.id;
        user.FamilyId = record.familyId;
        user.FirstName = record.firstName;
        user.LastName = record.lastName;
        user.Role = record.role;
        user.Phone = record.phone;
        user.Password = record.password;
        usersResponse.push(user);

      });
    }
    return usersResponse;
  }
}
