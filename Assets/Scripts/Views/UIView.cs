using UnityEngine;
using UnityEngine.UI;

namespace WarOfTheParticles
{
    public sealed class UIView : BaseView<UIController>
    {
        [SerializeField]
        private Text textComponent;
        
        private void Awake()
        {
            controller = new UIController(textComponent);
        }
    }
}