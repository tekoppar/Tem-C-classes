using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace Tem.Utility
{
    public static class MathTE
    {
        static public bool IsEqualTo(this double a, double b, double margin)
        {
            return Math.Abs(a - b) < margin;
        }

        static public float Map(float input, float inputMin, float inputMax, float min, float max)
        {
            return min + (input - inputMin) * (max - min) / (inputMax - inputMin);
        }
        static public float MapClamp(float input, float inputMin, float inputMax, float min, float max)
        {
            if (min > max)
                return Math.Max(Math.Min(min + (input - inputMin) * (max - min) / (inputMax - inputMin), min), max);
            else
                return Math.Min(Math.Max(min + (input - inputMin) * (max - min) / (inputMax - inputMin), min), max);
        }
    }

    public static class TE
    {
        private static Dictionary<string, int> ParentIndexes = new Dictionary<string, int>();
        public struct ObjectValue
        {
            public string Name { get; set; }
            public string Parent { get; set; }

            public ObjectValue(string name, string parent)
            {
                this.Name = name;
                this.Parent = parent;
            }
        };
        public static Dictionary<ObjectValue, object> GetPropertyObjectsByType(object src, string objectName, Type type)
        {
            Dictionary<ObjectValue, object> objects = new Dictionary<ObjectValue, object>();
            FieldInfo[] fields = src.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                string fieldName = field.Name.Replace("<", "").Replace(">k__BackingField", "").Replace("Color", "");
                var value = TE.GetPropertyValue(src, fieldName);
                if (value != null)
                {
                    if (value.GetType() == type)
                    {
                        ObjectValue value1 = new ObjectValue();
                        value1.Name = fieldName;
                        value1.Parent = objectName;
                        objects.Add(value1, value);
                    }
                    else
                    {
                        if (value is IList && value.GetType().IsGenericType)
                        {
                            foreach (var iProp in (IList)value)
                            {
                                Dictionary<ObjectValue, object> foundObjects = GetPropertyObjectsByType(iProp, fieldName, type);
                                if (foundObjects.Count > 0)
                                {
                                    foreach (var fObject in foundObjects)
                                    {
                                        if (ParentIndexes.ContainsKey(fObject.Key.Parent + '.' + fObject.Key.Name) == true)
                                        {
                                            ParentIndexes[fObject.Key.Parent + '.' + fObject.Key.Name] = ParentIndexes[fObject.Key.Parent + '.' + fObject.Key.Name] + 1;

                                            int count = ParentIndexes[fObject.Key.Parent + '.' + fObject.Key.Name];
                                            objects.Add(new ObjectValue(fObject.Key.Name, fObject.Key.Parent + count.ToString()), fObject.Value);
                                        }
                                        else
                                        {
                                            ParentIndexes[fObject.Key.Parent + '.' + fObject.Key.Name] = 1;
                                            objects.Add(new ObjectValue(fObject.Key.Name, fObject.Key.Parent + ParentIndexes[fObject.Key.Parent + '.' + fObject.Key.Name].ToString()), fObject.Value);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Dictionary<ObjectValue, object> foundObjects = GetPropertyObjectsByType(value, fieldName, type);
                            if (foundObjects.Count > 0)
                            {
                                foreach (var fObject in foundObjects)
                                {
                                    objects.Add(new ObjectValue(fObject.Key.Name, fObject.Key.Parent), fObject.Value);
                                }
                            }
                        }
                    }
                }
            }
            return objects;
        }
        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            var properties = src.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if (prop.Name == propName)
                {
                    var propFound = src.GetType().GetProperty(propName);
                    return propFound != null ? propFound.GetValue(src, null) : null;
                }
            }

            foreach (var prop in properties)
            {
                if (prop.PropertyType.IsClass == true && prop.CanWrite == true)
                {
                    object oProp = prop.GetValue(src, null);
                    if (oProp != null)
                    {
                        if (oProp is IList && oProp.GetType().IsGenericType)
                        {
                            foreach (var iProp in (IList)oProp)
                            {
                                if (iProp != null)
                                {
                                    object rProp = GetPropertyValue(iProp, propName);
                                    if (rProp != null)
                                        return rProp;
                                }
                            }
                        }
                        else
                        {
                            object rProp = GetPropertyValue(oProp, propName);
                            if (rProp != null)
                                return rProp;
                        }
                    }
                }
            }

            return null;
        }

        public static bool IsPathRelative(string a, string relative)
        {
            string rootFolder = "";
            if (Path.IsPathRooted(relative))
                rootFolder = Path.GetPathRoot(relative);

            return rootFolder != "" && a.Contains(rootFolder);
        }

        public static string GetRelativePath(string a, string relative)
        {
            string rootFolder = "";
            if (Path.IsPathRooted(relative))
                rootFolder = Path.GetPathRoot(relative);

            if (rootFolder != "")
            {
                rootFolder = a.Replace(relative, "");
            }
            return rootFolder;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static List<int> StringToIntVector(string s, char delimiter)
        {
            return TE.StringToIntVector(s, new string[] { delimiter.ToString() });
        }
        public static List<int> StringToIntVector(string s, string delimiter)
        {
            return TE.StringToIntVector(s, new string[] { delimiter });
        }
        public static List<int> StringToIntVector(string s, string[] delimiter)
        {
            List<string> splitS = s.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> ints = new List<int>();

            if (splitS.Count > 0)
            {
                foreach (string ss in splitS)
                {
                    ints.Add(int.Parse(ss));
                }
                return ints;
            }
            else
                return new List<int>();
        }
        public static string IntVectorToString(List<int> ints, string delimiter)
        {
            string s = "";

            if (ints.Count > 0)
            {
                for (int i = 0; i < ints.Count; i++)
                {
                    if (i == ints.Count - 1)
                        s += ints[i].ToString();
                    else
                        s += ints[i].ToString() + delimiter;
                }
                return s;
            }
            else
                return "";
        }
        public static List<string> StringToStringVector(string s, string delimiter)
        {
            return s.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public static string StringVectorToString(List<string> s, string delimiter)
        {
            string ss = "";

            for (int i = 0; i < s.Count; i++)
            {
                if (i == s.Count - 1)
                    ss += s[i];
                else
                    ss += s[i] + delimiter;
            }

            return ss;
        }
    }
}