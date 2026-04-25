using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//交互系统
public class PlayerInteractor : MonoBehaviour
{
    PlayerController playerController;

    //玩家选中的类型是什么
    InteractableObject selectInteractable=null;

    Land selectLand;
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down, out hit,1)) 
        {
            OnInteractableHit(hit);
        }
    }

    private void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        Land land=null;
        //检查如果是被选中的土地就显示
        if (other.tag=="Land")
        {
            land = other.GetComponent<Land>();
            SelectLand(land);           
        }

        if(other.tag=="Item")
        {
            selectInteractable = other.GetComponent<InteractableObject>();
            return;
        }

        if(selectInteractable!=null)
        {
            selectInteractable = null;
        }

        //如果玩家走开就不显示
        if(selectLand!=null && selectLand!=land)
        {
            selectLand.Select(false);
            selectLand = null;
        }
    }

    void SelectLand(Land land)
    {
        //如果玩家走开就不显示
        if (selectLand != null && selectLand != land)
        {
            selectLand.Select(false);
            selectLand = null;
        }

        selectLand =land;
        land.Select(true);
    }

    public void Interact()
    {
        //手上拿着东西禁用工具
        if(InventoryManager.Instance.equippedItem!=null)
        {
            return;
        }

        if (selectLand!=null)
        {
            selectLand.Interact();
            return;
        }
    }

    public void ItemInteract()
    {
        //如果手上拿着东西，把手里的放包里
        if(InventoryManager.Instance.equippedItem!=null)
        {
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }

        //如果手上没东西，捡到手里
        if(selectInteractable != null)
        {
            selectInteractable.Pickup();
            
        }
    }
}
