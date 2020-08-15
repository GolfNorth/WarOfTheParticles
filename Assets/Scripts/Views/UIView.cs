using UnityEngine;
using UnityEngine.UI;

namespace WarOfTheParticles
{
    public sealed class UIView : BaseView<UIController>
    {
        [SerializeField]
        private Text textComponent;
        [SerializeField]
        private Text scoreComponent;
        
        private void Awake()
        {
            controller = new UIController(textComponent, scoreComponent);
        }
    }
}