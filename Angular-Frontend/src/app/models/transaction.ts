/* Transaction Model */
export class Transaction {
  private _id: string;
  private _accountNo: string;
  private _deposit: number;
  private _withdrawal: number;
  private _reference: string;
  private _date: Date;

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

  get Deposit(): number {
    return this._deposit;
  }
  set Deposit(value: number) {
    this._deposit = value;
  }

  get Withdrawal(): number {
    return this._withdrawal;
  }
  set Withdrawal(amount: number) {
    this._withdrawal = amount;
  }

  get Reference(): string {
    return this._reference;
  }
  set Reference(ref: string) {
    this._reference = ref;
  }

  get Date(): Date {
    return this._date;
  }
  set Date(dateIn: Date) {
    this._date = dateIn;
  }

  static mapResponseToTransaction(record: any): Transaction {
    let transaction = new Transaction();

    if (record) {
      transaction.Id = record.id;
      transaction.AccountNo = record.accountNo;
      transaction.Deposit = record.deposit;
      transaction.Withdrawal = record.withdrawal;
      transaction.Date = record.date;
      transaction.Reference = record.reference;
    }
    return transaction;
  }

  static mapResponseToTransactionList(recordSet: any[]): Transaction[] {
    let transactionsList: Transaction[] = [];

    if (recordSet) {
      recordSet.forEach(record => {
        let transaction = new Transaction();

        transaction.Id = record.id;
        transaction.AccountNo = record.accountNo;
        transaction.Deposit = record.deposit;
        transaction.Withdrawal = record.withdrawal;
        transaction.Date = record.date;
        transaction.Reference = record.reference;

        transactionsList.push(transaction);
      });
    }
    return transactionsList;
  }
}

