using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SaiMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] BaseBtn clickBtn;
    public BaseBtn ClickBtn { get { return clickBtn; } set { clickBtn = value; } }
    [SerializeField]
    private Text currencyTxt;

    private int currency;

    private bool isGamePaused = false;
    public Transform gameFinish;
    public Transform fade;
    public GameObject backMap;

    [SerializeField]
    CardRefresh cardRefresh;
    public CardRefresh CardRefresh => cardRefresh;
    CardBtn cardBtn;


    //Envent
    public bool OnFinish;
    public int Currency
    {
        get { return currency; }
        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString();
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        PortalSpawnManager.AllPortalsSpawned += GameFinish;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        PortalSpawnManager.AllPortalsSpawned -= GameFinish;
    }
    private void OnApplicationQuit()
    {
        // Xử lý tại đây khi game bị tắt
        HandleEscape();
    }
    protected override void Start()
    {
        base.Start();
        Currency = 20;
    }
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("Onlly 1 GameManager Warning");
        GameManager.instance = this;
    }
    public void PickButton(BaseBtn button, CardRefresh CardPickup)
    {
        if (button == clickBtn)
        {
            DePickButton(button);
            return; // If the button overlaps with clickBtn, do not perform other operations
        }
        else if (clickBtn != null) // If you are pressing another button
        {
            HandleEscape(); // HandleEscape for the current button
        }

        if (button is MachineBtn && Currency >= button.Price)
        {
            this.clickBtn = button as MachineBtn;

            Hover.Instance.Activate(button.Sprite);
        }
        else if (button is CardBtn)
        {
            HandleActivation(button);

            cardRefresh = CardPickup;
        }
    }

    public void DePickButton(BaseBtn button)
    {
        if (clickBtn == null) return;
        if (button == clickBtn)
        {
            HandleEscape();
            Debug.Log("DePickButton");
        }
    }

    public void BuyCard()
    {

        this.SubtractCurrency(clickBtn.Price);

        this.HandleEscape();

    }

    //--
    void SubtractCurrency(int amount)
    {
        Debug.Log(amount);

        Currency -= amount;
    }
    public void HandleEscape()
    {
        Hover.Instance.Deactivate();
        this.clickBtn = null;
        if (cardBtn != null)
        {
            cardBtn.SelectButton.SetActive(false);
            cardBtn = null;
        }
    }
    public void HandleActivation(BaseBtn button)
    {
        if (button is CardBtn)
        {
            Hover.Instance.Activate(button.Sprite);

            this.clickBtn = button as CardBtn;

            cardBtn = (CardBtn)clickBtn;

            cardBtn.SelectButton.SetActive(true);
        }
    }
    // Pause Game
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Dừng thời gian trong trò chơi

    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Tiếp tục thời gian trong trò chơi       
    }
    private void GameFinish()
    {
        //Hadnle Escapse Hover
        GameManager.instance.HandleEscape();

        this.OnFinish = true;
        gameFinish.gameObject.SetActive(true);
        fade.gameObject.SetActive(true);
        backMap.SetActive(true);

    }

}
