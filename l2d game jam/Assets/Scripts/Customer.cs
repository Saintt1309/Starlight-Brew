using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    public EffectsDB effectsDB;

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
    public int correctOrders;
    public TMP_Text wrongOrderText;
    public int wrongOrders;
    public List<EffectData> currentOrder;

    public enum OrderStatus
    {
        noOrder,
        inProgress
    }

    public OrderStatus orderStatus;

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
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            order();
            Debug.Log("generating order.");
        }

        difficultyStageText.text = "Difficulty Stage: " + difficultyStage;
        correctOrderText.text = "Correct Orders: " + correctOrders;
        wrongOrderText.text = "Wrong Orders: " + wrongOrders;

    }

    public void order()
    {
        int count;
        switch (difficultyStage)
        {
            case DifficultyStages.stage1:
                count = 1;
                break;
            case DifficultyStages.stage2:
                count = 2;
                break;
            case DifficultyStages.stage3:
                count = 3;
                break;
            default:
                count = 1; // fallback
                break;

        }
        orderStatus = OrderStatus.inProgress;

        currentOrder = GetRandomEffects(count);
        currentOrderText.text = "Current Order: " + string.Join(", ", currentOrder.ConvertAll(b => b.effectName));
        Debug.Log("current order count: " + currentOrder.Count);

        foreach (var e in currentOrder)
        {
            Debug.Log("Customer ordered: " + e.effectName);

        }

    }

    public List<EffectData> GetRandomEffects(int count)
    {
        List<EffectData> copy = new List<EffectData>(effectsDB.allEffects);
        List<EffectData> chosen = new List<EffectData>();

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int index = Random.Range(0, copy.Count);
            chosen.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return chosen;
    }

    
}
