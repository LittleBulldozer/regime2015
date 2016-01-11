using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SpawnPosition : MonoBehaviour
{
    public AnchorType anchorType;
    public string customAnchorName;
    public AnchorRelationshipType anchorRelationshipType;

    public Vector3 relativePosition;
    public Vector3 relativeDirection;

    public Transform GetAnchor()
    {
        return cachedAnchor;
    }
    
#if UNITY_EDITOR
    void CalcTransformFromAnchor()
    {
        if (cachedAnchor == null)
        {
            throw new System.Exception("No anchor!");
        }
        
        relativePosition = cachedAnchor.InverseTransformPoint(transform.position);
        relativeDirection = cachedAnchor.InverseTransformDirection(transform.forward);
    }

    public void StickToAnchor()
    {
        Undo.RecordObject(transform, "Transform...");
        transform.position = cachedAnchor.position;
        transform.rotation = cachedAnchor.rotation;
    }

    public void Reset()
    {
        OnDisable();
        OnEnable();
    }

    void Update()
    {
        if (cachedAnchor != null)
        {
            CalcTransformFromAnchor();
        }
        else
        {
            relativePosition = relativeDirection = Vector3.zero;
        }
    }
#endif

    Transform cachedAnchor;
    System.Action unsubscribeAction;

    void TrySubscribe()
    {
        if (Kernel.anchorManager == null)
        {
            Invoke("TrySubscribe", 0.5f);
            return;
        }

        if (anchorType == AnchorType.CUSTOM)
        {
            SubscribeAnchor(customAnchorName, Kernel.anchorManager.Custom);
        }
        else
        {
            SubscribeAnchor(anchorType, Kernel.anchorManager.Default);
        }
    }

    void OnEnable()
    {
        TrySubscribe();
    }

    void OnDisable()
    {
        if (unsubscribeAction != null)
        {
            unsubscribeAction();
            unsubscribeAction = null;
        }

        cachedAnchor = null;
    }

    void SubscribeAnchor<T>(T key, AnchorManager.System<T> system)
    {
        var subscribeRef = system.Subscribe(key, SetAnchor);
        unsubscribeAction += () =>
        {
            system.Unsubscribe(key, subscribeRef);
        };

        var anchor = system.GetAnchor(key);
        if (anchor != null)
        {
            SetAnchor(anchor);
        }
    }

    void SetAnchor(Transform newAnchor)
    {
        transform.position = newAnchor.TransformPoint(relativePosition);
        transform.rotation = Quaternion.LookRotation(newAnchor.TransformDirection(relativeDirection));

        cachedAnchor = newAnchor;
    }
}
