using UnityEngine.UI;

public interface IItemStatusSlot
{
    public Image GetItemImage();
    public Image GetArrowImage();

    public void SetItemImage(string spriteName);
    public void SetArrowImage(string spriteName);
}