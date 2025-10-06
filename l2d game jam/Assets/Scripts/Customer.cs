using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Customer : MonoBehaviour
{
    public RecipeBook recipeBook;
    public Shaker shaker;
    
    public GameObject DialogueUI;
    public GameObject TextUI;

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
    public TMP_Text greetingText;
    //public TMP_Text customerOrderDialogtext;

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
    [Header("Audio")]
    private AudioSource Audio;
    public AudioClip happySpeak;
    public AudioClip startSpeak;
    public AudioClip sadSpeak;

    public List<string> greetingList = new List<string> {
        "Hello, can I have a",
        "Hi, I'd like a",
        "Hey, let me get a"
        };

    public List<string> orderHappy = new List<string> {
        "Thank you so much.",
        "Yay, thanks.",
        "Oh my god, thank you."
        };

    public List<string> orderSad = new List<string> {
        "This isn't what I ordered.",
        "What is this?",
        "Um, no thank you."
        };

    void Start()
    {
        DialogueUI.SetActive(false);
        difficultyStage = DifficultyStages.stage1;
        orderStatus = OrderStatus.noOrder;

        correctOrders = 0;
        wrongOrders = 0;

        currentOrderText = GameObject.Find("customer order").GetComponent<TMP_Text>();
        difficultyStageText = GameObject.Find("difficulty stage").GetComponent<TMP_Text>();
        correctOrderText = GameObject.Find("correct orders").GetComponent<TMP_Text>();
        wrongOrderText = GameObject.Find("wrong orders").GetComponent<TMP_Text>();
        greetingText = GameObject.Find("Greeting").GetComponent<TMP_Text>();
        Audio = GetComponent<AudioSource>();

        difficultyStageText.text = "Difficulty Stage: " + difficultyStage;
        Debug.Log("Customer system initialized in " + gameObject.name);
    }

    public void OrderDrink()
    {
        Audio.PlayOneShot(startSpeak);
        TextUI.SetActive(true);
        DialogueUI.SetActive(true);

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

        string greeting = GenerateGreeting(greetingList);

        int index = Random.Range(0, allDrinks.Count);
        currentDrinkOrder = allDrinks[index];
        orderStatus = OrderStatus.inProgress;

        currentOrderText.text = "Customer Order: " + currentDrinkOrder;
        greetingText.text =$"{greeting} <wave>{currentDrinkOrder}</wave>.";

        Debug.Log("Customer ordered: " + currentDrinkOrder);
        customerAnim.nextCustomer();
        //Debug.Log("Called function to call next customer");
    }

    public void OrderResult(bool correct)
    {
        if (correct)
        {
            Audio.PlayOneShot(happySpeak);
            correctOrders++;
            Debug.Log("Customer received the correct drink!");
        }
        else
        {
            Audio.PlayOneShot(sadSpeak);
            wrongOrders++;
            Debug.Log("Customer received the wrong drink!");
        }
        TextUI.SetActive(false);
        correctOrderText.text = "Correct Orders: " + correctOrders;
        wrongOrderText.text = "Wrong Orders: " + wrongOrders;
    }

    public void OnMouseDown()
    {
        shaker.serve();

    }

    string GenerateGreeting(List<string> list)
    {
        if (list == null || list.Count == 0)
            return null;

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
    public void PlayHappyText()
    {
        string randomText = orderHappy[Random.Range(0, orderHappy.Count)];
        Debug.Log("Play Happy Text");
        greetingText.text = randomText;
    }

    public void PlaySadText()
    {
        string randomText = orderSad[Random.Range(0, orderSad.Count)];
        Debug.Log("Play Sad Text");
        greetingText.text = randomText;
    }

}
