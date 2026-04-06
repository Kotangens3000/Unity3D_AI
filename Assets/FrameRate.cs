using UnityEngine;
using TMPro;

public class FrameRate : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        textMeshPro.text = (int)fps + "";
    } 
}
