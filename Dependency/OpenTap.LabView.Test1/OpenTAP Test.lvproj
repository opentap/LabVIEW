<?xml version='1.0' encoding='UTF-8'?>
<Project Type="Project" LVVersion="24008000">
	<Property Name="NI.LV.All.SourceOnly" Type="Bool">true</Property>
	<Item Name="My Computer" Type="My Computer">
		<Property Name="server.app.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.control.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.tcp.enabled" Type="Bool">false</Property>
		<Property Name="server.tcp.port" Type="Int">0</Property>
		<Property Name="server.tcp.serviceName" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.tcp.serviceName.default" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.vi.callsEnabled" Type="Bool">true</Property>
		<Property Name="server.vi.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="specify.custom.address" Type="Bool">false</Property>
		<Item Name="Error_Handling_Test.vi" Type="VI" URL="../Error_Handling_Test.vi"/>
		<Item Name="Test Array.vi" Type="VI" URL="../Test Array.vi"/>
		<Item Name="Test Call SubVi.vi" Type="VI" URL="../Test Call SubVi.vi"/>
		<Item Name="Test Class 1.lvclass" Type="LVClass" URL="../Test Class 1/Test Class 1.lvclass"/>
		<Item Name="Test Multiply With Self.vi" Type="VI" URL="../Test Multiply With Self.vi"/>
		<Item Name="Dependencies" Type="Dependencies">
			<Item Name="vi.lib" Type="Folder">
				<Item Name="Error Cluster From Error Code.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Error Cluster From Error Code.vi"/>
				<Item Name="Get LV Class Default Value.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/LVClass/Get LV Class Default Value.vi"/>
			</Item>
		</Item>
		<Item Name="Build Specifications" Type="Build">
			<Item Name="OpenTap.LabView.Test1" Type=".NET Interop Assembly">
				<Property Name="App_copyErrors" Type="Bool">true</Property>
				<Property Name="App_INI_aliasGUID" Type="Str">{7E5E900A-8207-4DD4-A8A0-280F7F28A110}</Property>
				<Property Name="App_INI_GUID" Type="Str">{EBFC67D1-84BD-4613-81BA-1A94370AE30F}</Property>
				<Property Name="App_serverConfig.httpPort" Type="Int">8002</Property>
				<Property Name="App_serverType" Type="Int">0</Property>
				<Property Name="App_winsec.description" Type="Str">http://www.Keys.com</Property>
				<Property Name="Bld_autoIncrement" Type="Bool">true</Property>
				<Property Name="Bld_buildCacheID" Type="Str">{65371357-6200-4587-A554-FA3596D6F860}</Property>
				<Property Name="Bld_buildSpecName" Type="Str">OpenTap.LabView.Test1</Property>
				<Property Name="Bld_excludeInlineSubVIs" Type="Bool">true</Property>
				<Property Name="Bld_excludeLibraryItems" Type="Bool">true</Property>
				<Property Name="Bld_excludePolymorphicVIs" Type="Bool">true</Property>
				<Property Name="Bld_localDestDir" Type="Path">../builds/NI_AB_PROJECTNAME/OpenTap.LabView.Test1</Property>
				<Property Name="Bld_localDestDirType" Type="Str">relativeToCommon</Property>
				<Property Name="Bld_modifyLibraryFile" Type="Bool">true</Property>
				<Property Name="Bld_previewCacheID" Type="Str">{7F60A1B0-4C2C-4F91-AC61-2B98844F767E}</Property>
				<Property Name="Bld_version.build" Type="Int">9</Property>
				<Property Name="Bld_version.major" Type="Int">1</Property>
				<Property Name="Destination[0].destName" Type="Str">OpenTap.LabView.Test1.dll</Property>
				<Property Name="Destination[0].path" Type="Path">../builds/NI_AB_PROJECTNAME/OpenTap.LabView.Test1/OpenTap.LabView.Test1.dll</Property>
				<Property Name="Destination[0].preserveHierarchy" Type="Bool">true</Property>
				<Property Name="Destination[0].type" Type="Str">App</Property>
				<Property Name="Destination[1].destName" Type="Str">Support Directory</Property>
				<Property Name="Destination[1].path" Type="Path">../builds/NI_AB_PROJECTNAME/OpenTap.LabView.Test1/data</Property>
				<Property Name="DestinationCount" Type="Int">2</Property>
				<Property Name="DotNET2011CompatibilityMode" Type="Bool">false</Property>
				<Property Name="DotNETAssembly_ClassName" Type="Str">LabVIEWExports</Property>
				<Property Name="DotNETAssembly_delayOSMsg" Type="Bool">true</Property>
				<Property Name="DotNETAssembly_Namespace" Type="Str">OpenTap.LabView.Test1</Property>
				<Property Name="DotNETAssembly_signAssembly" Type="Bool">false</Property>
				<Property Name="DotNETAssembly_StrongNameKeyFileItemID" Type="Ref"></Property>
				<Property Name="DotNETAssembly_StrongNameKeyGUID" Type="Str">{355D8948-4820-4E96-A734-35D90A751E4D}</Property>
				<Property Name="Source[0].itemID" Type="Str">{F8951296-917F-4A3B-A7B4-79A39D95A382}</Property>
				<Property Name="Source[0].type" Type="Str">Container</Property>
				<Property Name="Source[1].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Numeric</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]MethodName" Type="Str">Test___32Multiply___32With___32Self</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIName" Type="Str">Test Multiply With Self.vi</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">Gauge</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str"></Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[1].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">3</Property>
				<Property Name="Source[1].itemID" Type="Ref">/My Computer/Test Multiply With Self.vi</Property>
				<Property Name="Source[1].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[1].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[2].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]MethodName" Type="Str">Test___32Call___32SubVi</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIName" Type="Str">Test Call SubVi.vi</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str"></Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[2].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">1</Property>
				<Property Name="Source[2].itemID" Type="Ref">/My Computer/Test Call SubVi.vi</Property>
				<Property Name="Source[2].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[2].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[3].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">Array</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Array</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">Index</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]MethodName" Type="Str">Test___32Array</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIName" Type="Str">Test Array.vi</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">Gauge</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str">This is Test 3 vi.</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[3].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">4</Property>
				<Property Name="Source[3].itemID" Type="Ref">/My Computer/Test Array.vi</Property>
				<Property Name="Source[3].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[3].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[4].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">LabVIEW Class Instance</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Test__32Class__321__32in</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">Numeric</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">LabVIEW Class Instance</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">Test__32Class__321__32out</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">2</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">Meter</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">2</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoConNum" Type="Int">8</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoDir" Type="Int">6</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoIutputIdx" Type="Int">8</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoName" Type="Str">error__32in__32__40no__32error__41</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[5]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]MethodName" Type="Str">Set___32Property</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIName" Type="Str">Set Property.vi</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoConNum" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoDir" Type="Int">7</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoName" Type="Str">error__32out</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfo[6]VIProtoOutputIdx" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str"></Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[4].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">7</Property>
				<Property Name="Source[4].itemID" Type="Ref">/My Computer/Test Class 1.lvclass/Set Property.vi</Property>
				<Property Name="Source[4].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[4].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[5].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">Numeric</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">10</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">DBL</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">10</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">Numeric__322</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoConNum" Type="Int">9</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDataType" Type="Str">Bool</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoIutputIdx" Type="Int">9</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoName" Type="Str">Boolean</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[3]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]MethodName" Type="Str">Test___32Class___321</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIName" Type="Str">Test Class 1.vi</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDataType" Type="Str">LabVIEW Class Instance</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoName" Type="Str">Test__32Class__321__32out</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfo[4]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str"></Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[5].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">5</Property>
				<Property Name="Source[5].itemID" Type="Ref">/My Computer/Test Class 1.lvclass/Test Class 1.vi</Property>
				<Property Name="Source[5].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[5].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="Source[6].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoConNum" Type="Int">-1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDataType" Type="Str">void</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoDir" Type="Int">1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoName" Type="Str">returnvalue</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[0]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoConNum" Type="Int">11</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDataType" Type="Str">String</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoDir" Type="Int">0</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoIutputIdx" Type="Int">11</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoName" Type="Str">String</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[1]VIProtoOutputIdx" Type="Int">-1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]MethodName" Type="Str">Error_Handling_Test</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIName" Type="Str">Error_Handling_Test.vi</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoConNum" Type="Int">3</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDataType" Type="Str">Cluster</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoDir" Type="Int">7</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoIutputIdx" Type="Int">-1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoName" Type="Str">error__32out</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfo[2]VIProtoOutputIdx" Type="Int">3</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfoVIDocumentation" Type="Str"></Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfoVIDocumentationEnabled" Type="Int">0</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfoVIIsNamesSanitized" Type="Int">1</Property>
				<Property Name="Source[6].ExportedAssemblyVI.VIProtoInfoVIProtoItemCount" Type="Int">3</Property>
				<Property Name="Source[6].itemID" Type="Ref">/My Computer/Error_Handling_Test.vi</Property>
				<Property Name="Source[6].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[6].type" Type="Str">ExportedAssemblyVI</Property>
				<Property Name="SourceCount" Type="Int">7</Property>
				<Property Name="TgtF_companyName" Type="Str">Keys</Property>
				<Property Name="TgtF_fileDescription" Type="Str">OpenTap.LabView.Test1</Property>
				<Property Name="TgtF_internalName" Type="Str">OpenTap.LabView.Test1</Property>
				<Property Name="TgtF_legalCopyright" Type="Str">Copyright © 2024 Keys</Property>
				<Property Name="TgtF_productName" Type="Str">OpenTap.LabView.Test1</Property>
				<Property Name="TgtF_targetfileGUID" Type="Str">{E5260485-3FD4-45D0-AC99-D606B182F313}</Property>
				<Property Name="TgtF_targetfileName" Type="Str">OpenTap.LabView.Test1.dll</Property>
				<Property Name="TgtF_versionIndependent" Type="Bool">true</Property>
			</Item>
		</Item>
	</Item>
</Project>
