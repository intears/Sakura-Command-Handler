using SakuraCommandHandler;

namespace Test
{
    internal class Program
    {
        
        public static void Main(string[] args)
        {
            // create your client so you can setup the loop and plugin search
            // @switchChar {string} is that variable that is used to separate arguments
            ConsoleClient client = new ConsoleClient("-");
            
            // start the reading of the console,
            // this will allow the client to get input to use for the commands
            client.ConsoleStart();
            
            // the next step is to make commands in your application
            // I will make a simple test command and a help command
            // test.cs will be my test command location
        }
    }
}