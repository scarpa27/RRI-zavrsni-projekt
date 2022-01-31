using Predmeti;
using UnityEngine;
using UnityEngine.UI;

public class PoljeRuksak : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    private Predmet _predmet;

    public void AddItem(Predmet novi)
    {
        _predmet = novi;

        icon.sprite = _predmet.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        _predmet = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void RemoveItemFromInventory()
    {
        Ruksak.Instance.Remove(_predmet);
        var objekt = new GameObject();
    }

    public void UseItem()
    {
        if (_predmet != null) _predmet.Use();
    }
}