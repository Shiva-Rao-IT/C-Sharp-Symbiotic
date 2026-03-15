/*
namespace DependencyInjection
{

    public class Hammer2
    {
        public void Hit()
        {
            Console.WriteLine("Hammer is hitting!");
        }
    }
    public class Saw2
    {
        public void Cut()
        {
            Console.WriteLine("Saw is cutting!");
        }
    }

    public class Builder2
    {
        public Hammer2 hammer2 { get; set; }

        public Saw2 saw2 { get; set; }


        public void Build2()
        {
            saw2.Cut();
            hammer2.Hit();
            Console.WriteLine("Builder is building!");

        }
    }

    internal class SetterInjection
    {
        static void Main(string[] args)
        {
            Hammer2 hammer2 = new Hammer2();
            Saw2 saw2 = new Saw2();
            Builder2 builder2 = new Builder2();
            builder2.hammer2 = hammer2; // dependencies are set through properties, which is an example of setter injection.
            builder2.saw2 = saw2;
            builder2.Build2();
            Console.ReadLine();
        }

    }
}
*/
