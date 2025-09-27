using UnityEngine;
using System.Collections.Generic;

public class RecipeBook : MonoBehaviour
{
    public static Dictionary<HashSet<string>, string> recipes = new Dictionary<HashSet<string>, string>(HashSet<string>.CreateSetComparer())
        {
            { new HashSet<string>{ "Bottle 1", "Bottle 2" }, "Drink 12" },
            { new HashSet<string>{ "Bottle 1", "Bottle 3" }, "Drink 13" },
            { new HashSet<string>{ "Bottle 2", "Bottle 3" }, "Drink 23" }
        };
}
