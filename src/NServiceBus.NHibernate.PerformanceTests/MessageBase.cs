namespace Runner
{
    using System;

    using NServiceBus_6;

    [Serializable]
    public class MessageBase : IMessage
    {
        public int Id { get; set; }
        public bool TwoPhaseCommit { get; set; }
    }
}