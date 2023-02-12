using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveController : MonoBehaviour
{
    public GameObject[] SlotsInstPos;
    bool[] IsSlotBussy = new bool[2];
    bool[] IsSlotHasBurger = new bool[2];
    GameObject FreeSlot;
    GameObject[] SlotFoodOBJ = new GameObject[2];
    public Image SlotFill1;
    public Image SlotFill2;
    public bool PlayerOnStove;
    PlayerController player;

    void Start()
    {
        IsSlotHasBurger[0] = false;
        IsSlotHasBurger[1] = false;
        PlayerOnStove = false;
        player = GameManager.Instance.Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if ((IsSlotHasBurger[0] || IsSlotHasBurger[1]) & PlayerOnStove && player.Carry == false)
        {
            if (IsSlotHasBurger[0])
            {
                IsSlotHasBurger[0] = false;
                IsSlotBussy[0] = false;
                Destroy(SlotFoodOBJ[0]);
                GameManager.Instance.Player.GetComponent<PlayerController>().GetBurger();
                return;
            }
            if (IsSlotHasBurger[1])
            {
                IsSlotHasBurger[1] = false;
                IsSlotBussy[1] = false;
                Destroy(SlotFoodOBJ[1]);
                GameManager.Instance.Player.GetComponent<PlayerController>().GetBurger();
                return;
            }
        }

    }

    public void PutMeatOnStove()
    {

        if (FreeSlot == SlotsInstPos[0])
        {
            SlotFoodOBJ[0] = Instantiate(GameManager.Instance.Meat, FreeSlot.transform.position, FreeSlot.transform.rotation, FreeSlot.transform);
            StartCoroutine(MeatCook1Countdown());
            IsSlotBussy[0] = true;
        }
        if (FreeSlot == SlotsInstPos[1])
        {
            SlotFoodOBJ[1] = Instantiate(GameManager.Instance.Meat, FreeSlot.transform.position, FreeSlot.transform.rotation, FreeSlot.transform);
            StartCoroutine(MeatCook2Countdown());
            IsSlotBussy[1] = true;
        }
    }

    public bool CheckForFreeSlot()
    {
        if (IsSlotBussy[0] == false)
        {
            FreeSlot = SlotsInstPos[0];
            return true;
        }
        else if (IsSlotBussy[1] == false)
        {
            FreeSlot = SlotsInstPos[1];
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator MeatCook1Countdown()
    {
        float duration = 6f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            SlotFill1.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        IsSlotHasBurger[0] = true;
        SlotFill1.fillAmount = 0;
        Destroy(SlotFoodOBJ[0]);
        SlotFoodOBJ[0] = Instantiate(GameManager.Instance.Burger, SlotsInstPos[0].transform.position, SlotsInstPos[0].transform.rotation, SlotsInstPos[0].transform);
    }
    private IEnumerator MeatCook2Countdown()
    {
        float duration = 6f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            SlotFill2.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        IsSlotHasBurger[1] = true;
        SlotFill2.fillAmount = 0;
        Destroy(SlotFoodOBJ[1]);
        SlotFoodOBJ[1] = Instantiate(GameManager.Instance.Burger, SlotsInstPos[1].transform.position, SlotsInstPos[1].transform.rotation, SlotsInstPos[1].transform);
        //IsSlotBussy[1] = false;
    }


}
