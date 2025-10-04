using UnityEngine;
using Live2D.Cubism.Core;

public class ModelRandomizer : MonoBehaviour
{
    private CubismModel model;

    // Store references to parameters
    private CubismParameter param;
    private CubismParameter param2;
    private CubismParameter param3;
    private CubismParameter param4;

    private void Start()
    {
        model = GetComponent<CubismModel>();

        // Get parameters by name
        param = model.Parameters.FindById("param");
        param2 = model.Parameters.FindById("param2");
        param3 = model.Parameters.FindById("param3");
        param4 = model.Parameters.FindById("param4");

        // Example: randomize once
        Randomize();
    }

    public void Randomize()
    {
        if (param != null)
            param.Value = Mathf.Round(Random.Range(param.MinimumValue, param.MaximumValue)); // int

        if (param2 != null)
            param2.Value = Random.Range(param2.MinimumValue, param2.MaximumValue); // float

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