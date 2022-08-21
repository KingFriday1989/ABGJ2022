using DoubleAgent.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Game
{
    public sealed class Bomb : Core.Behaviour
    {
        [SerializeField] private MeshRenderer rend;
        [SerializeField] private UnityEvent _OnExplode;

        private void OnCollisionEnter(Collision collision)
        {
            //if(collision.gameObject.CompareTag(Constants.TAG_GROUND))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                rend.enabled = false;
                _OnExplode?.Invoke();
            }
        }
    }
}