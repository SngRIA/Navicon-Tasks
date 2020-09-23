using System;

namespace TaskLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NonSerializedAttribute : Attribute
    {
        public NonSerializedAttribute() { }
    }
}
