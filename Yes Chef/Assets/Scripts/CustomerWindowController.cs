using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerWindowController : MonoBehaviour
{
    public Sprite[] FoodSprites;
    public Image[] Foods;
    public int ingredientCount;
    public int[] ingredients;
    GameObject[] FoodOnPlate;
    int foodOnPlateIndex;

    public GameObject CustomerChar;
    public GameObject InstPos;
    public TextMeshProUGUI CustomerWindowTimer;
    int score;
    bool orderEnded;
    float timeLeft;
    void Start()
    {
        score = 0;
        orderEnded = false;
        CustomerChar.GetComponent<Animator>().speed = Random.Range(.7f, 1.2f);
        GenerateNewOrder();
    }
    IEnumerator timer()
    {
        timeLeft = 0;

        while (!orderEnded)
        {
            if (GameManager.GameIsStart)
            {
                timeLeft += Time.deltaTime;
                CustomerWindowTimer.text = ((int)timeLeft).ToString();
            }
            yield return null;
        }

    }
    public void GenerateNewOrder()
    {
        Foods[0].sprite = FoodSprites[0];
        Foods[1].sprite = FoodSprites[0];
        Foods[2].sprite = FoodSprites[0];
        ingredientCount = Random.Range(2, 4);
        ingredients = new int[ingredientCount];
        FoodOnPlate = new GameObject[ingredientCount];
        foodOnPlateIndex = 0;
        orderEnded = false;
        generateFoods();
        CustomerChar.SetActive(true);
        StartCoroutine(timer());
    }
    void generateFoods()
    {
        for (int i = 0; i < ingredientCount; i++)
        {
            ingredients[i] = Random.Range(0, 3);
        }
        ArrangeFoods();
    }
    public void ArrangeFoods()
    {
        for (int i = 0; i < ingredientCount; i++)
        {
            Foods[i].sprite = FoodSprites[ingredients[i] + 1];
        }
    }

    public void PutBurgerOnCustomerWindow()
    {
        score += 30;
        int sumOfIngredient = 0;
        FoodOnPlate[foodOnPlateIndex] = Instantiate(GameManager.Instance.Burger, InstPos.transform.position, InstPos.transform.rotation);
        FoodOnPlate[foodOnPlateIndex].transform.position += new Vector3(0, foodOnPlateIndex * 0.15f, 0);
        foodOnPlateIndex++;
        foreach (int ingredient in ingredients)
        {
            sumOfIngredient += ingredient;
        }
        if (Mathf.Abs(sumOfIngredient) == ingredientCount)
        {
            orderEnded = true;
            Invoke("preResetOrderSets", 2);
            Invoke("GenerateNewOrder", 7);
        }
    }
    public void PutCheeseOnCustomerWindow()
    {
        score += 10;
        int sumOfIngredient = 0;
        FoodOnPlate[foodOnPlateIndex] = Instantiate(GameManager.Instance.Cheese, InstPos.transform.position, InstPos.transform.rotation);
        FoodOnPlate[foodOnPlateIndex].transform.position += new Vector3(0, foodOnPlateIndex * 0.15f, 0);
        foodOnPlateIndex++;
        foreach (int ingredient in ingredients)
        {
            sumOfIngredient += ingredient;
        }
        if (Mathf.Abs(sumOfIngredient) == ingredientCount)
        {
            orderEnded = true;
            Invoke("preResetOrderSets", 2);
            Invoke("GenerateNewOrder", 7);
        }
    }
    public void PutLettuceOnCustomerWindow()
    {
        score += 20;
        int sumOfIngredient = 0;
        FoodOnPlate[foodOnPlateIndex] = Instantiate(GameManager.Instance.ChopedLettuce, InstPos.transform.position, InstPos.transform.rotation);
        FoodOnPlate[foodOnPlateIndex].transform.position += new Vector3(0, foodOnPlateIndex * 0.15f, 0);
        foodOnPlateIndex++;
        foreach (int ingredient in ingredients)
        {
            sumOfIngredient += ingredient;
        }
        if (Mathf.Abs(sumOfIngredient) == ingredientCount)
        {

            orderEnded = true;
            Invoke("preResetOrderSets", 2);
            Invoke("GenerateNewOrder", 7);
        }
    }
    void preResetOrderSets()
    {
        GameManager.Score += (score - (int)timeLeft);
        UIManager.Instance.ScoreSetter();
        CustomerChar.SetActive(false);
        CustomerWindowTimer.text = "";
        foreach (var food in FoodOnPlate)
        {
            Destroy(food);
        }
        ////////////////////////////
    }

}
