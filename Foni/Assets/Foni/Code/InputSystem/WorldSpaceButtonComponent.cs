using UnityEngine;

namespace Foni.Code.InputSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class WorldSpaceButtonComponent : MonoBehaviour, IInputListener
    {
        public delegate void ClickEvent();

        public ClickEvent OnClicked;

        public void OnPressDown()
        {
        }

        public void OnPressUp()
        {
            OnClicked();
        }
    }
}