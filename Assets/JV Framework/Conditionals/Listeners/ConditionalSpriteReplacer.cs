using UnityEngine;

namespace JVFramework.Collection
{
    [AddComponentMenu("JVFramework/Conditionals/Listeners/Sprite Replacer")]
    [RequireComponent(typeof(SpriteRenderer))]
    internal class ConditionalSpriteReplacer : AConditionalListener
    {
        [SerializeField] private Sprite _OnTrueSprite;
        [SerializeField] private Sprite _OnFalseSprite;

        internal override void OnTrue()
        {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().sprite = _OnTrueSprite;
        }
        internal override void OnFalse()
        {
            if (GetComponent<SpriteRenderer>() != null)
                GetComponent<SpriteRenderer>().sprite = _OnFalseSprite;
        }
    }
}