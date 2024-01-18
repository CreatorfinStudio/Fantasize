using Definition;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class StatusSlot : MonoBehaviour, IItemStatusSlot
    {
        public StatusSlotInfo statusSlotInfo;

        public Image GetItemImage() => statusSlotInfo.ItemIcon;
        public Image GetArrowImage() => statusSlotInfo.ArrowIcon;


        public void SetItemImage(string spriteName)
        {
            Sprite loadedSprite = Resources.Load<Sprite>(spriteName);

            if (loadedSprite != null)
            {
                statusSlotInfo.ItemIcon.sprite = loadedSprite;
            }
        }
        public void SetArrowImage(string arrowIcon)
        {
            Sprite loadedSprite = Resources.Load<Sprite>("UI/" + arrowIcon);

            if (loadedSprite != null)
            {
                statusSlotInfo.ArrowIcon.sprite = loadedSprite;
            }
            else
            {
                Debug.LogError("Sprite not found: " + arrowIcon);
            }
        }

    }
}

