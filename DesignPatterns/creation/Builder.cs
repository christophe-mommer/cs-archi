using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.creation
{
    public interface Ingredient { string Name { get; } }
    public interface IBread { string Name { get; } }
    public class Ham : Ingredient { public string Name => "Ham"; }
    public class Tomato : Ingredient { public string Name => "Tomato"; }
    public class Cheese : Ingredient { public string Name => "Cheese"; }
    public class Bacon : Ingredient { public string Name => "Bacon"; }
    public class Onion : Ingredient { public string Name => "Onion"; }
    public class CompleteBread : IBread { public string Name => "Complete bread"; }
    public class WhiteBread : IBread { public string Name => "White bread"; }
    public class Sandwich
    {
        public IBread Bread { get; }
        public IEnumerable<Ingredient> Ingredients { get; }
        public Sandwich(IBread bread, params Ingredient[] ingredients)
        {
            Ingredients = ingredients;
            Bread = bread;
        }
    }

    class ClassicSandichBuilder
    {
        public static Sandwich GetClassicSandwich()
            => new Sandwich(new WhiteBread(), new Ham(), new Tomato(), new Cheese());

        public static Sandwich GetBurger()
            => new Sandwich(new WhiteBread(), new Bacon(), new Tomato(), new Onion(), new Cheese());
    }
}
