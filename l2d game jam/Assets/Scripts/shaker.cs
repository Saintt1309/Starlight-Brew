using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Data.Common;

public class Shaker : MonoBehaviour
{
    private List<BottleData> bottles = new List<BottleData>();
    public BottleData bottleData;
    public TMP_Text shakerContentsText;
    public TMP_Text mixedDrinkText;
    public TMP_Text shakerStatusText;
    public TMP_Text previousOrderStatusText;
    public string currentlyMadeDrink;
    public Customer customer;

    public enum CompareResult
    {
        match,
        noMatch,
    }

    public CompareResult compareResult;

    void Start()
    {
        shakerContentsText = GameObject.Find("shaker contents").GetComponent<TMP_Text>();
        mixedDrinkText = GameObject.Find("mixed drink").GetComponent<TMP_Text>();
        shakerStatusText = GameObject.Find("shaker status").GetComponent<TMP_Text>();
        previousOrderStatusText = GameObject.Find("previous order status").GetComponent<TMP_Text>();
        contentsDisplay();

        currentlyMadeDrink = null;
    }
    public void OnMouseDown()
    {
        Debug.Log("Shaker clicked");

        Bottle selectedBottle = Bottle.currentlySelectedBottle;

        if (selectedBottle != null && currentlyMadeDrink == null)
        {
            addBottles(selectedBottle.bottleData);

            selectedBottle.Deselect();
            Bottle.currentlySelectedBottle = null;

            contentsDisplay();
        }
    }

    public void addBottles(BottleData bottleData)
    {
        bottles.Add(bottleData);
        Debug.Log("Added " + bottleData.bottleName + " to shaker");

    }

    public void contentsDisplay()
    {
        if (bottles.Count > 0)
        {
            shakerContentsText.text = "Shaker Contents: " + string.Join(", ", bottles.ConvertAll(b => b.bottleName));
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
        if (bottles.Count < 1)
        {
            Debug.Log("nothing to mix");
        }

        else
        {
           
                currentlyMadeDrink = string.Join(", ", getAllEffects());
                mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;
            
        }

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
                emptyShaker();
                currentlyMadeDrink = null;
                mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;

                Debug.Log("Throwing Drinks");
                break;
        }
    }

    public List<EffectData> getAllEffects()
    {
        List<EffectData> totalEffects = new List<EffectData>();

        foreach (var bottle in bottles)
        {
            foreach (var e in bottle.effects)
            {
                totalEffects.Add(e.effect); // only the EffectData reference
            }
        }

        return totalEffects;
    }

    public void compareOrder(List<EffectData> requestedEffects)
    {
        if (requestedEffects != null && requestedEffects.Count > 0)
        {
            if (string.IsNullOrEmpty(currentlyMadeDrink))
            {
                Debug.Log("There is nothing to serve (no mixed drinks)");
            }
            else
            {
                Debug.Log("Comparing order");

                List<EffectData> shakerEffects = getAllEffects();

                // Convert to sets (ignores duplicates, order doesn’t matter)
                HashSet<EffectData> shakerSet = new HashSet<EffectData>(shakerEffects);
                HashSet<EffectData> requestSet = new HashSet<EffectData>(requestedEffects);

                if (shakerSet.SetEquals(requestSet))
                {
                    compareResult = CompareResult.match;
                    previousOrderStatusText.text = "Previous Order Status: " + compareResult;
                    customer.correctOrders++;
                    Debug.Log("Correct Order!");

                    emptyShaker();
                    currentlyMadeDrink = null;
                    mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;

                    customer.order();
                } 

                else
                {
                    compareResult = CompareResult.noMatch;
                    previousOrderStatusText.text = "Previous Order Status: " + compareResult;
                    customer.wrongOrders++;
                    Debug.Log("Wrong Order!");
                }
            } 
        }

        else
        {
            customer.orderStatus = Customer.OrderStatus.noOrder;
            Debug.Log("There are no orders");
        }
    }

    public void serve()
    {
        if (customer.orderStatus != Customer.OrderStatus.noOrder)
        {
            compareOrder(customer.currentOrder);
        }
        else
        {
            Debug.Log("No order to serve to");
        }
        
    }
 
}
