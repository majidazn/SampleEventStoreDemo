using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEventStoreDemo {
    public interface IEvent{
        Guid Id { get; }
    }
    public class AccountCreatedEvent : IEvent {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AccountCreatedEvent(Guid id, string name) {
            Id = id;
            Name = name;
        }
    }

    public class FundsDepositedEvent : IEvent {
        public Guid Id { get; private set; }
        public Decimal Amount { get;private set; }
        public FundsDepositedEvent(Guid id, decimal amount) {
            Id = id;
            Amount = amount;
        }
    }

    public class FundsWithdrawedEvent : IEvent {
        public Guid Id { get; private set; }
        public Decimal Amount { get; private set; }
        public FundsWithdrawedEvent(Guid id, decimal amount) {
            Id = id;
            Amount = amount;
        }
    }

}
