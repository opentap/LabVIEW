# LabVIEW Plugin

Allows OpenTAP to call .NET assemblies generated by the NI LabVIEW Application Builder module (generated assemblies wrap .vi files).

Requirements:
* LabVIEW Runtime that is compatible with generated assembly
* OpenTAP 9.x 


How it works
----------

VI's are exported using the Application Builder module in LabVIEW.
A type data provider (ITypeDataProvider/ITypeDataSearcher) finds DLLs which reference LabVIEW and search them for plugins.

The VI input(s) map to settings on the test step, while VI output(s) map to output settings.

VI classes and clusters can be passed between VI steps, but they can only be created from VI steps.

Use OpenTap.LabView.Test1.dll to test various functionality (_LabVIEW Examples plugin_)

# License
The LabVIEW Plugin is released under the MIT License. Feel free to use, modify, and distribute it in accordance with the terms of the license.