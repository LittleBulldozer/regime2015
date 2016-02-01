using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AnchorManager : MonoBehaviour
{
    public delegate void NewAnchorCallback(Transform anchor);

    public class System<T>
    {
        public class SubscribeRef
        {
            public static implicit operator SubscribeRef(LinkedListNode<NewAnchorCallback> node)  // explicit byte to digit conversion operator
            {
                var subRef = new SubscribeRef();
                subRef.nodeRef = node;

                return subRef;
            }

            public LinkedListNode<NewAnchorCallback> nodeRef;
        }

        public void Register(T type, Transform anchor)
        {
            if (dict.ContainsKey(type))
            {
                dict.Remove(type);
            }

            dict.Add(type, anchor);

            LinkedList<NewAnchorCallback> callbackList;
            if (callbackDict.TryGetValue(type, out callbackList))
            {
                foreach (var callback in callbackList)
                {
                    callback(anchor);
                }
            }
        }

        public Transform GetAnchor(T type)
        {
            if (dict.ContainsKey(type) == false)
            {
                return null;
            }

            return dict[type];
        }

        public SubscribeRef Subscribe(T type, NewAnchorCallback anchorCallback)
        {
            LinkedList<NewAnchorCallback> callbackList;
            if (callbackDict.TryGetValue(type, out callbackList) == false)
            {
                callbackList = new LinkedList<NewAnchorCallback>();
                callbackDict.Add(type, callbackList);
            }

            return callbackList.AddFirst(anchorCallback);
        }

        public void Unsubscribe(T type, SubscribeRef subscribeRef)
        {
            LinkedList<NewAnchorCallback> callbackList;
            if (callbackDict.TryGetValue(type, out callbackList) == false)
            {
                throw new System.Exception("Callback list not found : " + type);
            }

            callbackList.Remove(subscribeRef.nodeRef);
            if (callbackList.Count == 0)
            {
                callbackDict.Remove(type);
            }
        }

        Dictionary<T, Transform> dict = new Dictionary<T, Transform>();
        Dictionary<T, LinkedList<NewAnchorCallback>> callbackDict = new Dictionary<T, LinkedList<NewAnchorCallback>>();
    }

    public System<AnchorType> Default = new System<AnchorType>();
    public System<string> Custom = new System<string>();

    public void Register(AnchorType type, Transform anchor)
    {
        if (type == AnchorType.CUSTOM)
        {
            Custom.Register(anchor.name, anchor);
        }
        else
        {
            Default.Register(type, anchor);
        }
    }

    void Awake()
    {
 //       Kernel.anchorManager = this;
    }

    #if UNITY_EDITOR
    void OnEnable()
    {
//        Kernel.anchorManager = this;
    }

    void Start()
    {
  //      Kernel.anchorManager = this;
    }
#endif

}
