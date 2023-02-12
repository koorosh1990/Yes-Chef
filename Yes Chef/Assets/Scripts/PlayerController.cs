using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public FloatingJoystick variableJoystick;
    Rigidbody rb;
    public bool Carry;
    public GameObject PlayerInstPos;
    Animator anim;
    public GameObject CarryOBJ;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Carry = false;
        anim.speed = 0;
    }
    public void Update()
    {
        if (GameManager.GameIsStart && (!GameManager.GameIsFinished) && GameManager.Instance.IsGamePause == false)
        {
            playerMovmeny();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    void playerMovmeny()
    {
        float x = variableJoystick.Horizontal;
        float y = variableJoystick.Vertical;
        Vector3 movement = new Vector3(x, 0, y);
        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }
        if (movement != Vector3.zero)
        {
            rb.velocity = movement * speed;
            anim.speed = 1;
        }
        else
        {
            rb.velocity = Vector3.zero;
            anim.speed = 0;
        }
    }
    public void GetChopedLettuce()
    {
        Carry = true;
        anim.SetTrigger("runCarry");
        CarryOBJ = Instantiate(GameManager.Instance.ChopedLettuce, PlayerInstPos.transform.position, PlayerInstPos.transform.rotation, PlayerInstPos.transform);
    }
    public void GetBurger()
    {
        Carry = true;
        anim.SetTrigger("runCarry");
        CarryOBJ = Instantiate(GameManager.Instance.Burger, PlayerInstPos.transform.position, PlayerInstPos.transform.rotation, PlayerInstPos.transform);
    }
    private void OnCollisionEnter(Collision col)
    {
        switch (col.transform.tag)
        {
            case "fLettuce":
                if (Carry == false)
                {
                    Carry = true;
                    anim.SetTrigger("runCarry");
                    CarryOBJ = Instantiate(GameManager.Instance.Lettuce, PlayerInstPos.transform.position, PlayerInstPos.transform.rotation, PlayerInstPos.transform);
                }
                break;

            case "fMeat":
                if (Carry == false)
                {
                    Carry = true;
                    anim.SetTrigger("runCarry");
                    CarryOBJ = Instantiate(GameManager.Instance.Meat, PlayerInstPos.transform.position, PlayerInstPos.transform.rotation, PlayerInstPos.transform);
                }
                break;
            case "fCheese":
                if (Carry == false)
                {
                    Carry = true;
                    anim.SetTrigger("runCarry");
                    CarryOBJ = Instantiate(GameManager.Instance.Cheese, PlayerInstPos.transform.position, PlayerInstPos.transform.rotation, PlayerInstPos.transform);
                }
                break;
            case "trash":
                if (Carry == true)
                {
                    Carry = false;
                    anim.SetTrigger("run");
                    Destroy(CarryOBJ);
                }
                break;
            case "table":
                if (Carry == true && CarryOBJ.transform.CompareTag("lettuce") && col.transform.GetComponent<TableController>().FoodOnTable == false)
                {
                    Carry = false;
                    anim.SetTrigger("run");
                    col.transform.GetComponent<TableController>().PutLettuceOnTable();
                    Destroy(CarryOBJ);
                }
                break;
            case "stove":
                if (Carry == true && CarryOBJ.transform.CompareTag("meat") && col.transform.GetComponent<StoveController>().CheckForFreeSlot())
                {
                    Carry = false;
                    anim.SetTrigger("run");
                    col.transform.GetComponent<StoveController>().PutMeatOnStove();
                    Destroy(CarryOBJ);
                }
                break;
            case "customerWindow":
                if (Carry == true && CarryOBJ.transform.CompareTag("burger"))
                {
                    var customerWindow = col.transform.GetComponent<CustomerWindowController>();
                    for (int i = 0; i < customerWindow.ingredientCount; i++)
                    {
                        if (customerWindow.ingredients[i] == 0)
                        {
                            Carry = false;
                            customerWindow.ingredients[i] = -1;
                            anim.SetTrigger("run");
                            col.transform.GetComponent<CustomerWindowController>().PutBurgerOnCustomerWindow();
                            col.transform.GetComponent<CustomerWindowController>().ArrangeFoods();
                            Destroy(CarryOBJ);
                            return;
                        }
                    }
                }
                if (Carry == true && CarryOBJ.transform.CompareTag("cheese"))
                {
                    var customerWindow = col.transform.GetComponent<CustomerWindowController>();
                    for (int i = 0; i < customerWindow.ingredientCount; i++)
                    {
                        if (customerWindow.ingredients[i] == 1)
                        {
                            Carry = false;
                            customerWindow.ingredients[i] = -1;
                            anim.SetTrigger("run");
                            col.transform.GetComponent<CustomerWindowController>().PutCheeseOnCustomerWindow();
                            col.transform.GetComponent<CustomerWindowController>().ArrangeFoods();
                            Destroy(CarryOBJ);
                            return;
                        }
                    }
                }
                if (Carry == true && CarryOBJ.transform.CompareTag("chopedLettuce"))
                {
                    var customerWindow = col.transform.GetComponent<CustomerWindowController>();
                    for (int i = 0; i < customerWindow.ingredientCount; i++)
                    {
                        if (customerWindow.ingredients[i] == 2)
                        {
                            Carry = false;
                            customerWindow.ingredients[i] = -1;
                            anim.SetTrigger("run");
                            col.transform.GetComponent<CustomerWindowController>().PutLettuceOnCustomerWindow();
                            col.transform.GetComponent<CustomerWindowController>().ArrangeFoods();
                            Destroy(CarryOBJ);
                            return;
                        }
                    }
                }
                break;
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("table"))
        {
            if (Carry == false)
            {
                col.transform.GetComponent<TableController>().DoChop = true;
            }
        }
        if (col.transform.CompareTag("stove"))
        {
            if (Carry == false)
            {
                col.transform.GetComponent<StoveController>().PlayerOnStove = true;
            }
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("table"))
        {
            col.transform.GetComponent<TableController>().DoChop = false;
        }
        if (col.transform.CompareTag("stove"))
        {
            col.transform.GetComponent<StoveController>().PlayerOnStove = false;
        }
    }


}
