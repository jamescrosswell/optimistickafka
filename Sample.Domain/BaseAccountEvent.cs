using Sample.Domain.Accounts;
using System;
using Writ.Messaging.Kafka.Events;

namespace Sample.Domain
{
    public abstract class BaseAccountEvent : IEvent<Account, Guid>
    {
        protected BaseAccountEvent(MessageOffset commandOffset, Guid id)
        {
            Id = id;
            CommandOffset = commandOffset ?? throw new ArgumentNullException(nameof(commandOffset));
        }

        public Guid Id { get; }

        /// <summary>
        /// Stores the offset of the command that produced this event. This can be
        /// used to track a high water mark for commands. 
        /// </summary>
        /// <remarks>
        /// Note that we would typically track high water marks when processing
        /// events rather than commands, since events might potentially be reprocessed
        /// (either to reinterpret these or because the application state is being
        /// held in ephemeral storage). This is not the case with commands as 
        /// reprocessing these would result in duplicate events!
        /// 
        /// For this reason, the command offset is recorded along with the event
        /// rather than with the original command.
        /// </remarks>
        public MessageOffset CommandOffset { get; }

        protected bool Equals(BaseAccountEvent other)
        {
            return Id.Equals(other.Id) && Equals(CommandOffset, other.CommandOffset);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseAccountEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ CommandOffset.GetHashCode();
            }
        }
    }
}