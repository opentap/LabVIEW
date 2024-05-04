# OpenTAP And LabVIEW

This document describes how to use the LabVIEW OpenTAP plugin.

LabVIEW is a programming language and execution environment made by National Instruments for developing programs in a visual manner. 

A LabVIEW program consists of .vi-files containing LabVIEW code. Each file contains one functional unit - a VI function or a "vi". Hereafter these will be referred to as vi's.

LabVIEW is a data-flow programming languages where each vi has inputs and outputs. They can be executed in parallel or sequentially based on need. 

The LabVIEW OpenTAP plugin makes it is possible to call into vi's from OpenTAP. This is achieved by mapping each vi to an OpenTAP test step. 

Since a vi's has inputs and outputs and a vi function call takes those input and emits the outputs as results,
 they are analogous to how many OpenTAP test step works and this makes the conversion fairly simple. 
Below you can see an example of adding two numbers together in LabVIEW and how that looks inside OpenTAP.

![Add in LabVIEW](images/add-definition.png)

![LabVIEW Add in OpenTAP](images/Add_In_OpenTAP.png)

Many more examples like this can be found inside the LabVIEW Examples Package.

## Getting Started

The first step in getting your LabVIEW project to run inside OpenTAP is to use the LabVIEW Application Builder plugin to build your project into a .NET DLL.

This is done by building adding a build specification targeting a .NET Interop Assembly as seen below.

![Adding A Build Specification](images/Adding_A_Build_Specification.png)

This will open a dialog for configuring your build specification. We recommend giving fitting names to the items inside this dialog, but do not change the .NET interop assembly class name. This must be `LabVIEWExports`.

Take note of the destination directory, for an easy deployment, you can set this to be your TAP installation folder or somewhere inside `[TapInstallation]/Packages/[MyProjectName]`.

![Assembly Build Properties.png](images/Assembly_Build_Properties.png)

Most other categories can be ignored, but the crucial part is specifying the Source Files as seen below. Here you must export all the vi's that you are interested in calling directly into from OpenTAP.

![Source Files Specification.png](images/Source_Files_Specification.png)

## Results and Logging

Sometimes it is wanted to store inputs or outputs as results or writing them to the log.

To save them as results, select the settings in the Publish Result drop-down under test step settings. 
To write the values to the log, select the Log Inputs and Outputs.

![Selecting Results](images/SelectResults.png)

