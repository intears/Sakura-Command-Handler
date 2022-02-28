using System;

namespace SakuraCommandHandler
{
    public class ConsoleClient
    {
        private ConsoleClient _client = null;


        /// <summary>
        /// Create a console client
        /// </summary>
        /// <param name="switchChar">Get the switch char</param>
        public ConsoleClient(string switchChar = "-")
        {
            _client = this;
            _client.CommandHandler = new CommandHandler(this, switchChar);
        }
        public bool GetInput { get; set; } = false;
        public  CommandHandler CommandHandler { get; set; }
        

        /// <summary>
        /// Start the console
        /// </summary>
        public void ConsoleStart()
        {
            GetInput = true;
            while (GetInput)
            {
                var input = Console.ReadLine();
                if (input == null) { return; }
                ConsoleMessage consoleMessage = new ConsoleMessage() { Content=input };
                MessageArgs messageArgs = new MessageArgs { Message = consoleMessage };
                CommandHandler.Client_OnMessage(_client, messageArgs);
            }
        }
    }
}
