using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEventStoreDemo {
    public class BankAccount {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<Transaction> Transactions = new List<Transaction>();
        public BankAccount() {

        }
        public void Apply(AccountCreatedEvent @event) {
            Id=@event.Id;
            Name=@event.Name;
            CurrentBalance = 0;
        }

        public void Apply(FundsDepositedEvent @event) {
            var newTransaction= new Transaction {Id= @event.Id,Amount=@event.Amount};
            Transactions.Add(newTransaction);
            CurrentBalance += @event.Amount;
        }

        public void Apply(FundsWithdrawedEvent @event) {
            var newTransaction = new Transaction { Id = @event.Id, Amount = @event.Amount };
            Transactions.Add(newTransaction);
            CurrentBalance -= @event.Amount;
        }

    }
    public class Transaction {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
