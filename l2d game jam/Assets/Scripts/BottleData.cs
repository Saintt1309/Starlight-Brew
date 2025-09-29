using UnityEngine;

[System.Serializable]
public struct effects
{
    public EffectData effect;
    public float effectValue;
}

[CreateAssetMenu(fileName = "Bottles", menuName = "Scriptable Objects/Bottles")]
public class BottleData : ScriptableObject
{
    [Header("Stats")]
    public string bottleName;

    public effects[] effects;

    public Sprite texture;
}
