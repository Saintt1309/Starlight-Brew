using UnityEngine;
using System.Collections.Generic;

public class RecipeBook : MonoBehaviour
{
    public static Dictionary<HashSet<string>, string> recipes = new Dictionary<HashSet<string>, string>(HashSet<string>.CreateSetComparer())
        {
            { new HashSet<string>{ "Water", "Syrup", "Ice" }, "Cold Syrup" },
            { new HashSet<string>{ "Tea", "Milk", "Ice"  }, "Iced Milk Tea" },
            { new HashSet<string>{ "Tea", "Milk", "Steamer" }, "Hot Milk Tea" },
            { new HashSet<string>{ "Coffee", "Chocolate", "Milk", "Ice" }, "Coffcolatte" },
            { new HashSet<string>{ "Coffee", "Chocolate", "Milk", "Steamer" }, "Hot Coffcolatte" },
            { new HashSet<string>{ "Coffee", "Water", "Steamer" }, "Pure Caffe" },
            { new HashSet<string>{ "Steamer", "Chocolate", "Milk" }, "Hot Choc" },
            { new HashSet<string>{ "Water", "Ice" }, "Cold Water" },
            { new HashSet<string>{ "Water", "Steamer" }, "Hot Water" },
            { new HashSet<string>{ "Water", "Tea", "Steamer" }, "Herbal Chamomile Tea" },
            { new HashSet<string>{ "Ice" }, "Cup of Ice" },
            { new HashSet<string>{ "Steamer" }, "Hot Cup" },
        };
    public Drinks drinks = new Drinks();
}

public class Drinks
{
    public string name;
    public int effectValue;
}
