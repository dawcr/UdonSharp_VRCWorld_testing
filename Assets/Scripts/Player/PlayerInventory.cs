using System.Text;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PlayerInventory : UdonSharpBehaviour
{
    // TODO: find a way better way to trigger panel update
    [SerializeField] private Panel panel;
    // could be a struct (string name, int amount) but Udon# hates my attempts
    private readonly int[] _gatheredResources = new int[(int)GatherableResourceType.LENGTH];
    
    public string GetInventoryContentDescription()
    {
        StringBuilder builder = new StringBuilder();
        for (var i = 0; i < _gatheredResources.Length; i++)
        {
            builder.AppendLine($"{GatherableResourceTypeExtensions.GetName(i)} amount: {_gatheredResources[i]}");
        }
        return builder.ToString();
    }

    public void AddItems(GatherableResourceType resource, int amount)
    {
        _gatheredResources[(int)resource] += amount;
        Debug.Log($"Added {amount} {GatherableResourceTypeExtensions.GetName(resource)} to inventory");
        panel.UpdateResourcesDescription();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Networking.IsOwner(other.gameObject))
        {
            Debug.Log("Not owner");
            return;
        }
        
        PlayerWorkerUnit worker = other.GetComponent<PlayerWorkerUnit>();
        if (worker == null) return;
        
        worker.DropResource();
    }
}
