
using System;
using SakuraCommandHandler;
using SakuraCommandHandler.Parameter;


namespace Test
{
    // this is the command name and other params you give it, you can always add more to this in the actual source
    [Command("test", "this is the description")]
    public class Test : CommandBase
    {
        
        // these are parameters that you can add to the command, the second item that is passed in is a bool for
        // optionality so it doesnt need to always have all 
        // MAKE SURE THAT THE OPTIONAL PARAMETERS AND AT THE END
        // this is required parameters
        [Parameter("m", "the message that is shown")]
        public string MessageText { get; set; }
        
        [Parameter("s", "show the command", true)]
        public bool Show { get; set; }
        
        public override async void Execute()
        {
            Console.WriteLine("there was a test message");
            if (Show == true)
                Console.WriteLine($"the message was: \"{@MessageText}\"");
        }
    }
}