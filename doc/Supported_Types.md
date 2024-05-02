

## Supported VI Types
| Type Name                | Supported | Notes                                                               |
|--------------------------|-----------|---------------------------------------------------------------------|
| Number                   | ✅         | Treated as a double.                                                |
| String                   | ✅         |                                                                     |
| VI defined Class objects | ✅         | Can be shared between steps from the same DLL, but not across DLLs. | 
| Clusters                 | ✅         | Can be shared between steps from the same DLL                       |
| Boolean                  | ✅         |                                                                     |
| Enums                    | ✅         |                                                                     |
| Set                      | ❌         | Cannot be translated to .NET                                        |
| Map                      | ❌         | Cannot be translated to .NET                                        |
| Waveform                 | ❌         | Support to be defined                                               |
| Digital Data             | ❌         | Support to be defined                                               |
| Real Matrix              | ❌         | Translated to `double[,]`. Support to be defined                    |
| ComboBox                 | ❌         | Translated to string                                                |


