using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    public GameObject TableInstPos;
    GameObject LettuceOBJ;
    public Image ChopFill;
    public bool FoodOnTable;
    public bool DoChop;
    float timeLeft;


    private void Start()
    {
        FoodOnTable = false;
        DoChop = false;
    }
    private void Update()
    {
        if (FoodOnTable && DoChop)
        {
            timeLeft -= Time.deltaTime;
            ChopFill.fillAmount = 1 - timeLeft / 2;
            if (timeLeft < 0)
            {
                ChopFill.fillAmount = 0;
                Destroy(LettuceOBJ);
                GameManager.Instance.Player.GetComponent<PlayerController>().GetChopedLettuce();
                DoChop = false;
                FoodOnTable = false;
            }
        }
    }

    public void PutLettuceOnTable()
    {
        LettuceOBJ = Instantiate(GameManager.Instance.Lettuce, TableInstPos.transform.position, TableInstPos.transform.rotation, TableInstPos.transform);
        FoodOnTable = true;
        timeLeft = 2f;
    }

}
