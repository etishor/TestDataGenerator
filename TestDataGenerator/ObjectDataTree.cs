
namespace TestDataGenerator
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

	/// <summary>
	/// Helper class to read the fields of an object and expose them as strings.
	/// </summary>
	/// <remarks>
	/// This class is intended to be used for checking if two objects have the same state.
	/// </remarks>
    public class ObjectDataTree
    {
        public ObjectDataTree(object instance, Type type, string fieldName)
        {
            // can be null
            this.Instance = instance;
            this.Type = type;
            this.Name = CleanName(fieldName);
        }

        public ObjectDataTree(object instance)
            : this(instance, instance.GetType(), string.Empty)
        {
        }

        public object Instance { get; private set; }

        public Type Type { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<ObjectDataTree> Fields
        {
            get
            {
                if (IsComparable(this.Type) || this.Instance == null)
                {
                    return new ObjectDataTree[0];
                }

                return this.Type.GetFields(BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance)
                    .Select(f => new ObjectDataTree(f.GetValue(this.Instance), f.FieldType, f.Name));
            }
        }

        public string StringValue()
        {
            return StringValue(string.Empty);
        }

        private string StringValue(string prefix)
        {
            string name = string.IsNullOrEmpty(this.Name) ? string.Empty : string.Format("{0}:", this.Name);
            if (this.Instance == null)
            {
                return string.Format("{0}{1}\"NULL\"", prefix, name);
            }

            if (IsComparable(this.Type))
            {
                return string.Format("{0}{1}\"{2}\"", prefix, name, GetInstanceString(this.Instance).Replace("\"", "\\\""));
            }

            StringBuilder builder = new StringBuilder();


            if (typeof(IEnumerable).IsAssignableFrom(this.Type))
            {
                builder.AppendFormat("{0}{1}[\n", prefix, name);
                foreach (object item in this.Instance as IEnumerable)
                {
                    var obj = new ObjectDataTree(item);
                    builder.AppendLine(obj.StringValue(string.Concat(prefix, "\t")));
                }
                builder.AppendFormat("{0}]\n", prefix);
            }
            else
            {
                builder.AppendFormat("{0}{1}{{\n", prefix, name);
                foreach (var o in this.Fields)
                {
                    builder.AppendLine(o.StringValue(string.Concat(prefix, "\t")));
                }
                builder.AppendFormat("{0}}}\n", prefix);
            }
            return builder.ToString();
        }

		private string GetInstanceString(object instance)
		{
			if(this.Type == typeof(DateTime))
			{
				return ((DateTime)instance).ToUniversalTime().ToString();
			}

			return instance.ToString();
		}

        private bool IsComparable(Type type)
        {
            if (comparableTypes.Contains(this.Type))
            {
                return true;
            }

            if (type.IsEnum)
            {
                return true;
            }

            return false;
        }


        private string CleanName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return fieldName;
            }

            Match match = Regex.Match(fieldName, "<(.*)>k__BackingField");

            if (match == null || match.Groups.Count != 2 || string.IsNullOrEmpty(match.Groups[1].Value))
            {
                return fieldName;
            }

            return match.Groups[1].Value;
        }

        private static readonly ISet<Type> comparableTypes = new HashSet<Type> {
            typeof(sbyte),typeof(byte), typeof(short), typeof(ushort), 
            typeof(int),typeof(uint),typeof(long),typeof(ulong),
            typeof(float),typeof(double), typeof(decimal),
            typeof(bool),
            typeof(char),
            typeof(string),
			typeof(TimeSpan),
            typeof(DateTime),
            typeof(Guid),
            typeof(Uri)
        };
    }

}
