using UnityEngine;

[CreateAssetMenu(fileName = "MaskSO", menuName = "Scriptable Objects/MaskSO")]
public class MaskSO : ScriptableObject
{
    public enum MaskEffect
    {
        NONE,
        Qwizard,
        PoesMask,

    }


    public string DisplayName;
    public GameObject WorldPrefab;

    public Sprite Icon;

    public MaskEffect Effect;
}
