/* namespace DependencyInjection
{

    public class Hammer1
    {
        public void Hit()
        {
            Console.WriteLine("Hammer is hitting!");
        }
    }
    public class Saw1
    {
        public void Cut()
        {
            Console.WriteLine("Saw is cutting!");
        }
    }

    public class Builder1
    {
        private Hammer1 _hammer1;
        private Saw1 _saw1;

        public Builder1(Hammer1 hammer1,Saw1 saw1)
        {
            _hammer1 = hammer1; 
            _saw1 = saw1;
        }

        public void Build1()
        {
            _saw1.Cut();
            _hammer1.Hit();
            Console.WriteLine("Builder is building!");
            
        }
    }

    internal class ConstructorInjection
    {
        static void Main(string[] args)
        {
            Hammer1 hammer1 = new Hammer1();
            Saw1 saw1 = new Saw1();
            Builder1 builder1 = new Builder1(hammer1, saw1);
            builder1.Build1();
            Console.ReadLine();
            // The Builder class receives its dependencies (Hammer and Saw) through its constructor, which is an example of constructor injection.
        }
    }
}
*/
