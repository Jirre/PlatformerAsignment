using UnityEngine;
using UnityEngine.UI;

namespace JVFramework.Collection
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    [AddComponentMenu("JVFramework/Conditionals/Listeners/Image Replacer")]
    internal class ConditionalImageReplacer : AConditionalListener
    {
        [SerializeField] private Sprite _TrueImage;
        [SerializeField] private Sprite _FalseImage;

        internal override void OnTrue() => GetComponent<Image>().sprite = _TrueImage;
        internal override void OnFalse() => GetComponent<Image>().sprite = _FalseImage;
    }
}
