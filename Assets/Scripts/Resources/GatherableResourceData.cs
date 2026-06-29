using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class GatherableResourceData : UdonSharpBehaviour
{
    [SerializeField] private Transform woodLocation;
    
    public Transform GetResourceLocation(GatherableResourceType resourceType)
    {
        switch (resourceType)
        {
            case GatherableResourceType.Wood:
                return woodLocation;
            default:
                Debug.LogError("Unknown GatherableResourceType");
                // default to wood
                return woodLocation;
        }
    }
}
