using UnityEngine;

namespace WarOfTheParticles
{
    public abstract class BaseView<TController> : MonoBehaviour where TController : IController<TController>
    {
        protected TController controller;

        public TController Controller
        {
            get => controller;
            set => controller = value;
        }
    }
}