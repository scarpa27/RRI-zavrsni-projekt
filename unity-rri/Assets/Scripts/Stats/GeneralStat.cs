using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralStat
{
    [SerializeField] private float baseValue;

    private List<float> modifiers = new List<float>();


    public float GetValue()
    {
        var ret = baseValue;
        modifiers.ForEach(m => ret += m);
        return ret;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != 0f) modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0f) modifiers.Remove(modifier);
    }
}