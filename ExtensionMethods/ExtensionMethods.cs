using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WpfTask.Models;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static object CloneObject<T>(this object source)
            where T : ICloneable
        {
            List<T> result = Activator.CreateInstance<List<T>>();
                
            foreach (var value in  (IEnumerable) source)
            {
                result.Add((T)value);
            }
            return result;
        }
    }
}