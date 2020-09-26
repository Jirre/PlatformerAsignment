using UnityEngine;

namespace UnityEditor.Tilemaps
{
    static internal partial class AssetCreation
    {
        [MenuItem("Assets/Create/2D/Brushes/Prefab Brush")]
        static void CreatePrefabBrush()
        {
            ProjectWindowUtil.CreateAsset(ScriptableObject.CreateInstance<PrefabBrush>(), "New Prefab Brush.asset");
        }
    }
}