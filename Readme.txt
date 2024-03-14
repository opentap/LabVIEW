#Sample Labview VI project exported to .NET Interop assembly; Dependency/InteropAssembly.dll

#Labview RTE is mostly likely required to be installed. Dependency/NationalInstruments.LabVIEW.Interop.dll is copied from LV RTE 2024 version

#OpenTap 9.x is required to be installed


How it works
----------

VI functions are exported using the .NET export function in LabVIEW.
A type data provider (ITypeDataProvider/ITypeDataSearcher) finds DLLs which reference LabVIEw and search them for plugins. 

The input maps to settings on the test step while outputs map to output settings.

Classes in LabView maps to a resource while functions on those classes map to test steps which take them as input (and return the modified class as output).

We still dont understand fully how classes in LabView maps to OpenTAP, but I believe we are well under way.

When classes are defined they should define a vi function with the same name as the class. That functions should return the class and work as a kind of constructor.

Use OpenTap.LabView.Test1.dll to test various functionality. VI files to be added.