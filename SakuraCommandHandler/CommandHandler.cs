using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SakuraCommandHandler
{
    public class CommandHandler
    {
        private readonly ConsoleClient _client;

        public string SwitchChar { get; private set; }
        public Dictionary<string, ConsoleCommand> Commands { get; private set; }
        internal CommandHandler(ConsoleClient client, string switchChar = "-")
        {
            Assembly assembly = Assembly.GetEntryAssembly(); // get the attributes in assembly to see collect commands
            _client = client;
            this.SwitchChar = switchChar;
            Commands = new Dictionary<string, ConsoleCommand>(); // create a dictionary to hold the commands

            if (assembly != null)
                foreach (var type in assembly.GetTypes()) // add each attr(command) to the dictionary
                {
                    if (typeof(CommandBase).IsAssignableFrom(type) &&
                        TryGetAttribute(type.GetCustomAttributes(), out CommandAttribute attr))
                        Commands.Add(attr.Name, new ConsoleCommand(type, attr));
                }
        }

        internal void Client_OnMessage(ConsoleClient client, MessageArgs args)
        {
            if (args.Message == null)
                return;
            List<string> arguemnts = new List<string>(); // get the switch amount
            List<string> parts = args.Message.Content.Split(' ').ToList();     // parts of the shit

            if (parts.Count > 1)
            {
                var nonParsedArgs = String.Join(" ", parts.Skip(1)).Substring(1); // (REMOVE)command -s:TSET -f -e:FALSE
                arguemnts = Regex.Split(nonParsedArgs, @"\ \" + SwitchChar).ToList(); // finds " <switchChar>" so < ->s:TSET< ->f< ->e:FALSE
            }



            if (Commands.TryGetValue(parts[0], out ConsoleCommand cmd))
            {
                parts.RemoveAt(0);

                CommandBase cmdBase = (CommandBase)Activator.CreateInstance(cmd.Type); // create a instance for teh cmd type
                cmdBase.Prepare(_client, args.Message); // pass the info

                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    if (arguemnts.Count > i) // there is more arguements i 
                    {
                        object value;
                        // for each param in the command, if its optional note it, if its required connect it
                        foreach (var param in arguemnts) // check if param that is listed in the command that was given
                        {
                            try
                            {
                                if (param.Split(':')[0] == cmd.Parameters[i].Name) // parameter was found
                                {
                                    // add argument to the command
                                    value = String.Join(":", param.Split(':').Skip(1));
                                    if (!cmd.Parameters[i].Property.PropertyType.IsAssignableFrom(value.GetType()))
                                        value = Convert.ChangeType(value, cmd.Parameters[i].Property.PropertyType);

                                    cmd.Parameters[i].Property.SetValue(cmdBase, value); // added argument to command
                                                                                         //arguemnts.Remove(param); // remove the arg from the list so we dont have to repeat it
                                    break;
                                }
                            }catch (Exception ex)
                            {
                                cmdBase.HandleError(cmd.Parameters[i].Name, String.Join(":", param.Split(':').Skip(1)), ex);
                            }
                        }
                    }
                    else if (cmd.Parameters[i].Optional)
                        break;
                    else
                    {
                        cmdBase.HandleError(cmd.Parameters[i].Name, null, new ArgumentNullException("Too few arguments provided"));

                        return;
                    }
                }

                cmdBase.Execute();
            }

        }
        internal static bool TryGetAttribute<TAttr>(IEnumerable<object> attributes, out TAttr attr) where TAttr : Attribute // get the attr info 
        {
            foreach (var attribute in attributes)
            {
                if (attribute.GetType() == typeof(TAttr))
                {
                    attr = (TAttr)attribute;
                    return true;
                }
            }

            attr = null;
            return false;
        }
    }
}
