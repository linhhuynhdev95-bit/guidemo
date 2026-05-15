using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public Sprite deactiveSpr;
    public Sprite activeSpr;
    
    public List<Image> images;

    public void OnClickBtn(int buttonId)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = buttonId != i ? activeSpr : deactiveSpr;
            images[i].color = buttonId == i ? new Color(1,0.5424528f,0.5424528f,1): Color.white;
        }
    }
}
