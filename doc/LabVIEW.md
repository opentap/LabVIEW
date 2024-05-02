# LabVIEW

Integrate LabVIEW code into OpenTAP.

LabVIEW is a programming language and execution environment for developing programs in a visual manner. 

A .vi-file is a file containing LabVIEW code. Each file contains one functional unit - a function or a "vi".

LabVIEW is a data-flow programming languages where each unit has inputs and outputs. They can be executed in parallel or sequentially based on need. 


## LabVIEW OpenTAP Integration
The LabVIEW OpenTAP plugin makes it is possible to call into 'vi's from OpenTAP. This is done by making a mapping of vi's into OpenTAP test steps. 

Since vi's has inputs and outputs, this makes the conversion fairly simple. Below you can see an example of adding two numbers together in LabVIEW and how that looks inside OpenTAP.

![Add in LabVIEW](images/add-definition.png)

![LabVIEW Add in OpenTAP](images/Add_In_OpenTAP.png)

Many more examples like this can be found inside the LabVIEW Examples Package.

## Getting Started

The first step in getting your LabVIEW project to run  inside OpenTAP is to use the LabVIEW Application Builder plugin to build your project into a .NET DLL.

This is done by building adding a build specification targeting a .NET Interop Assembly as seen below.

![Adding_A_Build_Specification.png](images/Adding_A_Build_Specification.png)

This will open a dialog for configuring your build specification. We recommend giving fitting names to the items inside this dialog, but do not change the .NET interop assembly class name. This must be `LabVIEWExports`.

Take note of the destination directory, for an easy deployment, you can set this to be your TAP installation folder or somewhere inside `[TapInstallation]/Packages/[MyProjectName]`.

![Assembly_Build_Properties.png](images/Assembly_Build_Properties.png)

Most other categories can be ignored, but the crucial part is specifying the Source Files as seen below. Here you must export all the vi's that you are interested in calling directly into from OpenTAP.

![Source_Files_Specification.png](images/Source_Files_Specification.png)


## Sharing Plugins

