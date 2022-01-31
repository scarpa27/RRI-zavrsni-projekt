
using System.Linq;
using Predmeti;
using UnityEngine;

public static class ProvjeraIzvrsenosti
{
    
    public static bool Provjeri(int brZad)
    {
        return brZad switch
        {
            1 => ProvjeriQuest1(),
            2 => ProvjeriQuest2(),
            3 => ProvjeriQuest3(),
            _ => false
        };
    }

    private static bool ProvjeriQuest1()
    {
        const int potKol = 5;
        const string ime = "Jabuka";
        
        
        var kolicina = 0;
        
        Ruksak.Instance.items.ForEach(pr =>
        {
            if (pr.name.Equals(ime))
            {
                kolicina++;
            }
        });
        

        if (kolicina < potKol) return false;
        
        for (var i = 0; i < potKol; i++)
        {
            foreach (var t in Ruksak.Instance.items.Where(t => t.name.Equals(ime)))
            {
                Ruksak.Instance.Remove(t);
                break;
            }
        }

        Player.instance.playerStats.bodovi.AddModifier(300);
        return true;

    }

    private static bool ProvjeriQuest2()
    {
        const int potKol = 8;
        const string ime = "Gljiva";
        
        
        var kolicina = 0;
        
        Ruksak.Instance.items.ForEach(pr =>
        {
            if (pr.name.Equals(ime))
            {
                kolicina++;
            }
        });
        

        if (kolicina < potKol) return false;
        
        for (var i = 0; i < potKol; i++)
        {
            foreach (var t in Ruksak.Instance.items.Where(t => t.name.Equals(ime)))
            {
                Ruksak.Instance.Remove(t);
                break;
            }
        }

        Player.instance.playerStats.bodovi.AddModifier(300);
        return true;
    }

    private static bool ProvjeriQuest3()
    {
        if (GameObject.FindWithTag("BanditiMisija").transform.childCount != 0) return false;
        Player.instance.playerStats.bodovi.AddModifier(500);
        return true;

    }
    
    
    
    
    
    
}