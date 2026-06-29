
using System;
using System.Collections;
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Enums;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PlayerWorkerUnit : UdonSharpBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Transform inventoryStorageLocation;
    [SerializeField] private float gatherTimeMultiplier = 1f;
    private NavMeshAgent _agent;
    private GatherableResourceType _gatheredResource;
    private VRCTweenHandle _gatherOperation;
    private Transform _gatherTarget;
    private bool _isHoldingResource;
    // I'm not sure if I'll want it to be incremental
    private const int GatheredResourceAmount = 1;
    // remove this
    [SerializeField] private Transform testing;


    public void SetGatherTarget(Transform target)
    {
        _gatherTarget = target;
        SetTarget(_gatherTarget);
    }
    
    // there must be a better way to do this..?
    public void ReceiveResource(GatherableResourceType resource, float gatherTime)
    {
        float finalGatherTime = gatherTime * gatherTimeMultiplier;
        _gatheredResource = resource;
        Debug.Log($"Starting to gather {GatherableResourceTypeExtensions.GetName(_gatheredResource)}");
        _gatherOperation = VRCTween.DelayedCall(this, nameof(StoreCurrentResource), finalGatherTime);
    }

    public void StoreCurrentResource()
    {
        _isHoldingResource = true;
        Debug.Log("Walking to storage");
        SetTarget(inventoryStorageLocation);
    }
    
    public void DropResource()
    {
        if (!_isHoldingResource) return;
        Debug.Log($"Storing {GatheredResourceAmount} {GatherableResourceTypeExtensions.GetName(_gatheredResource)}");
        inventory.AddItems(_gatheredResource, GatheredResourceAmount);
        _isHoldingResource = false;
        Debug.Log($"Walking to {GatherableResourceTypeExtensions.GetName(_gatheredResource)}");
        SetTarget(_gatherTarget);
    }
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void SetTarget(Transform target)
    {
        if (_gatherOperation.IsActive)
        {
            _gatherOperation.Kill();
        }
        _agent.SetDestination(target.position);
    }
}
