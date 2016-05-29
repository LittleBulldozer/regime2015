using UnityEngine;

[ExecuteInEditMode]
public class ForceRenderQueue : MonoBehaviour
{

    public int renderQueue;

    Renderer ren;

    // Use this for initialization
    void Start()
    {
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ren.sharedMaterial.renderQueue = renderQueue;
    }
}
