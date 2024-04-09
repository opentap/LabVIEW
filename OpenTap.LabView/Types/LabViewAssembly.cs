using System;
using System.Collections.Generic;
using System.Linq;
namespace OpenTap.LabView.Types
{
    /// <summary>   This class represent a LabView assembly.  </summary>
    class LabViewAssembly : ITypeDataSource
    {
        internal readonly HashSet<LabViewTypeData> LabViewTypes = new HashSet<LabViewTypeData>();
        
        internal LabViewAssembly(AssemblyData asm2)
        {

            Name = $"LabView: {asm2.Name}";
            Location = asm2.Location;
            References = asm2.References.ToArray();
            Version = ((ITypeDataSource)asm2).Version;
        }
        
        /// <summary> The name of the assembly. </summary>
        public string Name { get; }
        /// <summary> The location of the assembly.  </summary>
        public string Location { get; }
        /// <summary> The types in the assembly. </summary>
        public IEnumerable<ITypeData> Types => LabViewTypes.Cast<ITypeData>();
        /// <summary> The attributes of the assembly. </summary>
        public IEnumerable<object> Attributes => Array.Empty<object>();
        /// <summary> The references of the assembly. </summary>
        public IEnumerable<ITypeDataSource> References { get; }
        /// <summary> The version of the assembly. </summary>
        public string Version { get; }
        
        /// <summary>   Returns a string that represents the current object. </summary>
        public override string ToString() => $"LabView: {Location}";
        
        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        public override bool Equals(object obj)
        {
                return obj is LabViewAssembly other && other.Location == Location;   
        }
        
        /// <summary>   Serves as the default hash function. </summary>
        public override int GetHashCode()
        {
            return (Location?.GetHashCode() ?? 0) * 17 + 23;
        }
    }
}
