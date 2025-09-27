using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Shaker : MonoBehaviour
{
    private List<string> bottles = new List<string>();
    public TMP_Text shakerContentsText;
    public TMP_Text mixedDrinkText;

    void Start()
    {
        shakerContentsText = GameObject.Find("shaker contents").GetComponent<TMP_Text>();
        mixedDrinkText = GameObject.Find("mixed drink").GetComponent<TMP_Text>();
        contentsDisplay();
    }

    public void OnMouseDown()
    {
        Debug.Log("Shaker clicked");

        Bottle selectedBottle = Bottle.currentlySelectedBottle;

        if (selectedBottle != null)
        {
            bottles.Add(selectedBottle.gameObject.name);
            Debug.Log("Added " + selectedBottle.gameObject.name + " to shaker");

            selectedBottle.Deselect();
            Bottle.currentlySelectedBottle = null;

            contentsDisplay();
        }
        else
        {
            Debug.Log("No bottle selected");
        }
    }

    public void contentsDisplay()
    {
        if (bottles.Count > 0)
        {
            shakerContentsText.text = "Shaker Contents: " + string.Join(", ", bottles);
        }
        else
        {
            shakerContentsText.text = "Shaker Contents: Empty";
        }
    }

    public void emptyShaker()
    {
        bottles.Clear();
        contentsDisplay();
        Debug.Log("Shaker cleared");
    }

    public void mixDrink()
    {
        if (bottles.Count < 2)
        {
            Debug.Log("2 or more bottles are needed to mix duh");
        }

        else
        {
            HashSet<string> shakerSet = new HashSet<string>(bottles);

            if (RecipeBook.recipes.TryGetValue(shakerSet, out string drink))
            {
                mixedDrinkText.text = "You made: " + drink;
                Debug.Log("Made " + drink);
                emptyShaker();
            }
            else
            {
                mixedDrinkText.text = "Made Bad Drink (no recipe found)";
                Debug.Log("Made Bad Drink");
                emptyShaker();
            }
        }
    }
}
