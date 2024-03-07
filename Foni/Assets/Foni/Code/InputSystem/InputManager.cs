using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Foni.Code.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private Camera _raycastCamera;

        private void Start()
        {
            EnhancedTouchSupport.Enable();

#if UNITY_EDITOR
            TouchSimulation.Enable();
#endif
        }

        private void Update()
        {
            var activeTouches = Touch.activeTouches;
            foreach (var activeTouch in activeTouches)
            {
                ProcessTouch(activeTouch);
            }
        }

        private void EnsureRaycastCamera()
        {
            if (_raycastCamera != null) return;

            _raycastCamera = Camera.main;
        }

        private IInputListener RaycastTouchForListener(Touch touch)
        {
            EnsureRaycastCamera();
            var touchWorldPosition = _raycastCamera.ScreenToWorldPoint(touch.startScreenPosition);
            var raycastHit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

            return !raycastHit.collider ? null : raycastHit.collider.gameObject.GetComponent<IInputListener>();
        }

        private void ProcessTouch(Touch touch)
        {
            if (touch.began)
            {
                HandleInitialTouch(touch);
            }

            if (touch.ended)
            {
                HandleEndedTouch(touch);
            }
        }

        private void HandleInitialTouch(Touch touch)
        {
            var listenerComponent = RaycastTouchForListener(touch);
            listenerComponent?.OnPressDown();
        }

        private void HandleEndedTouch(Touch touch)
        {
            var listenerComponent = RaycastTouchForListener(touch);
            listenerComponent?.OnPressUp();
        }
    }
}