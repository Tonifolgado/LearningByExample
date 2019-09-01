using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    interface IPlugin
    {
        // A common interface that all plug-ins must implement.
        string Description { get; set; }
        void Start();
        void Stop();
    }
}
