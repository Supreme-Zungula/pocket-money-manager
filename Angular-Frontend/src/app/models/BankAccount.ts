/* BankAccount model class */
export class BankAccount {
    private _id: string;
    private _accountNo: string;
    private _balace: number;
    private _customerRef: string;

    get Id(): string {
        return this._id;
    }
    set Id(value: string) {
        this._id = value;
    }

    get AccountNo(): string {
        return this._accountNo;
    }
    set AccountNo(value: string) {
        this._accountNo = value;
    }

    get Balance(): number {
        return this._balace;
    }
    set Balance(value: number) {
        this._balace = value;
    }

    get CustomerRef(): string {
        return this._customerRef;
    }
    set CustomerRef(ref: string) {
        this._customerRef = ref;
    }


    static mapResponseToBankAccount(record: any): BankAccount {
        let account: BankAccount;

        if (record) {
            account = new BankAccount();
            account.Id = record.id;
            account.AccountNo = record.accountNo;
            account.Balance = record.balance;
            account.CustomerRef = record.customerRef;
        }
        return account;
    }

    static mapResponseToBankAccountList(recordSet: any[]): BankAccount[] {
        let accountsList: BankAccount[] = [];

        if (recordSet) {
            recordSet.forEach(record => {
                let account = new BankAccount();
                account.Id = record.id;
                account.AccountNo = record.accountNo;
                account.Balance = record.balance;
                account.CustomerRef = record.customerRef;
                accountsList.push(account);
            });

        }
        return accountsList;
    }
}