using UnityEngine;
using UnityEngine.UI;
public class healthhearth : MonoBehaviour
{
    public Sprite hearth5, hearth4, hearth3, hearth2, hearth1;
    Image hearth_image;

    private void Awake()
    {
        hearth_image = GetComponent<Image>();
    }
    public void setHearthImage(HearthStatus status)
    {
        switch (status)
        {   
            case HearthStatus.empty:
                hearth_image.sprite = hearth1;
                break;
            case HearthStatus.low:
                hearth_image.sprite = hearth2;
                break;
            case HearthStatus.dmg2:
                hearth_image.sprite = hearth3;
                break;
            case HearthStatus.dmg:
                hearth_image.sprite = hearth4;
                break;
            case HearthStatus.full:
                hearth_image.sprite = hearth5;
                break;
            
        }
    }
}
public enum HearthStatus
{
    empty = 1,
    full = 5,
    dmg = 4,
    dmg2 = 3,
    low = 2,
}