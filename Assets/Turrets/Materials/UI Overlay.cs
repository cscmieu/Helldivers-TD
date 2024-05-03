using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Turrets.Materials
{
    public sealed class WorldSpaceOverlayUI : MonoBehaviour
    {
        private const            string                                shaderTestMode      = "unity_GUIZTestMode";
        [SerializeField] private UnityEngine.Rendering.CompareFunction desiredUIComparison = UnityEngine.Rendering.CompareFunction.Always;
        [Tooltip("Set to blank to automatically populate from the child UI elements")]
        [SerializeField] private Graphic[] uiElementsToApplyTo;

        //Allows us to reuse materials
        private readonly        Dictionary<Material, Material> _materialMappings = new Dictionary<Material, Material>();
        private static readonly int                            unityGuizTestMode = Shader.PropertyToID(shaderTestMode);

        private void Start()
        {
            if (uiElementsToApplyTo.Length == 0)
            {
                uiElementsToApplyTo = gameObject.GetComponentsInChildren<Graphic>();
            }

            foreach (var graphic in uiElementsToApplyTo)
            {
                Material material = graphic.materialForRendering;
                if (material == null)
                {
                    Debug.LogError($"{nameof(WorldSpaceOverlayUI)}: skipping target without material {graphic.name}.{graphic.GetType().Name}");
                    continue;
                }

                if (!_materialMappings.TryGetValue(material, out var materialCopy))
                {
                    materialCopy = new Material(material);
                    _materialMappings.Add(material, materialCopy);
                }

                materialCopy.SetInt(unityGuizTestMode, (int)desiredUIComparison);
                graphic.material = materialCopy;
            }
        }
    }
}