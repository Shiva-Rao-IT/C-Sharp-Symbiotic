/*namespace DependencyInjection
{
    
    public class Hammer
    {
        public void Hit()
        {
            Console.WriteLine("Hammer is hitting!");
        }
    }
    public class Saw
    {
        public void Cut()
        {
            Console.WriteLine("Saw is cutting!");
        }
    }

    public class Builder
    {
        private Hammer _hammer;
        private Saw _saw;

        public Builder()
        {
            _hammer = new Hammer(); // dependency injection is not used here, as the Builder class is responsible for creating its own dependencies
            _saw = new Saw();
        }

        public void Build()
        {
            _hammer.Hit();
            _saw.Cut();
            Console.WriteLine("Builder is building!");
        }
    }

   
    internal class NoInjection
    {
        static void Main(string[] args)
        {
            Builder builder = new Builder();
            builder.Build();
            Console.ReadLine();
            // The Builder class creates its own dependencies (Hammer and Saw) without using dependency injection.
        }
    }
}
*/
