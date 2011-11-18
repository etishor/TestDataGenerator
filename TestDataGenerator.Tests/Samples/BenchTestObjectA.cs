using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class BenchTestObjectA : IAssertEquality
    {
        public static BenchTestObjectA CreateInstance()
        {
            return CreateChildInstance(0);
        }
        
        public static BenchTestObjectA CreateChildInstance(int level)
        {
            BenchTestObjectA instance = new BenchTestObjectA();

            instance.StringData = string.Format("test string {0}",level);
			instance.Integer = 100;
            List<string> data = new List<string>();
            for (int i = 0; i < 5; i++)
            {
				data.Add(string.Format("other test string {0}", level));
            }
            instance.Data = data.ToArray();
            
            if (level < 3)
            {
                List<BenchTestObjectA> children = new List<BenchTestObjectA>();
                for (int i = 0; i < 9; i++)
                {
                    children.Add(BenchTestObjectA.CreateChildInstance(level + 1));
                }
                instance.Children = children.ToArray();

            }
            return instance;
        }

       
       
        public string StringData { get; set; }

       
       
        public string[] Data { get; set; }

       
       
        public BenchTestObjectA[] Children { get; set; }

		
		
		public int Integer { get; set; }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<BenchTestObjectA>(other);
            BenchTestObjectA target = other as BenchTestObjectA;

            Assert.AreEqual(this.StringData, target.StringData);
            Assert.AreEqual(this.Data, target.Data);

            if (this.Children != null && target.Children != null)
            {
                Assert.AreEqual(this.Children.Count(), target.Children.Count());
                for (int i = 0; i < this.Children.Count(); i++)
                {
                    this.Children[i].AssertEquality(target.Children[i]);
                }
            }
        }
    }
}
