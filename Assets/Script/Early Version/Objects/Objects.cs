using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    //[SerializeField] private string name;
    //[SerializeField] private ObjectTypes objectType;
    //[SerializeField] private string description;

    public virtual string Name { get; set; }
    public virtual ObjectTypes ObjectType { get; set; }
    public virtual void SetUpdate(float inGameSpeedMultiplier) { }
}


public enum ObjectTypes
{
    BUILDING,
    NPC,
    VEHICLE,
    VEGETATION,
    ANIMAL
}