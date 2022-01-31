using UnityEngine;

namespace Predmeti
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Ruksak/Predmet", order = 0)]
    public class Predmet : ScriptableObject
    {
        public new string name = "New Item";
        public Sprite icon;
        public bool prikazi = true;

        public virtual void Use()
        {
        }

        public void RemoveFromInv()
        {
            Ruksak.Instance.Remove(this);
        }
    }
}