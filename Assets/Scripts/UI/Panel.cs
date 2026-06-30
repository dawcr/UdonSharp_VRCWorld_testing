using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Panel : UdonSharpBehaviour
{
    [SerializeField] private TMP_Text resourcesDescription;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GatherableResourceData gatherableResourceData;
    [SerializeField] private GameObject resourcesTab;
    [SerializeField] private GameObject shopTab;
    
    private PlayerWorkerUnit _playerWorker;
    private bool _showShopTab;
    
    // not sure how to trigger an update when panel is activated so using this for now
    public void UpdateResourcesDescription()
    {
        resourcesDescription.text = playerInventory.GetInventoryContentDescription();
    }

    public void SetGatherLocationWood()
    {
        _playerWorker.SetGatherTarget(gatherableResourceData.GetResourceLocation(GatherableResourceType.Wood));
    }

    public void SetGatherLocationStone()
    {
        _playerWorker.SetGatherTarget(gatherableResourceData.GetResourceLocation(GatherableResourceType.Stone));
    }

    public void SwitchTab()
    {
        _showShopTab = !_showShopTab;
        UpdateActiveTab();
    }

    private void UpdateActiveTab()
    {
        resourcesTab.SetActive(!_showShopTab);
        shopTab.SetActive(_showShopTab);
    }
    
    private void Start()
    {
        UpdateResourcesDescription();
        GameObject[] playerObjects = Networking.GetPlayerObjects(Networking.LocalPlayer);
        foreach (GameObject playerObject in playerObjects)
        {
            _playerWorker = playerObject.GetComponent<PlayerWorkerUnit>();
            if (_playerWorker != null) break;
        }
        UpdateActiveTab();
    }
}
