using UnityEngine;

public class InvSucelje : MonoBehaviour
{
    public GameObject inventoryUI;
    public Transform itemsParent;

    private Ruksak _ruksak;

    private void Start()
    {
        _ruksak = Ruksak.Instance;
        _ruksak.OnItemChangedCallback += UpdateUI;
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Ruksak"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        var slots = GetComponentsInChildren<PoljeRuksak>();

        for (var i = 0; i < slots.Length; i++)
            if (i < _ruksak.items.Count)
                slots[i].AddItem(_ruksak.items[i]);
            else
                slots[i].ClearSlot();
    }
}