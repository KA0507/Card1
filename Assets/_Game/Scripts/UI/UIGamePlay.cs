using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIGamePlay : UICanvas
{
    public Text textLevel;
    public Button buttonFighting;
    public RectTransform rectButtonCard;
    public List<ButtonCardInGamePlay> buttonCards;
    public Vector3[] positionButton;
    public Card currentCard;
    public RangeCard rangeCard;
    //public bool hasCard = false;
    public ButtonCardInGamePlay currentButtonCard;
    public int indexCurrentButton;
    public LayerMask groundLayer;
    public List<Character> targets;
    public bool isChangeCard;
    public bool isDrag;
    public bool isClick;

    private void Awake()
    {
        /*for (int i = 0; i < buttonCards.Count; i++)
        {
            int currentIndex = i;
            buttonCards[currentIndex].btn.onClick.RemoveAllListeners();
            buttonCards[currentIndex].btn.onClick.AddListener(() => buttonCards[currentIndex].SelectCard(this, currentIndex));
        }*/
        
    }
    public void UseCard()
    {
        textLevel.text = Constant.LEVEL + (LevelManager.Ins.indexLevel + 1);
        CardManager.Ins.uiGamePlay = this;
        for (int i = 0; i < buttonCards.Count; i++)
        {
            buttonCards[i].ChangeCard(UserData.Ins.GetEnumData<CardType>(UserData.KEY_BUTTON_CARDTYPE + i, CardType.Digits),
                                      UserData.Ins.GetEnumData<PoolType>(UserData.KEY_BUTTON_POOLTYPE + i, PoolType.None));
            buttonCards[i].TF.localPosition = new Vector3(positionButton[i].x, positionButton[i].y, 0);
            buttonCards[i].TF.localRotation = Quaternion.Euler(new Vector3(0, 0, positionButton[i].z));
            buttonCards[i].gameObject.SetActive(true);
        }
        buttonFighting.gameObject.SetActive(true);
    }
    public void PlayGame()
    {
        GameManager.Ins.ChangeState(GameState.GAMEPLAY);
    }

    public void Fighting()
    {
        for (int i = 0; i < buttonCards.Count; i++)
        {
            buttonCards[i].gameObject.SetActive(false);
        }
        buttonFighting.gameObject.SetActive(false);
        Invoke("PlayGame", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            if (rangeCard == null && currentCard != null && Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;  // Lấy vị trí chuột trên màn hình

                Ray ray = Camera.main.ScreenPointToRay(mousePosition);  // Tạo một tia từ vị trí chuột trên màn hình

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LevelManager.Ins.groundLayer))  // Kiểm tra xem tia va chạm với một đối tượng trong không gian 3D không
                {
                    Vector3 hitPosition = hit.point;  // Lấy vị trí va chạm trên không gian 3D của game
                    rangeCard = SimplePool.Spawn<RangeCard>(PoolType.RangeCard, new Vector3(hitPosition.x, 0.5f, hitPosition.z), Quaternion.Euler(0, 0, 0));
                    rangeCard.OnInit(this, currentCard.isPlayer, currentCard.isBot);
                }
            }
            if (rangeCard != null && Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;  // Lấy vị trí chuột trên màn hình

                Ray ray = Camera.main.ScreenPointToRay(mousePosition);  // Tạo một tia từ vị trí chuột trên màn hình

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))  // Kiểm tra xem tia va chạm với một đối tượng trong không gian 3D không
                {
                    Vector3 hitPosition = hit.point;  // Lấy vị trí va chạm trên không gian 3D của game
                    rangeCard.TF.position = new Vector3(hitPosition.x, 0.5f, hitPosition.z);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (rangeCard != null)
                {
                    if (targets.Count > 0 || currentCard.cardType == CardType.Support)
                    {
                        CardManager.Ins.UseCard(targets, currentCard);
                        currentButtonCard.gameObject.SetActive(false);
                        if (CheckGamePlay())
                        {
                            Invoke("PlayGame", 1f);
                        }
                        currentCard = null;
                    }
                    else
                    {
                        if (currentButtonCard != null)
                        {
                            currentButtonCard.rect.DOLocalMove(new Vector3(positionButton[indexCurrentButton].x, positionButton[indexCurrentButton].y, 0), 1f);
                            currentButtonCard.rect.DOLocalRotate(new Vector3(0, 0, positionButton[indexCurrentButton].z), 1f);
                            currentButtonCard = null;
                            currentCard = null;

                        }
                    }
                    SimplePool.Despawn(rangeCard);
                    rangeCard = null;
                }
                isClick = false;
            }
        }
    }

    public void ChangeCard()
    {
        isChangeCard = false;
    }

    public void SelectCard(ButtonCardInGamePlay buttonCard)
    {
        buttonCard.rect.DOLocalMove(new Vector3(currentButtonCard.rect.localPosition.x, -300, currentButtonCard.rect.localPosition.z), 0.5f);
        buttonCard.rect.DOLocalRotate(Vector3.zero, 0.5f);
    }
    public bool CheckGamePlay()
    {
        for (int i = 0; i < buttonCards.Count; i++)
        {
            if (buttonCards[i].gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
