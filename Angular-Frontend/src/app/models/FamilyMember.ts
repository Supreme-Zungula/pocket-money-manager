export class FamilyMember {
    private _id: string;
    private _familyId: number;
    private _firstName: string;
    private _lastName: string;
    private _relation: string;
    private _phone: string;

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

    get Relationship(): string {
        return this._relation;
    }
    set Relationship(value: string) {
        this._relation = value;
    }

    get Phone(): string {
        return this._phone;
    }
    set Phone(phoneNumber: string) {
        this._phone = phoneNumber;
    }

    static mapResponseToFamilyMember(record: any) {
        let member: FamilyMember;

        if (record) {
            member = new FamilyMember();
            member.Id = record.id;
            member.FamilyId = record.familyId;
            member.FirstName = record.firstName;
            member.LastName = record.lastName;
            member.Relationship = record.relationship;
            member.Phone = record.phone;
        }
        return member;
    }

    static mapResponseToFamilyMembersList(recordsSet: any): FamilyMember[] {
        let membersResponse: FamilyMember[] = [];
        if (recordsSet) {
            recordsSet.forEach(record => {
                let member: FamilyMember = new FamilyMember();
                member.Id = record.id;
                member.FamilyId = record.familyId;
                member.FirstName = record.firstName;
                member.LastName = record.lastName;
                member.Relationship = record.relationship;
                member.Phone = record.phone;
                membersResponse.push(member);

            });
        }
        return membersResponse;
    }
}
