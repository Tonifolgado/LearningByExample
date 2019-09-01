using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    // A simple IPlugin implementation to demonstrate the PluginFactory class.
    public class SimplePlugin : AbstractPlugin
    {
        // Implement Start method.
        public override void Start()
        {
            Console.WriteLine(Description + ": Starting...");
        }
        // Implement Stop method.
        public override void Stop()
        {
            Console.WriteLine(Description + ": Stopping...");
        }
    }
}
