namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;

    public class PrimitieveBuilder : IBuildInstances
    {
        private readonly IDictionary<Type, Func<string, object>> builders = new Dictionary<Type, Func<string, object>>();

        public PrimitieveBuilder()
        {
            this.builders.Add(typeof(sbyte), (s) => Rnd.SByte());
            this.builders.Add(typeof(byte), (s) => Rnd.Byte());
            this.builders.Add(typeof(short), (s) => Rnd.Short());
            this.builders.Add(typeof(ushort), (s) => Rnd.UShort());
            this.builders.Add(typeof(int), (s) => Rnd.Integer());
            this.builders.Add(typeof(uint), (s) => Rnd.UInt());
            this.builders.Add(typeof(long), (s) => Rnd.Long());
            this.builders.Add(typeof(ulong), (s) => Rnd.ULong());
            this.builders.Add(typeof(char), (s) => Rnd.Char());
            this.builders.Add(typeof(float), (s) => Rnd.Float());
            this.builders.Add(typeof(double), (s) => Rnd.Double());
            this.builders.Add(typeof(bool), (s) => Rnd.Bool());
            this.builders.Add(typeof(decimal), (s) => Rnd.Decimal());


            this.builders.Add(typeof(TimeSpan), (s) => Rnd.TimeSpan());
            this.builders.Add(typeof(DateTime), (s) => Rnd.Date());
            this.builders.Add(typeof(string), BuildString);
            this.builders.Add(typeof(Uri), (s) => Rnd.Uri(s));
            this.builders.Add(typeof(Guid), (s) => Guid.NewGuid());

        }

        public bool CanCreate(Type type)
        {
            return this.builders.ContainsKey(type) || type.IsEnum;
        }

        public object CreateInstance(Type type, string name)
        {
            if (!this.CanCreate(type))
            {
                throw new InvalidOperationException("Unable to create instance of type");
            }

            if (type.IsEnum)
            {
                return Rnd.RandomEnumValue(type);
            }

            return this.builders[type](name);
        }


        private static string BuildString(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (name.Contains("Name"))
                {
                    return Faker.NameFaker.Name();
                }

                if (name.Contains("Phone"))
                {
                    return Faker.PhoneFaker.Phone();
                }

                if (name.Contains("Email"))
                {
                    return Faker.InternetFaker.Email();
                }

                if (name.Contains("Url") || name.Contains("Uri"))
                {
                    return Faker.InternetFaker.Url();
                }

                if (name.Contains("Text") || name.Contains("Content"))
                {
                    return Faker.TextFaker.Sentences(3);
                }
            }
            return Rnd.String(name);
        }
    }
}
