using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class CardAnimParam : MonoBehaviour
{
    [Range(0f, 1f)]
    public float yogi = 0f;

    public Vector3 origin = Vector3.zero;

    [SerializeField]
    Vector3 originalPos;

    void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            originalPos = transform.localPosition;
            EditorUtility.SetDirty(this);
        }
        else
#endif
        {
            transform.localPosition = originalPos;
        }
        yogi = 0f;
    }

    void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(originalPos, origin, yogi);
    }
}
