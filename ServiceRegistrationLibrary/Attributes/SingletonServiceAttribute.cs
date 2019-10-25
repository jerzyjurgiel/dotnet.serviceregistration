using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRegistrationLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]

    public class SingletonServiceAttribute : Attribute
    {
    }
}
