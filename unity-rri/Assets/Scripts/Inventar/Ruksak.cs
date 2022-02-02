using System.Collections.Generic;
using Predmeti;
using UnityEngine;

public class Ruksak : MonoBehaviour
{
    public delegate void OnItemChanged();

    public int space = 30;

    public List<Predmet> items = new List<Predmet>();
    public OnItemChanged OnItemChangedCallback;

    public void Add(Predmet predmet)
    {
        if (predmet.prikazi)
        {
            if (items.Count >= space) return;

            items.Add(predmet);
            OnItemChangedCallback?.Invoke();
        }
    }

    public void Remove(Predmet predmet)
    {
        items.Remove(predmet);

        OnItemChangedCallback?.Invoke();
    }

    #region Static

    public static Ruksak Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
}