using UnityEngine;

public enum AnchorType
{
    INVALID = -1,
    CUSTOM,
    ENEMY_CENTER,
    ENEMY_LANDSCAPE_LOOK_TARGET,
    CASTER = 100,
    CASTER_FACE_SHOT,
    CASTER_PELVIS,
    CASTER_SPINE,
    CASTER_LEFT_HAND,
    CASTER_RIGHT_HAND,
    CASTER_ORIGIN,
    CASTER_HUD,
    CASTER_TURN_CENTER,
    CASTER_TURN_TARGET_ORIGIN,
    CASTER_TURN_CENTER_DESTINATION,
    TARGET = 200,
    TARGET_FACE_SHOT,
    TARGET_HUD,
    TARGET_PELVIS,
    TARGET_SPINE,
    TARGET_FIRE_HIT
}

public enum AnchorRelationshipType
{
    FOLLOW_TARGET,
    DONT_FOLLOW_TARGET
}

[ExecuteInEditMode]
public class Anchor : MonoBehaviour
{
    public AnchorType type;

    void OnEnable()
    {
        TryRegister();
    }

    void TryRegister()
    {
        /*
        if (Kernel.anchorManager == null)
        {
            Invoke("TryRegister", 0.5f);
            return;
        }

        Kernel.anchorManager.Register(type, transform);
        */
    }

#if UNITY_EDITOR
    void Update()
    {
        TryRegister();
    }
#endif
}