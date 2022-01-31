using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZadajZadatak : MonoBehaviour
{


    public int brojZadatka;
    
    public Player igrac;
    public Zadatak zadatak;

    public GameObject prozor;
    public GameObject naslov;
    public GameObject opis;
    public GameObject nagrada;
    public Button botunPrihvati;
    public Button botunOdbij;


    public void Start()
    {
    }

    public void PonudiZadatak()
    {
        prozor.SetActive(true);
        naslov.GetComponent<TextMeshProUGUI>().SetText(zadatak.naslov.Replace("\\n","\n"));
        opis.GetComponent<TextMeshProUGUI>().SetText(zadatak.opis.Replace("\\n","\n"));
        nagrada.GetComponent<TextMeshProUGUI>().SetText((zadatak.nagrada+" xp").ToString().Replace("\\n","\n"));
        botunPrihvati.onClick.AddListener(PrihvatiZadatak);
        botunOdbij.onClick.AddListener(OdbijZadatak);
    }
    
    public void NijeRjeseno() {
        prozor.SetActive(true);
        naslov.GetComponent<TextMeshProUGUI>().SetText("Nije rješeno".Replace("\\n","\n"));
        opis.GetComponent<TextMeshProUGUI>().SetText("Nisu ispunjeni svi moji uvjeti!\nNemoj me razočarat!\n\nŽeliš li nastaviti sa zadatkom?".Replace("\\n","\n"));
        nagrada.GetComponent<TextMeshProUGUI>().SetText(zadatak.nagrada.ToString().Replace("\\n","\n"));
        botunPrihvati.onClick.AddListener(PrihvatiZadatak);
        botunOdbij.onClick.AddListener(OdbijZadatak);
        
    }

    public void JeRjeseno()
    {
        prozor.SetActive(true);
        naslov.GetComponent<TextMeshProUGUI>().SetText("Rješeno!".Replace("\\n","\n"));
        opis.GetComponent<TextMeshProUGUI>().SetText(zadatak.onRjeseno.Replace("\\n","\n"));
        nagrada.GetComponent<TextMeshProUGUI>().SetText(zadatak.nagrada.ToString().Replace("\\n","\n"));
        botunPrihvati.onClick.AddListener(() => prozor.SetActive(false));
        botunOdbij.enabled = false;
    }
    
    public void NemaZadatka()
    {
        prozor.SetActive(true);
        naslov.GetComponent<TextMeshProUGUI>().SetText("Rješeno!".Replace("\\n","\n"));
        opis.GetComponent<TextMeshProUGUI>().SetText("Pozdrav, prijatelju!\nHvala ti na pomoći\nNemam trenutno nikakav zadatak za tebe. U blizini ima lijepe prirode za razgledavat, samo se pazi vukova!".Replace("\\n","\n"));
        nagrada.GetComponent<TextMeshProUGUI>().SetText(zadatak.nagrada.ToString().Replace("\\n","\n"));
        botunPrihvati.onClick.AddListener(() => prozor.SetActive(false));
        botunOdbij.enabled = false;
    }

    private void PrihvatiZadatak()
    {
        prozor.SetActive(false);
        GetComponentInParent<NPCInteract>()._aktivanZadatak = true;
    }

    private void OdbijZadatak()
    {
        prozor.SetActive(false);
        GetComponentInParent<NPCInteract>()._aktivanZadatak = false;
        GetComponentInParent<NPCInteract>()._rjesenZadatak = false;
    }
    
    // public void ProtoZadatakSucelje(bool akt, bool rje)
    // {
    //     if (!akt && !rje) PonudiZadatak();
    //     if (akt  && !rje) ;
    //     if (!akt &&  rje) ;
    //     if (akt  &&  rje) ;
    //     
    // }
    //
    // public void ProtoPostaviSucelje(bool b1, string s1, string s2, string s3) {
    //     prozor.SetActive(b1);
    //     naslov.GetComponent<TextMeshProUGUI>().SetText(s1.Replace("\\n","\n"));
    //     opis.GetComponent<TextMeshProUGUI>().SetText(s2.Replace("\\n","\n"));
    //     nagrada.GetComponent<TextMeshProUGUI>().SetText(s3.ToString().Replace("\\n","\n"));
    //     botunPrihvati.onClick.AddListener(() => PrihvatiZadatak());
    //     botunOdbij.onClick.AddListener(() => OdbijZadatak());
    //     
    // }
}