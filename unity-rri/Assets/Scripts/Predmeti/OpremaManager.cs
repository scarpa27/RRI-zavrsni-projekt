using System;
using UnityEngine;

public class OpremaManager : MonoBehaviour
{
    // Callback for when an item is equipped
    public delegate void OnEquipmentChanged(Oprema newItem, Oprema oldItem);

    public Oprema[] defaultWear;

    public SkinnedMeshRenderer targetMesh;

    private Oprema[] currentEquipment;
    private SkinnedMeshRenderer[] currentMeshes;

    private Ruksak inventory;

    private void Start()
    {
        inventory = Ruksak.Instance;

        var numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Oprema[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipAllDefault();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
    }

    public event OnEquipmentChanged onEquipmentChanged;


    public Oprema GetEquipment(EquipmentSlot slot)
    {
        return currentEquipment[(int) slot];
    }

    // Equip a new item
    public void Equip(Oprema newItem)
    {
        Oprema oldItem = null;

        // Find out what slot the item fits in
        // and put it there.
        var slotIndex = (int) newItem.equipSlot;

        // If there was already an item in the slot
        // make sure to put it back in the inventory
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];

            inventory.Add(oldItem);
        }

        // An item has been equipped so we trigger the callback
        onEquipmentChanged?.Invoke(newItem, oldItem);

        currentEquipment[slotIndex] = newItem;
        Debug.Log(newItem.name + " equipped!");

        if (newItem.prefab) AttachToMesh(newItem.prefab, slotIndex);
        //equippedItems [itemIndex] = newMesh.gameObject;
    }

    private void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            var oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            if (currentMeshes[slotIndex] != null) Destroy(currentMeshes[slotIndex].gameObject);


            // Equipment has been removed so we trigger the callback
            onEquipmentChanged?.Invoke(null, oldItem);
        }
    }

    private void UnequipAll()
    {
        for (var i = 0; i < currentEquipment.Length; i++) Unequip(i);
        EquipAllDefault();
    }

    private void EquipAllDefault()
    {
        foreach (var e in defaultWear) Equip(e);
    }

    private void AttachToMesh(SkinnedMeshRenderer mesh, int slotIndex)
    {
        if (currentMeshes[slotIndex] != null) Destroy(currentMeshes[slotIndex].gameObject);

        var newMesh = Instantiate(mesh);
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    #region Singleton

    public static OpremaManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<OpremaManager>();
            return _instance;
        }
    }

    private static OpremaManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    #endregion
}