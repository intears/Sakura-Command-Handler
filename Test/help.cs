
using System;
using SakuraCommandHandler;
using SakuraCommandHandler.Parameter;



namespace Test
{
    // this is the command name and other params you give it, you can always add more to this in the actual source
        [Command("help", "a help command gives you a list of commands")]
        public class Help : CommandBase
        {

            public override async void Execute()
            {
                // get each command in the command handler
                foreach (var command in Client.CommandHandler.Commands.Values)
                {
                    // write the command name
                    Console.Write(command.Name);
                    // get each param in the command and write it with its name and description
                    foreach (var param in command.Parameters)
                    {
                        Console.Write($" -{param.Name} : {param.Desc}");
                    }
                    // add a new line
                    Console.WriteLine();
                }
            }
        }
}