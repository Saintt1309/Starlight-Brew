using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Analytics;
using Unity.VisualScripting;

public class Shaker : MonoBehaviour
{
    public GameObject DialogueUI;
    
    [Space(10)]
    private List<BottleData> bottles = new List<BottleData>();

    public TMP_Text shakerContentsText;
    public TMP_Text mixedDrinkText;
    public TMP_Text shakerStatusText;
    public TMP_Text previousOrderStatusText;

    public string currentlyMadeDrink;
    public Customer customer;

    public enum CompareResult
    {
        match,
        noMatch
    }
    public CompareResult compareResult;
    public CustomerAnim customerAnim;
    public AudioSource audioSource;
    public AudioClip serveClip;
    public AudioClip throwClip;
    public AudioClip mixClip;
    void Start()
    {
        shakerContentsText = GameObject.Find("shaker contents").GetComponent<TMP_Text>();
        mixedDrinkText = GameObject.Find("mixed drink").GetComponent<TMP_Text>();
        shakerStatusText = GameObject.Find("shaker status").GetComponent<TMP_Text>();
        previousOrderStatusText = GameObject.Find("previous order status").GetComponent<TMP_Text>();

        audioSource = GetComponent<AudioSource>();

        contentsDisplay();
        currentlyMadeDrink = null;
        Debug.Log("Shaker initialized");
    }

    public void OnMouseDown()
    {
        Debug.Log("Shaker clicked");

        Bottle selectedBottle = Bottle.currentlySelectedBottle;

        if (selectedBottle != null && currentlyMadeDrink == null)
        {
            addBottles(selectedBottle.bottleData);
            audioSource.PlayOneShot(selectedBottle.bottleData.clickSound);

            selectedBottle.Deselect();
            Bottle.currentlySelectedBottle = null;

            contentsDisplay();
        }
        else if (selectedBottle == null)
        {
            Debug.Log("No bottle selected.");
        }
        else
        {
            Debug.Log("Already mixed a drink. Empty first before adding new bottles.");
        }
    }

    public void addBottles(BottleData bottleData)
    {
        bottles.Add(bottleData);
        shakerStatusText.text = "Added: " + bottleData.bottleName;
        Debug.Log("Added " + bottleData.bottleName + " to shaker");
    }

    public void contentsDisplay()
    {
        if (bottles.Count > 0)
        {
            string ingredients = string.Join(", ", bottles.ConvertAll(b => b.bottleName));
            shakerContentsText.text = "Shaker Contents: " + ingredients;
            Debug.Log("Shaker now contains: " + ingredients);
        }
        else
        {
            shakerContentsText.text = "Shaker Contents: Empty";
            Debug.Log("Shaker is empty");
        }
    }

    public void emptyShaker()
    {
        bottles.Clear();
        currentlyMadeDrink = null;
        mixedDrinkText.text = "Mixed Drink: ";
        shakerStatusText.text = "Shaker emptied.";
        Debug.Log("Shaker cleared");
        contentsDisplay();

    }

    public void mixDrink()
    {
        if (bottles.Count < 1)
        {
            shakerStatusText.text = "Nothing to mix!";
            Debug.Log("Nothing to mix");
            return;
        }

        string identifiedDrink = IdentifyRecipe();

        if (identifiedDrink != "Unknown Drink")
        {
            audioSource.PlayOneShot(mixClip);
            currentlyMadeDrink = identifiedDrink;
            mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;
            shakerStatusText.text = "Successfully mixed: " + currentlyMadeDrink;
            Debug.Log("Successfully mixed: " + currentlyMadeDrink);
        }
        else
        {
            currentlyMadeDrink = "Unknown Drink";
            mixedDrinkText.text = "Mixed Drink: Unknown Drink";
            shakerStatusText.text = "No matching recipe found.";
            Debug.Log("No matching recipe.");
        }
    }

    private string IdentifyRecipe()
    {
        HashSet<string> currentIngredients = new HashSet<string>(bottles.ConvertAll(b => b.bottleName));

        foreach (var recipe in RecipeBook.recipes)
        {
            if (recipe.Key.SetEquals(currentIngredients))
                return recipe.Value;
        }

        return "Unknown Drink";
    }

    public void checkAction(string hitInfo)
    {
        switch (hitInfo)
        {
            case "Mix Area":
                mixDrink();

                Debug.Log("Mixing Drinks");
                break;

            case "Trash Area":
                if (bottles.Count > 0 || string.IsNullOrEmpty(currentlyMadeDrink) == false)
                {
                    audioSource.PlayOneShot(throwClip);
                }

                emptyShaker();
                Debug.Log("Throwing Drinks");
                break;
        }
    }

    public void serve()
    {
        if (customer == null)
        {
            shakerStatusText.text = "No customer linked!";
            Debug.LogError("No customer reference in shaker!");
            return;
        }

        if (string.IsNullOrEmpty(currentlyMadeDrink))
        {
            shakerStatusText.text = "Nothing to serve!";
            Debug.Log("There is nothing to serve (no mixed drink)");
            return;
        }

        if (customer.orderStatus == Customer.OrderStatus.noOrder)
        {
            shakerStatusText.text = "No customer order available!";
            Debug.Log("No customer order to compare to");
            return;
        }

        if (currentlyMadeDrink == customer.currentDrinkOrder)
        {
            audioSource.PlayOneShot(serveClip);
            compareResult = CompareResult.match;
            previousOrderStatusText.text = "Previous Order Status: Match";
            shakerStatusText.text = "Correct drink served!";
            Debug.Log("Correct Order!");

            customer.OrderResult(true);

            customerAnim.customerHappy();
            DialogueUI.SetActive(false);
            emptyShaker();

            customer.PlayHappyText();
        }
        else
        {
            audioSource.PlayOneShot(serveClip);
            compareResult = CompareResult.noMatch;
            previousOrderStatusText.text = "Previous Order Status: No Match";
            shakerStatusText.text = "Wrong drink served!";
            Debug.Log("Wrong Order!");

            customer.OrderResult(false);
            customerAnim.customerSad();
            DialogueUI.SetActive(false);
            emptyShaker();
            customer.PlaySadText();
        }
    }
}
