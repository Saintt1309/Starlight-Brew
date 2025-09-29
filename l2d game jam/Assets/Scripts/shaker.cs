using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Shaker : MonoBehaviour
{
    private List<BottleData> bottles = new List<BottleData>();
    public BottleData bottleData;
    public TMP_Text shakerContentsText;
    public TMP_Text mixedDrinkText;
    public TMP_Text shakerStatusText;
    public string currentlyMadeDrink;

    private ShakerStatus shakerStatus;

    enum ShakerStatus
    {
        open,
        closed
    }

    void Start()
    {
        shakerContentsText = GameObject.Find("shaker contents").GetComponent<TMP_Text>();
        mixedDrinkText = GameObject.Find("mixed drink").GetComponent<TMP_Text>();
        shakerStatusText = GameObject.Find("shaker status").GetComponent<TMP_Text>();
        contentsDisplay();

        currentlyMadeDrink = null;
        shakerStatus = ShakerStatus.open;
        shakerStatusText.text = "Shaker Status: " + shakerStatus;
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
            if (shakerStatus != ShakerStatus.closed)
            {
                Debug.Log("Gotta close the cap first!");
            }
            else
            {
                currentlyMadeDrink = string.Join(", ", GetAllEffects());
                mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;
                emptyShaker();
            }
        }
            
    }

    public void closeShaker()
    {
        if (shakerStatus == ShakerStatus.closed)
        {
            Debug.Log("Shaker is Already Closed");

        }

        else
        {
            if (bottles.Count < 1)
            {
                Debug.Log("The shaker is empty");
            }
            else
            {
                shakerStatus = ShakerStatus.closed;
                shakerStatusText.text = "Shaker Status: " + shakerStatus;
                Debug.Log("Closing Shaker");
            }
        }
    }

    public void checkAction(string hitInfo)
    {
        switch (hitInfo)
        {
            case "Mix Area":
                mixDrink();
                shakerStatus = ShakerStatus.open;
                shakerStatusText.text = "Shaker Status: " + shakerStatus;

                Debug.Log("Mixing Drinks");
                break;

            case "Trash Area":
                emptyShaker();
                currentlyMadeDrink = null;
                shakerStatus = ShakerStatus.open;
                shakerStatusText.text = "Shaker Status: " + shakerStatus;
                mixedDrinkText.text = "Mixed Drink: " + currentlyMadeDrink;

                Debug.Log("Throwing Drinks");
                break;
        }
    }
    
    public List<EffectData> GetAllEffects()
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

    public void compareOrder()
    {

    }

    
}
