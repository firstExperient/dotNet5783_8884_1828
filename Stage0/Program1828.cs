namespace Stage0
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            welcome1828();
            welcome8884();
            Console.ReadKey();
        }

        static partial void welcome8884();
        private static void welcome1828()
        {
            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}