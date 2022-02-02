using Interact;
using Predmeti;

public class SkupiPredmet : Interaktivno
{
    public Predmet predmet;


    private void Start()
    {
        Ruksak.Instance.OnItemChangedCallback += Unisti;
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Ruksak.Instance.Add(predmet);
        Unisti();
    }

    private void Unisti()
    {
        Destroy(gameObject);
    }
}