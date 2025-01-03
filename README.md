# LabVIEW Plugin

Allows OpenTAP to call .NET assemblies generated by the National Instruments (NI) LabVIEW Application Builder (generated assemblies wrap .vi files).

Requirements:
* LabVIEW Runtime compatible with generated assembly
* OpenTAP 9.x 


How it works
----------

VI files are exported using the Application Builder module in LabVIEW.
A type data provider (ITypeDataProvider/ITypeDataSearcher) finds DLLs which reference LabVIEW and searches them for plugins.

The VI inputs map to settings on the OpenTAP test step, while VI outputs map to output settings.

VI classes and clusters can be passed between VI steps, but they can only be created from VI steps.

Use OpenTap.LabView.Test1.dll to test various functionality (_LabVIEW Examples plugin_)

# License
The LabVIEW Plugin is released under the [MIT License](https://opensource.org/license/mit). Feel free to use, modify, and distribute it in accordance with the terms of that license.