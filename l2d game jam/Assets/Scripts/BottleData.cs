using UnityEngine;


public enum EffectTypes
{
    hot,
    cold,
    drowsy,
    energized,
    sweet,
    bitter,
    heightened,
    relaxed
    //add enums here im not creative enough for ts
    //ones I added are just sample enums to test the mixing system

}

[System.Serializable]
public struct effects
{
    public EffectTypes EffectType;
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