The results will be stored under a result name matching the name of the test step and column names matching the names of the selected settings.
For more information about results see the [OpenTAP Developer Guide](https://doc.opentap.io/Developer%20Guide/Test%20Step/#publishing-results).

## Improving Usability with Annotations

Extra information can be added when building the wrapper DLL. This information can be used to provide extra information to the user using the test step.

The types of information that can be included are:

- Display Name - A user-friendly name that can be any string - including using the OpenTAP name formatting.
- Description - Adds a description to a test step or setting (tooltip).
- Unit - For numbers only. Adds a unit to a number.

Refer to the image below for a practical example showcasing these annotations.

![Improved Usability with Annotations](images/AnnotationsUsability.png)

To add this information, use the Define Prototype dialog inside the LabVIEW project build specifications as seen below:

![Annotate in Define Prototype](images/AnnotateInDefinePrototype.png)

In the Define Prototype dialog, the normal VI documentation can be used as-is.  In that case it will be used as the test step description. A practical example of that can be seen below:

![Default Documentation](images/DefaultDocumentation.png)

To provide a higher degree of documentation, it can be written in YAML format inside the Custom Documentation in the Define Prototype dialog.

In the InputOutputAnnotation VI, the following YAML text defines the documentation:
```yaml
Display: Input Output Annotations #The test step name you want to show the user.
Description: This test step demonstrates how annotations can work to improve usability of a test step.
Input1:
   Display: Input 1 # The setting name you want the show the user.
   Description: This setting demonstrates how the "display name" and unit can be set.  # This turns into a tooltip on the setting. 
   Unit: s  # The unit of the value.
Output1:
  Display: Output 1
  Description: This setting just takes the value of Input 1 and outputs it from the VI.
  Unit: s
```

Each input or output that should be described is referenced by the name of the exported setting. 
In the example `Input1` gets referenced and then described  

The information given here is encoded into an XML file alongside the dll file created by Application Builder.


## Sharing Plugins
LabVIEW-based plugins can be shared and deployed just as regular OpenTAP plugins can.
To do this, first create a package.xml file. These are described in detail in the [OpenTAP Developer Guide](https://doc.opentap.io/Developer%20Guide/Plugin%20Packaging%20and%20Versioning/#plugin-packaging-and-versioning).

Below is an example of how this could look like:
```xml
<Package Name="My LabView Plugin"
         xmlns="http://opentap.io/schemas/package"
         InfoLink="https://doc.opentap.io/"
         Version="0.1.0-alpha"
         Tags="LabVIEW">
  <Description>My LabVIEW plugin containing my LabVIEW code..</Description>
  <Files>
    <File Path="Packages/My LabVIEW Plugin/MyLabViewPlugin.dll">
    </File>
   <File Path="Packages/My LabVIEW Plugin/MyLabViewPlugin.xml">
   </File>
  </Files>
</Package>
```

You can then build the plugin package from the command line as such:
```shell
tap package create ./package.xml
``` 
This will emit a file called `My LabVIEW Plugin.TapPackage`. This package file can be installed on other OpenTAP installations and as long as the prerequisites are installed work just as it did on your computer.


## Supported VI Types

Not every type that can be used as an input or output in LabVIEW works well when exported to a .NET DLL.
Hence only a subset of LabVIEW types of inputs can be effectively exported.

The below list describes the currently well working exported types.

| Type Name                | Supported | Notes                                                                                                        |
|--------------------------|-----------------------------------|--------------------------------------------------------------------------------------------------------------|
| Number                   | ![ok.png](images/ok.png)          | Treated as a double.                                                                                         |
| String                   | ![ok.png](images/ok.png)          | Text box interactivity.                                                                                      |
| VI defined Class objects | ![ok.png](images/ok.png)          | Can be shared between steps from the same DLL, but not across DLLs.                                          | 
| Clusters                 | ![ok.png](images/ok.png)          | Can be shared between steps from the same DLL                                                                |
| Error Clusters           | ![ok.png](images/ok.png)          | See below.                                                                                                   |
| Boolean                  | ![ok.png](images/ok.png)          | This is displayed like a checkbox.                                                                           |
| Enum                     | ![ok.png](images/ok.png)          | Exported as a .NET enum. Interaction works like a drop-down.                                                 |
| Ring                     | ![notok.png](images/notok.png)    | Rings are exported as a unsigned 16bit value. It can be used but does not provide a user friendly interface. |
| Set                      | ![notok.png](images/notok.png)    | Cannot be translated to .NET                                                                                 |
| Map                      | ![notok.png](images/notok.png)    | Cannot be translated to .NET                                                                                 |
| Waveform                 | ![notok.png](images/notok.png)    | Support to be defined                                                                                        |
| Digital Data             | ![notok.png](images/notok.png)    | Support to be defined                                                                                        |
| Real Matrix              | ![notok.png](images/notok.png)    | Translated to `double[,]`. Support to be defined                                                             |
| ComboBox                 | ![notok.png](images/notok.png)    | Translated to string. Dropdown functionality not available.                                                  |
| Complex Numbers          | ![notok.png](images/notok.png)    | Not currently supported.                                                                                     |

### Error Clusters
Error clusters are how errors occur inside LabVIEW. Many VIs has no side effects and hence produce no errors, but some might.

An error is VI is represented by an error code and a message. When an VI-based test step has an error, it is handled by throwing an exception which will cause the test step to fail with an Error verdict.

See the Error Handling example for a practical example.