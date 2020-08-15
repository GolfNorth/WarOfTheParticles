using UnityEngine;

namespace WarOfTheParticles
{
    public sealed class RectangleView : BaseView<RectangleController>
    {
        private void Awake()
        {
            controller = new RectangleController();
        }

        private void OnMouseOver()
        {
            controller.DestroyCursor();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            controller.DestroyParticle(other.gameObject);
        }
    }
}