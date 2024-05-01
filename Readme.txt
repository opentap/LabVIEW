#Sample Labview VI project exported to .NET Interop assembly; Dependency/InteropAssembly.dll

#Labview RTE is mostly likely required to be installed. Dependency/NationalInstruments.LabVIEW.Interop.dll is copied from LV RTE 2024 version

#OpenTap 9.x is required to be installed


How it works
----------

VI functions are exported using the .NET export function in LabVIEW.
A type data provider (ITypeDataProvider/ITypeDataSearcher) finds DLLs which reference LabVIEw and search them for plugins. 

The input maps to settings on the test step while outputs map to output settings.

VI Classes and clusters can be passed between VI steps, but they can only be created from VI steps.

Use OpenTap.LabView.Test1.dll to test various functionality. VI files to be added.