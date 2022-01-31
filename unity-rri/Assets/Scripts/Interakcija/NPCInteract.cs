using System.Collections;
using Interact;
using UnityEngine;

public class NPCInteract : Interaktivno
{
    private bool _zadajemZadatke;
    public bool _rjesenZadatak;
    public bool _aktivanZadatak;
    private bool _prosloDovoljnoVremena;

    private int brZad;

    private void Start()
    {
        _zadajemZadatke = GetComponentInParent<ZadajZadatak>() != null;
        if (_zadajemZadatke) brZad = GetComponentInParent<ZadajZadatak>().brojZadatka;
    }
    
    public override void Interact()
    {
        if (_zadajemZadatke)
        {
            if (!_aktivanZadatak && !_rjesenZadatak) Ponudi();
            if (_aktivanZadatak  && !_rjesenZadatak) Provjeri();
            if (!_aktivanZadatak && _rjesenZadatak)
                if (_prosloDovoljnoVremena) Odbij(); else GetComponentInParent<ZadajZadatak>().JeRjeseno();;
            if (_aktivanZadatak  && _rjesenZadatak)  _aktivanZadatak = false;
        }
    }

    private void Ponudi()
    {
        GetComponentInParent<ZadajZadatak>().PonudiZadatak();
        
    }

    private void Provjeri()
    {
        if (!ProvjeraIzvrsenosti.Provjeri(brZad))
        {
            GetComponentInParent<ZadajZadatak>().NijeRjeseno();
            return;
        }
        _rjesenZadatak = true;
        _aktivanZadatak = false;
        GetComponentInParent<ZadajZadatak>().JeRjeseno();
        StartCoroutine(ZahvaliOdbij());
    }

    private void Odbij()
    {
        GetComponentInParent<ZadajZadatak>().NemaZadatka();
    }

    private IEnumerator ZahvaliOdbij()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            _prosloDovoljnoVremena = true;
            yield break;
        }
    }
    
}