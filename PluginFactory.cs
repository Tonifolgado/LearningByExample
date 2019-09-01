using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    // A factory to instantiate instances of IPlugin.
    public sealed class PluginFactory
    {
        static IPlugin CreatePlugin(string assembly,
            string pluginName, string description)
        {
            // Obtain the Type for the specified plug-in.
            Type type = Type.GetType(pluginName + ", " + assembly);
            // Obtain the ConstructorInfo object.
            ConstructorInfo cInfo = type.GetConstructor(Type.EmptyTypes);
            // Create the object and cast it to StringBuilder.
            IPlugin plugin = cInfo.Invoke(null) as IPlugin;
            // Configure the new IPlugin.
            plugin.Description = description;
            return plugin;
        }
    }
}
