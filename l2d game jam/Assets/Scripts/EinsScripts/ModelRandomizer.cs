using UnityEngine;
using Live2D.Cubism.Core;

public class ModelRandomizer : MonoBehaviour
{
    public CubismModel model;

    // Store references to parameters
    public CubismParameter param;
    public CubismParameter param2;
    public CubismParameter param3;
    public CubismParameter param4;

    private void Start()
    {
        model = GetComponent<CubismModel>();

        // Get parameters by name
        param = model.Parameters.FindById("Param");
        param2 = model.Parameters.FindById("Param2");
        param3 = model.Parameters.FindById("Param3");
        param4 = model.Parameters.FindById("Param4");
    }

    public void Randomize()
    {
        if (param != null)
            param.Value = Random.Range(param.MinimumValue, param.MaximumValue); // float

        if (param2 != null)
            param2.Value = Mathf.Round(Random.Range(param2.MinimumValue, param2.MaximumValue)); // int

        if (param3 != null)
            param3.Value = Mathf.Round(Random.Range(param3.MinimumValue, param3.MaximumValue)); // int

        if (param4 != null)
            param4.Value = Mathf.Round(Random.Range(param4.MinimumValue, param4.MaximumValue)); // int

        model.ForceUpdateNow();
    }

    // Let you modify manually
    public void SetParam(int value) => param.Value = value;
    public void SetParam2(float value) => param2.Value = value;
    public void SetParam3(int value) => param3.Value = value;
    public void SetParam4(int value) => param4.Value = value;
}