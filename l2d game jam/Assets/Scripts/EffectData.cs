using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "Scriptable Objects/EffectData")]
public class EffectData : ScriptableObject
{
    public string effectName;
    public float effectValue;
    public Sprite effectIcon;
}
