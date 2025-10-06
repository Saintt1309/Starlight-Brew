using UnityEngine;
using System.Collections.Generic;

public class RecipeBook : MonoBehaviour
{

    [Header("Main Recipe Book UI")]
    public GameObject recipeBook;
    
    [Header("Child Panels")]
    public GameObject recipeToggle;
    public GameObject ingredientsToggle;

    [Header("Optional")]
    public GameObject panel;

    private bool isRecipeBookActive = false;
    private bool showingRecipes = false;
    private bool showingIngredients = false;

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

    void Start()
    {
        // Ensure everything starts hidden
        HideAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            bool isShowingRecipes = recipeBook.activeSelf && recipeToggle.activeSelf && !ingredientsToggle.activeSelf;

            if (isShowingRecipes)
            {
                recipeBook.SetActive(false);
                panel.SetActive(false);
                recipeToggle.SetActive(false);
                ingredientsToggle.SetActive(false);
            }
            else
            {

                recipeBook.SetActive(true);
                panel.SetActive(true);
                recipeToggle.SetActive(true);
                ingredientsToggle.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isShowingIngredients = recipeBook.activeSelf && ingredientsToggle.activeSelf && !recipeToggle.activeSelf;

            if (isShowingIngredients)
            {
                recipeBook.SetActive(false);
                panel.SetActive(false);
                recipeToggle.SetActive(false);
                ingredientsToggle.SetActive(false);
            }
            else
            {
                recipeBook.SetActive(true);
                panel.SetActive(true);
                recipeToggle.SetActive(false);
                ingredientsToggle.SetActive(true);
            }
        }
    }

    void ShowRecipeSection()
    {
        isRecipeBookActive = true;
        showingRecipes = true;
        showingIngredients = false;

        recipeBook.SetActive(true);
        if (panel != null) panel.SetActive(true);

        recipeToggle.SetActive(true);
        ingredientsToggle.SetActive(false);
    }

    void ShowIngredientsSection()
    {
        isRecipeBookActive = true;
        showingIngredients = true;
        showingRecipes = false;

        recipeBook.SetActive(true);
        if (panel != null) panel.SetActive(true);

        recipeToggle.SetActive(true);
        ingredientsToggle.SetActive(true);
    }

    void HideAll()
    {
        isRecipeBookActive = false;
        showingRecipes = false;
        showingIngredients = false;

        recipeBook.SetActive(false);
        if (panel != null) panel.SetActive(false);

        recipeToggle.SetActive(false);
        ingredientsToggle.SetActive(false);
    }
    
}

public class Drinks
{
    public string name;
    public int effectValue;
}


