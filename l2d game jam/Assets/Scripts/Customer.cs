using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    public RecipeBook recipeBook;
    public Shaker shaker;

    public enum DifficultyStages
    {
        stage1,
        stage2,
        stage3
    }
    public DifficultyStages difficultyStage;

    public TMP_Text currentOrderText;
    public TMP_Text difficultyStageText;
    public TMP_Text correctOrderText;
    public TMP_Text wrongOrderText;

    public int correctOrders;
    public int wrongOrders;

    public string currentDrinkOrder;

    public enum OrderStatus
    {
        noOrder,
        inProgress
    }
    public OrderStatus orderStatus;
    public CustomerAnim customerAnim;

    void Start()
    {
        difficultyStage = DifficultyStages.stage1;
        orderStatus = OrderStatus.noOrder;

        correctOrders = 0;
        wrongOrders = 0;

        currentOrderText = GameObject.Find("customer order").GetComponent<TMP_Text>();
        difficultyStageText = GameObject.Find("difficulty stage").GetComponent<TMP_Text>();
        correctOrderText = GameObject.Find("correct orders").GetComponent<TMP_Text>();
        wrongOrderText = GameObject.Find("wrong orders").GetComponent<TMP_Text>();

        difficultyStageText.text = "Difficulty Stage: " + difficultyStage;
        Debug.Log("Customer system initialized in " + gameObject.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            OrderDrink();
            Debug.Log("Generating new drink order.");
        }

        difficultyStageText.text = "Difficulty Stage: " + difficultyStage;
        correctOrderText.text = "Correct Orders: " + correctOrders;
        wrongOrderText.text = "Wrong Orders: " + wrongOrders;
    }

    public void OrderDrink()
    {
        if (recipeBook == null)
        {
            Debug.LogError("RecipeBook reference not set on Customer!");
            currentOrderText.text = "Customer Order: ERROR - No RecipeBook";
            return;
        }

        List<string> allDrinks = new List<string>(RecipeBook.recipes.Values);

        if (allDrinks.Count == 0)
        {
            Debug.LogWarning("No drinks found in recipe book!");
            currentOrderText.text = "Customer Order: No available drinks";
            return;
        }

        int index = Random.Range(0, allDrinks.Count);
        currentDrinkOrder = allDrinks[index];
        orderStatus = OrderStatus.inProgress;

        currentOrderText.text = "Customer Order: " + currentDrinkOrder;
        Debug.Log("Customer ordered: " + currentDrinkOrder);

        customerAnim.nextCustomer();
        Debug.Log("Called function to call next customer");
    }

    public void OrderResult(bool correct)
    {
        if (correct)
        {
            correctOrders++;
            Debug.Log("Customer received the correct drink!");
        }
        else
        {
            wrongOrders++;
            Debug.Log("Customer received the wrong drink!");
        }

        correctOrderText.text = "Correct Orders: " + correctOrders;
        wrongOrderText.text = "Wrong Orders: " + wrongOrders;
    }

    public void OnMouseDown()
    {
        shaker.serve();
        
    }


}
