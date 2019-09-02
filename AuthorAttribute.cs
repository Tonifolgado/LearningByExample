using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningByExample1
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly,
             AllowMultiple = true, Inherited = false)]

public class AuthorAttribute : System.Attribute
    {
        private string company; // Creator's company
        private string name;    // Creator's name
        // Declare a public constructor.
        public AuthorAttribute(string name)
        {
            this.name = name;
            company = "";
        }
        // Declare a property to get/set the company field.
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        // Declare a property to get the internal field.
        public string Name
        {
            get { return name; }
        }
    }

    /*
     The following example demonstrates how to decorate types 
     with AuthorAttribute:
    // Declare Allen as the assembly author. Assembly attributes
    // must be declared after using statements but before any other.
    // Author name is a positional parameter.
    // Company name is a named parameter.
    [assembly: Apress.VisualCSharpRecipes.Chapter03.Author("Allen",
    Company = "Apress")]
    namespace Apress.VisualCSharpRecipes.Chapter03
    {
    // Declare a class authored by Allen.
    [Author("Allen", Company = "Apress")]
    public class SomeClass
    {
        // Class implementation.
    }
    
    // Declare a class authored by Lena.
    [Author("Lena")]
    public class SomeOtherClass
    {
        // Class implementation.
    }
    }
     */

}
