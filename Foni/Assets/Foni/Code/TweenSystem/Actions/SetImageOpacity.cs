using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent SetImageOpacity(Image image)
        {
            var initialColor = image.color;
            return newValue => image.color = new Color(initialColor.r, initialColor.g, initialColor.b, newValue);
        }
    }
}