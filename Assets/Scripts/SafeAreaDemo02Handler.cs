using UnityEngine;
using UnityEngine.UI;


public class SafeAreaDemo02Handler : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    void Start()
    {
        AdjustSafeArea();
    }

    void AdjustSafeArea()
    {

        if ((float)Screen.width / Screen.height > (16f / 9f))
        {
            canvasScaler.matchWidthOrHeight =  1;
        }
        else
        {
            canvasScaler.matchWidthOrHeight =  0;
        }
    }
}
