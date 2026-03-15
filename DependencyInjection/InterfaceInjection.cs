

using System;

namespace DependencyInjection
{
    public interface ITool
    {
        void SetHammer(Hammer3 hammer3);
        void SetSaw(Saw3 saw3);
    }
    public class Hammer3
    {
        public void Hit()
        {
            Console.WriteLine("Hammer is hitting!");
        }
    }
    public class Saw3
    {
        public void Cut()
        {
            Console.WriteLine("Saw is cutting!");
        }
    }

    public class Builder3 : ITool
    {
        private Hammer3 _hammer3;
        private Saw3 _saw3;

        public void Build3()
        {
            _saw3.Cut();
            _hammer3.Hit();
            Console.WriteLine("Builder is building!");

        }

        public void SetHammer(Hammer3 hammer3)
        {
           _hammer3 = hammer3;
        }

        public void SetSaw(Saw3 saw3)
        {
            _saw3 = saw3;
        }
    }
    internal class InterfaceInjection
    {
        static void Main(string[] args)
        {
            Hammer3 hammer3 = new Hammer3();
            Saw3 saw3 = new Saw3();
            Builder3 builder3 = new Builder3();
            builder3.SetHammer(hammer3); // dependencies are set through interface methods, which is an example of interface injection.
            builder3.SetSaw(saw3);
            builder3.Build3();
            Console.ReadLine();
        }
    }
}
