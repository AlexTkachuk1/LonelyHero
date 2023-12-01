using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fPSCountText;
    [SerializeField] private string _prefix = "FPS: ";
    private int _lastFrameIndex;
    private readonly float[] _frameDeltatimeArray = new float[50];

    private void Update()
    {
        _frameDeltatimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltatimeArray.Length;

        _fPSCountText.text = _prefix + Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in _frameDeltatimeArray)
        {
            total += deltaTime;
        }

        return _frameDeltatimeArray.Length / total;
    }
}
