using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Grax.EasyResource
{
    public static class EasyResources
    {
        /// <summary>
        /// Get embedded resource from the assembly that contains type T
        /// </summary>
        /// <typeparam name="T">Any type from the assembly that contains the embedded resource.</typeparam>
        /// <param name="resourceName">Path name to embedded resource or full name from manifest resource name</param>
        /// <returns></returns>
        public static string GetStringResource<T>(string resourceName)
        {
            return ResourceFromType<T>.GetStringResource(resourceName);
        }

        /// <summary>
        /// Get embedded resource from the assembly that contains type T
        /// </summary>
        /// <typeparam name="T">Any type from the assembly that contains the embedded resource.</typeparam>
        /// <param name="resourceName">Path name to embedded resource or full name from manifest resource name</param>
        /// <returns></returns>
        public static byte[] GetResource<T>(string resourceName)
        {
            return ResourceFromType<T>.GetResource(resourceName);
        }

        /// <summary>
        /// Get embedded resource from the assembly
        /// </summary>
        /// <param name="assembly">The assembly that contains the embedded resource</param>
        /// <param name="resourceName">Path name to embedded resource or full name from manifest resource name</param>
        /// <returns></returns>
        public static string GetStringResource(this Assembly assembly, string resourceName)
        {
            return
                assembly.FetchStringResource(assembly.GetMatchingResourceName(resourceName));
        }

        /// <summary>
        /// Get embedded resource from the assembly
        /// </summary>
        /// <param name="assembly">The assembly that contains the embedded resource</param>
        /// <param name="resourceName">Path name to embedded resource or full name from manifest resource name</param>
        /// <returns></returns>
        public static byte[] GetResource(this Assembly assembly, string resourceName)
        {

            return
                assembly.FetchResource(assembly.GetMatchingResourceName(resourceName));
        }

        private static string FetchStringResource(this Assembly resourceAssembly, string resourceName)
        {

            using (Stream stream = resourceAssembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static byte[] FetchResource(this Assembly resourceAssembly, string resourceName)
        {
            using (Stream stream = resourceAssembly.GetManifestResourceStream(resourceName))
            {
                var length = (int)stream.Length;
                var returnValue = new byte[length];

                stream.Read(returnValue, 0, length);

                return returnValue;
            }
        }

        private static string GetMatchingResourceName(this Assembly assembly, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name parameter must not be null or empty", "name");
            }
            name = name.Replace("\\", ".").Replace("/", ".");
            return assembly.GetManifestResourceNames().Single(v => v.EndsWith(name, StringComparison.CurrentCultureIgnoreCase));
        }

        internal static class ResourceFromType<T>
        {
            static Assembly resourceAssembly = typeof(T).GetTypeInfo().Assembly;
            static string[] names = resourceAssembly.GetManifestResourceNames();

            private static string GetMatchingResourceName(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("name parameter must not be null or empty", "name");
                }
                name = name.Replace("\\", ".").Replace("/", ".");
                return names.Single(v => v.EndsWith(name, StringComparison.CurrentCultureIgnoreCase));
            }

            public static string GetStringResource(string name)
            {
                name = GetMatchingResourceName(name);
                return FetchStringResource(resourceAssembly, name);
            }

            public static byte[] GetResource(string name)
            {
                name = GetMatchingResourceName(name);
                return FetchResource(resourceAssembly, name);
            }
        }
    }
}