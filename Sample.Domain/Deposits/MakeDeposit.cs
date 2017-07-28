using Newtonsoft.Json;
using Sample.Domain.Accounts;
using System;

namespace Sample.Domain.Deposits
{
    public class MakeDeposit : BaseCommand<Account, Guid>
    {
        public int Amount { get; }

        [JsonConstructor]
        public MakeDeposit(Guid id, int amount)
            : base(id)
        {
            Amount = amount;
        }
    }
}