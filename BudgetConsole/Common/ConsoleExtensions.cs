using System;
using System.Linq;
using System.Reflection;

namespace Willowcat.Common
{
    public static class ConsoleExtensions
    {
        public static T ParseArguments<T>(string argString) 
        {
            T Result = Activator.CreateInstance<T>();

            ConsoleArguments argList = new ConsoleArguments(argString);

            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttributes<ConsoleFlagAttribute>();
                foreach (ConsoleFlagAttribute attr in attrs)
                {
                    string propName = prop.Name;
                    string flagName = attr.FlagName;

                    ConsoleArgument arg = argList.FirstOrDefault(item => item.FlagName.Equals(flagName, StringComparison.OrdinalIgnoreCase));
                    if ( arg != null )
                    {
                        if (prop.GetType() == typeof(bool))
                        {
                            prop.SetValue(Result, true);
                        }
                        else
                        {
                            prop.SetValue(Result, arg.Value);
                        }
                    }
                }
            }

            return Result;
        }
    }
}
