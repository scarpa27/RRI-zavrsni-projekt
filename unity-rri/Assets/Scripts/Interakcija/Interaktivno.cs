using UnityEngine;

namespace Interact
{
    public class Interaktivno : MonoBehaviour
    {
        public float radius = 3f;
        public Transform trans;

        private bool _focus;

        private bool _hasInteracted;
        private Transform _player;


        private void Start()
        {
        }

        private void Update()
        {
            if (_focus)
            {
                var distance = Vector2.Distance(new Vector2(_player.position.x, _player.position.z),
                    new Vector2(trans.position.x, trans.position.z));
                if (!_hasInteracted && distance <= radius)
                {
                    _hasInteracted = true;
                    Interact();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (trans == null) trans = transform;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(trans.position, radius);
        }

        public void OnFocused(Transform playerTransform)
        {
            _focus = true;
            _hasInteracted = false;
            _player = playerTransform;
        }

        public void OnDefocused()
        {
            _focus = false;
            _hasInteracted = false;
            _player = null;
        }

        public virtual void Interact()
        {
        }
    }
}