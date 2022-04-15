using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Conscript.Compiler;
using Conscript.Runtime;

namespace Conscript.Modules
{
    public class BasicMathModule
        : HostModule
    {
        #region Private Static Variables

        private static ReadOnlyCollection<HostFunctionPrototype> s_listHostFunctionPrototypes;
        private static Random s_random;

        #endregion

        #region Private Methods

        private void ThrowUnsupportedTypeError(String strFunctionName, Type type)
        {
            throw new ExecutionException(
                "Type '" + type.Name + "' is not supported by function '"
                + strFunctionName + "'.");
        }

        private float CastToFloat(object objectParameter)
        {
            if (objectParameter.GetType() == typeof(int))
                return (float)(int)objectParameter;
            else if (objectParameter.GetType() == typeof(float))
                return (float) objectParameter;
            else
                throw new ExecutionException(
                    "Cannot case type '" + objectParameter.GetType().Name + "' to a float.");
        }

        #endregion

        #region Public Methods

        public BasicMathModule()
        {
            if (s_listHostFunctionPrototypes != null) return;
            s_random = new Random((int) DateTime.Now.Ticks);

            List<HostFunctionPrototype> listHostFunctionPrototypes = new List<HostFunctionPrototype>();
            HostFunctionPrototype hostFunctionPrototype = null;
            hostFunctionPrototype = new HostFunctionPrototype((Type)null, "math_abs", (Type)null);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_acos", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_asin", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_atan", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_atan2", typeof(float), typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_ceiling", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_cos", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_cosh", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_e");
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_floor", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_log", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_log2", typeof(float), typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype((Type)null, "math_max", (Type)null);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype((Type)null, "math_min", (Type)null);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_pi");
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(int), "math_round", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_round2", typeof(float), typeof(int));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype((Type)null, "math_sign", (Type)null);
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_sin", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_sinh", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_sqrt", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_tan", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(float), "math_tanh", typeof(float));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);
            hostFunctionPrototype = new HostFunctionPrototype(typeof(int), "math_rand", typeof(int));
            listHostFunctionPrototypes.Add(hostFunctionPrototype);

            s_listHostFunctionPrototypes = listHostFunctionPrototypes.AsReadOnly();
        }

        public ReadOnlyCollection<HostFunctionPrototype> HostFunctionPrototypes
        {
            get { return s_listHostFunctionPrototypes; }
        }

        public object OnHostFunctionCall(String strFunctionName, List<object> listParameters)
        {
            if (strFunctionName == "math_abs")
            {
                Type typeParameter = listParameters[0].GetType();
                if (typeParameter == typeof(int))
                    return Math.Abs((int)listParameters[0]);
                if (typeParameter == typeof(float))
                    return Math.Abs((float)listParameters[0]);
                ThrowUnsupportedTypeError(strFunctionName, typeParameter);
            }
            else if (strFunctionName == "math_acos")
                return (float)Math.Acos((float)listParameters[0]);
            else if (strFunctionName == "math_asin")
                return (float)Math.Asin((float)listParameters[0]);
            else if (strFunctionName == "math_atan")
                return (float)Math.Atan((float)listParameters[0]);
            else if (strFunctionName == "math_atan2")
                return (float)Math.Atan2((float)listParameters[0], (float)listParameters[1]);
            else if (strFunctionName == "math_ceiling")
                return (float)Math.Ceiling((float)listParameters[0]);
            else if (strFunctionName == "math_cos")
                return (float)Math.Cos((float)listParameters[0]);
            else if (strFunctionName == "math_cosh")
                return (float)Math.Cosh((float)listParameters[0]);
            else if (strFunctionName == "math_e")
                return (float)Math.E;
            else if (strFunctionName == "math_floor")
                return (float)Math.Floor((float)listParameters[0]);
            else if (strFunctionName == "math_log")
                return (float)Math.Log((float)listParameters[0]);
            else if (strFunctionName == "math_log2")
                return (float)Math.Log((float)listParameters[0], (float)listParameters[1]);
            else if (strFunctionName == "math_max")
            {
                Type typeParameter1 = listParameters[0].GetType();
                Type typeParameter2 = listParameters[1].GetType();
                if (typeParameter1 != typeof(int) && typeParameter1 != typeof(float))
                    ThrowUnsupportedTypeError(strFunctionName, typeParameter1);
                if (typeParameter2 != typeof(int) && typeParameter2 != typeof(float))
                    ThrowUnsupportedTypeError(strFunctionName, typeParameter2);
                if (typeParameter1 == typeof(int) && typeParameter2 == typeof(int))
                    return (int) Math.Max((int)listParameters[0], (int)listParameters[1]);
                return (float)Math.Max(CastToFloat(listParameters[0]),
                    CastToFloat(listParameters[1]));
            }
            else if (strFunctionName == "math_min")
            {
                Type typeParameter1 = listParameters[0].GetType();
                Type typeParameter2 = listParameters[1].GetType();
                if (typeParameter1 != typeof(int) && typeParameter1 != typeof(float))
                    ThrowUnsupportedTypeError(strFunctionName, typeParameter1);
                if (typeParameter2 != typeof(int) && typeParameter2 != typeof(float))
                    ThrowUnsupportedTypeError(strFunctionName, typeParameter2);
                if (typeParameter1 == typeof(int) && typeParameter2 == typeof(int))
                    return (int)Math.Min((int)listParameters[0], (int)listParameters[1]);
                return (float)Math.Min(CastToFloat(listParameters[0]),
                    CastToFloat(listParameters[1]));
            }
            else if (strFunctionName == "math_pi")
                return (float)Math.PI;
            else if (strFunctionName == "math_round")
                return (int) Math.Round((float)listParameters[0]);
            else if (strFunctionName == "math_round2")
                return (float)Math.Round((float)listParameters[0], (int) listParameters[1]);
            if (strFunctionName == "math_sign")
            {
                Type typeParameter = listParameters[0].GetType();
                if (typeParameter == typeof(int))
                    return Math.Sign((int)listParameters[0]);
                if (typeParameter == typeof(float))
                    return Math.Sign((float)listParameters[0]);
                ThrowUnsupportedTypeError(strFunctionName, typeParameter);
            }
            else if (strFunctionName == "math_sin")
                return (float)Math.Sin((float)listParameters[0]);
            else if (strFunctionName == "math_sinh")
                return (float)Math.Sinh((float)listParameters[0]);
            else if (strFunctionName == "math_sqrt")
                return (float)Math.Sqrt((float)listParameters[0]);
            else if (strFunctionName == "math_tan")
                return (float)Math.Tan((float)listParameters[0]);
            else if (strFunctionName == "math_tanh")
                return (float)Math.Tanh((float)listParameters[0]);
            else if (strFunctionName == "math_rand")
                return (int)s_random.Next((int)listParameters[0]);

            throw new ExecutionException(
                "Unimplemented function '" + strFunctionName + "'.");
        }

        #endregion
    }
}
