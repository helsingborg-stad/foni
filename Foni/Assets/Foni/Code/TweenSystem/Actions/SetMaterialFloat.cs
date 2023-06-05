using UnityEngine;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent SetMaterialFloat(Material materialInstance, int shaderPropertyId)
        {
            return newValue => { materialInstance.SetFloat(shaderPropertyId, newValue); };
        }
    }
}