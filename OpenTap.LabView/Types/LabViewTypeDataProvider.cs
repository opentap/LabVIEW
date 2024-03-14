﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NationalInstruments.LabVIEW.Interop;

namespace OpenTap.LabView.Types
{

    /// <summary>
    /// The type data provider that loads the VEE files and generates the collection of VEE type data.
    /// </summary>
    public class LabViewTypeDataProvider : ITypeDataProvider, ITypeDataSearcher
    {
        internal const string PREFIX = "vi:";

        static readonly List<LabViewTypeData> LabViewTypes = new List<LabViewTypeData>();

        public IEnumerable<ITypeData> Types => LabViewTypes.Select(x => x);

        public LabViewTypeDataProvider()
        {

        }

        /// <summary> Locate a type based on string identifier. </summary>
        public ITypeData GetTypeData(string identifier)
        {
            if (identifier.StartsWith(PREFIX))
                return LabViewTypes.FirstOrDefault(type => type.Name == identifier);
            
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ITypeData GetTypeData(object obj)
        {
            if (obj is LabViewTestStep step)
                return step.PluginType;
            if (obj is LabViewResource res)
                return res.PluginType;
            
            return null;
        }

        /// <summary>  Priority slightly higher than default (required). </summary>
        public double Priority => 1;

        /// <summary> Load LabView DLLs and load labview types. </summary>
        public void Search()
        {
            var allAssemblies = PluginManager.GetSearcher().Assemblies.ToArray();

            foreach (var asm in allAssemblies)
            {
                bool isVi = false;
                foreach (var reference in asm.References)
                {
                    if (reference.Name.Contains("LabVIEW.Interop"))
                    {
                        isVi = true;
                        break;
                    }
                }
                if (!isVi) continue;

                var asm2 = asm.Load();

                Type[] types = asm2.GetTypes();

                foreach (Type type in types)
                {
                    if (typeof(LVClassRoot).IsAssignableFrom(type) == false && type.Name != "LabVIEWExports")
                        continue;

                    if (type.Name != "LabVIEWExports")
                    {
                        var resourceType = new LabViewTypeData(type);
                        LabViewTypes.Add(resourceType);
                    }
                    else
                    {
                        
                    }

                    var declaredMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    foreach (var method in declaredMethods)
                    {
                        var newType = new LabViewTypeData(method, type);
                        LabViewTypes.Add(newType);
                    }
                }

            }
        }
    }

    class LvClassAvailableValuesAnnotation : IAvailableValuesAnnotation
    {
        ITypeData TargetType;
        public LvClassAvailableValuesAnnotation(ITypeData memTypeDescriptor)
        {
            this.TargetType = memTypeDescriptor;
        }

        public IEnumerable AvailableValues
        {
            get => InstrumentSettings.Current.Where(instr => TypeData.GetTypeData(instr).DescendsTo(TargetType));
        }
    }
    
    public class LabViewAnotator : IAnnotator
    {

        public void Annotate(AnnotationCollection annotations)
        {
            var mem = annotations.Get<IMemberAnnotation>()?.Member;
            if (mem != null && mem.TypeDescriptor.DescendsTo(typeof(LabViewResource)))
            {
                annotations.Add(new LvClassAvailableValuesAnnotation(mem.TypeDescriptor));
            }
        }
        public double Priority => 0.0;
    }

}