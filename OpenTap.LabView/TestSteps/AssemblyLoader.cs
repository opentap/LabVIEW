using OpenTap;   // Use OpenTAP infrastructure/core components (log,TestStep definition, etc)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace OpenTap.LabView
{
    [Display("Load Assembly", Group: "LabVIEW", Description: "Load Assembly from Labview VI Interop")]
    class AssemblyLoaderStep : TestStep
    {
        #region Settings
        // ToDo: Add property here for each parameter the end user should be able to change.
        #endregion

        public class ListItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        [Display("Load Assembly", "Load Assembly", "", 1)]
        [Browsable(true)]
        public void Button_Clicked()
        {
            

            {
                _listOfAvailableText.Clear();
                _listOfAvailableValues.Clear();

                assemblyPath = "test.dll";

                // Load the assembly
                _assembly = Assembly.LoadFrom(assemblyPath);

                // Get all types in the assembly
                Type[] types = _assembly.GetTypes();

                // Get all methods from all types
                List<MethodInfo> methods = new List<MethodInfo>();
                foreach (Type type in types)
                {
                    MethodInfo[] declaredMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    methods.AddRange(declaredMethods);
                }

                // Display methods with parameters in dropdown box
                foreach (MethodInfo method in methods)
                {
                    // Get method parameters
                    ParameterInfo[] parameters = method.GetParameters();
                    string paramList = string.Join(", ", Array.ConvertAll(parameters, p => p.ParameterType.Name + " " + p.Name));
                    string itemText = $"{method.DeclaringType.Name}.{method.Name}({paramList})";
                    string itemVal = $"{method.DeclaringType.FullName}.{method.Name}({paramList})";
                    _listOfAvailableValues.Add(new ListItem() { Text = itemText, Value = itemVal });
                    _listOfAvailableText.Add(itemText);
                }
            }
        }

        private List<ListItem> _listOfAvailableValues;
        private List<String> _listOfAvailableText;
        [Display("Available Values", "An editable list of values.")]
        [Browsable(false)]
        public List<string> ListOfAvailableText
        {
            get { return _listOfAvailableText; }
            set
            {
                _listOfAvailableText = value;
                OnPropertyChanged("ListOfAvailableText");
            }
        }


        [Display("Function : ", "Shows the use of available values attribute.", "", 1)]
        [AvailableValues(nameof(ListOfAvailableText))]
        public string selectedText { get; set; }
        private string selectedValue { get { return _listOfAvailableValues.Where(x => x.Text == selectedText).Select(x => x.Value.ToString()).First(); } set { } }

        [Display("Parameters : ", "Parameters", "", 2)]
        [Browsable(true)]
        public string Parameters
        {
            get
             { return GetParameterTypeNames(selectedText); }

            private set { }
        }

        [Display("Parameters Value : ", "Parameters Value", "", 3)]
        public string ParamValues { get; set; }

        [Display("Output Parameters : ", "Output Parameters", "", 4)]
        [Browsable(true)]
        public string Output { get; private set; }
        private Assembly _assembly { get; set; }
        private string assemblyPath { get; set; }

        

        public AssemblyLoaderStep()
        {
            _listOfAvailableValues = new List<ListItem>();
            _listOfAvailableText = new List<string>();
            selectedText = string.Empty;
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
        //    RunChildSteps(); //If the step supports child steps.

            string methodNameWithNamespace = selectedValue;
            int indexOfLastDot = methodNameWithNamespace.LastIndexOf('.');

            int indexOfOpenParenthesis = methodNameWithNamespace.IndexOf('(');
            string methodNameOnly = methodNameWithNamespace.Substring(indexOfLastDot + 1, indexOfOpenParenthesis - indexOfLastDot - 1);

            string classNameWithNamespace = methodNameWithNamespace.Substring(0, indexOfLastDot);
            string className = selectedText;
            className = className.Substring(0, className.IndexOf('.'));

            var selectedMethod = _assembly.GetType(classNameWithNamespace).GetMethod(methodNameOnly);

            if (selectedMethod != null)
            {
                var mainClassConstructor = getMethodInfo(classNameWithNamespace, className);

                object[] parameters = getParameters(mainClassConstructor);

                mainClassConstructor.Invoke(null, parameters);
                var mainClassObject = parameters[0];

                List<object> objList = new List<object>();

                objList.Add(mainClassObject);

                if (selectedMethod.GetParameters().Count(x => x.ParameterType.Name == "Qry") != 0)
                {
                    //objList.Add(new Qry());
                }

                object[] parameters2 = getParameters(selectedMethod, objList.ToArray());

                selectedMethod.Invoke(null, parameters2.ToArray());

                Output = (string.Join(",", parameters2));
            }
            else
            {
                Output = "No method selected";
            }
        }

        public MethodInfo getMethodInfo(string classNameWithNamespace, string className)
        {
            return _assembly.GetType(classNameWithNamespace).GetMethod(className);
        }


        public object[] getParameters(MethodInfo clsobj, object[] objInst = null)
        {
            ParameterInfo[] parameterInfos = clsobj.GetParameters();
            object[] parameters = new object[parameterInfos.Length];

            string[] parts = ParamValues.Split(',');
            string methodName = parts[0];

            // Set the parameter values
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                string parameterValueString = parts[i].Trim();
                // Convert parameter values to appropriate types
                if (!parameterInfos[i].ParameterType.IsByRef)
                {
                    var tempObj = objInst.Where(x => x.GetType().Name.Equals(parameterInfos[i].ParameterType.Name)).FirstOrDefault();
                    
                    if (tempObj != null)
                    {                        
                        parameters[i] = tempObj;
                    }
                    else
                    {
                        if (parameterValueString != null)
                        {
                            if (parameterInfos[i].ParameterType.IsEnum)
                            {
                                parameters[i] = Enum.Parse(parameterInfos[i].ParameterType, parameterValueString);
                            }
                            else
                            {
                                parameters[i] = Convert.ChangeType(parameterValueString, parameterInfos[i].ParameterType);
                            }
                        }
                        else
                        {
                            parameters[i] = null;
                        }
                    }
                }
                else
                {
                    parameters[i] = null;
                }

            }

            return parameters;
        }


        public static string GetParameterTypeNames(string methodSignature)
        {
            int startIndex = methodSignature.IndexOf('(') + 1;
            int endIndex = methodSignature.LastIndexOf(')');

            // Extract the substring containing only the parameters
            string parametersString = methodSignature.Substring(startIndex, endIndex - startIndex);

            // Split the parameters by commas
            string[] parameters = parametersString.Split(',');

            // Extract parameter type names
            string[] parameterTypeNames = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                // Trim leading and trailing whitespace
                string parameter = parameters[i].Trim();

                // Get the last part as parameter type name
                int lastSpaceIndex = parameter.LastIndexOf(' ');
                parameterTypeNames[i] = parameter.Substring(0,lastSpaceIndex);
            }

            // Concatenate parameter type names into a single comma-separated string
            return string.Join(", ", parameterTypeNames);
        }

    }
}
