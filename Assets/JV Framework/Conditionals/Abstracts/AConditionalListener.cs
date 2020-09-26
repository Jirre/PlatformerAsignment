namespace JVFramework.Collection
{
    internal abstract class AConditionalListener : UnityEngine.MonoBehaviour
    {
        internal ConditionalManager Manager { get; private set; }
        internal void SetManager(ConditionalManager pManager) =>
            Manager = pManager;

        internal abstract void OnTrue();
        internal abstract void OnFalse();
    }
}
