using System;

namespace ConsoleApp2
{
    class Program
    {
        public static event Action<string> GetEv;
        private static string _val;
        static void Main(string[] args)
        {
            ValTest v = new ValTest();
            Program.GetEv += ValTest.Program_GetEv;
            ValTest.changeString(Console.ReadLine());
            ValTest.changeString(Console.ReadLine());
            ValTest.changeString(Console.ReadLine());

        }

        public static string GetVal
        {
            get { return _val; }
            set
            {
                if (value.ToLower() == "test")
                {
                    GetEv?.Invoke(value);
                    _val = string.Empty;
                }
                else
                {
                    _val = value;
                }
            }
        }

       
    }

    public class ValTest
    {
        Program a = new Program();

        public ValTest()
        {
            Fluent x = new Fluent();
            x.AddID(12).AddValue().SetType(FluType.None);
            x.AddID(4).AddValue("dddd").SetType(FluType.NonValuable);
            Console.WriteLine(
            x.ToString());
        }

        public static void changeString(string str)
        {
            Program.GetVal = str;
        }

        public static void Program_GetEv(string obj)
        {
            Console.WriteLine($"string under test Value: {obj}");
        }
    }

    public class Fluent
    {
        protected int  ID { get; set; }
        protected string Value { get; set; }
        protected FluType Type { get; set; }

        public Fluent AddID(int id)
        {
            this.ID = id;
            return this;
        }

        public Fluent AddValue(string val=null)
        {
            Value = val;
            return this;
        }

        public Fluent SetType(FluType type)
        {
            Type = type;
            return this;
        }

        public override string ToString()
        {
            return $"ID: {ID}  Value: {Value}  Type: {Type.ToString()}";
        }
    }

    public enum FluType
    {
        None,
        Valuable,
        NonValuable,
        Object
    }

}
