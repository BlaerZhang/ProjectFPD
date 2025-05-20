using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoostenProductions;

public class DeckUIManager : OverridableMonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private GameObject deckUIRoot;
    [SerializeField] private GameObject cardViewPrefab;
    private GameObject contentHolder;
    private GameObject cardVisualHolder;

    private BasicFPCC playerController;

    // Start is called before the first frame update
    void Start()
    {
        // 确保初始状态是隐藏的
        deckUIRoot.SetActive(false);

        // 获取contentHolder和cardVisualHolder
        contentHolder = deckUIRoot.transform.Find("Viewport/Content").gameObject;
        cardVisualHolder = deckUIRoot.transform.Find("Viewport/Card Visual Holder").gameObject;
        
        // 获取玩家控制器引用
        playerController = FindObjectOfType<BasicFPCC>();
        
        // 订阅游戏状态改变事件
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    public override void UpdateMe()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleDeckUI();
        }
    }

    private void ToggleDeckUI()
    {
        if (GameManager.Instance.CurrentGameState != GameState.ViewingDeck)
        {
            GameManager.Instance.OpenDeckView();
        }
        else
        {
            GameManager.Instance.ResumeGame();
        }
    }

    private void HandleGameStateChanged(GameState newState)
    {
        bool isDeckView = newState == GameState.ViewingDeck;
        
        // 控制玩家输入
        if (playerController != null)
        {
            playerController.useLocalInputs = !isDeckView;
        }
        
        // 控制鼠标显示
        GameManager.Instance.SetLockCursor(!isDeckView);

        // 显示/隐藏牌组UI
        PrepareAndShowDeckUI(isDeckView);
    }

    private void PrepareAndShowDeckUI(bool show)
    {
        if (show)
        {
            // 根据deckManager的deck生成contentHolder和cardVisualHolder的子物体
            foreach (var card in DeckManager.instance.deck)
            {
                // 生成cardView
                GameObject cardSlot = new GameObject("CardSlot");
                cardSlot.AddComponent<RectTransform>();
                cardSlot.layer = LayerMask.NameToLayer("UI");
                cardSlot.transform.SetParent(contentHolder.transform, false);
                CardView cardView = Instantiate(cardViewPrefab, cardSlot.transform, false).GetComponent<CardView>();

                // 生成cardVisual
                CardVisual cardVisual = cardView.InitializeAndCreateCardVisual(card);
                cardVisual.transform.SetParent(cardVisualHolder.transform, false);
            }
            deckUIRoot.SetActive(true);
        }
        else
        {
            deckUIRoot.SetActive(false);
            // 清除contentHolder和cardVisualHolder的所有子物体
            foreach (Transform child in contentHolder.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in cardVisualHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
    