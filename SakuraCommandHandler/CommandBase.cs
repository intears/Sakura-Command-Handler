using System;

namespace SakuraCommandHandler
{
    public abstract class CommandBase
    {
        public ConsoleClient Client { get; private set; }
        public ConsoleMessage Message { get; private set; }

        internal void Prepare(ConsoleClient client, ConsoleMessage message)
        {
            Client = client;
            Message = message;
        }

        public abstract void Execute();
        public virtual void HandleError(string parameterName, string providedValue, Exception exception) { }
    }
}
