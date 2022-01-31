using System;
using UnityEngine;

[Serializable]
public class Zadatak
{
    public bool aktivan;
    public Transform lokacija;
    public string naslov;
    public string opis;
    public int nagrada;

    public string onRjeseno;
}