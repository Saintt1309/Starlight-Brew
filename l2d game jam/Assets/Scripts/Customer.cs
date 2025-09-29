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
    public TMP_Text previousOrderStatusText;
    public TMP_Text difficultyStageText;

    void Start()
    {
        difficultyStage = DifficultyStages.stage1;

        currentOrderText = GameObject.Find("customer order").GetComponent<TMP_Text>();
        previousOrderStatusText = GameObject.Find("previous order status").GetComponent<TMP_Text>();
        difficultyStageText = GameObject.Find("difficulty stage").GetComponent<TMP_Text>();

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

        List<EffectData> requestedEffects = GetRandomEffects(count);
        currentOrderText.text = "Current Order: " + string.Join(", ", requestedEffects.ConvertAll(b => b.effectName));

        foreach (var e in requestedEffects)
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
