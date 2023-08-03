using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class UIGamePlay : UICanvas
{
    public RectTransform rectButtonCard;
    public List<ButtonCard> buttonCards;
    public Vector3[] positionButton;
    public Card currentCard;
    public RangeCard rangeCard;
    //public bool hasCard = false;
    public ButtonCard currentButtonCard;
    public int indexCurrentButton;
    public LayerMask groundLayer;
    public List<Character> targets;
    public bool isChangeCard;

    private void Awake()
    {
        for (int i = 0; i < buttonCards.Count; i++)
        {
            int currentIndex = i;
            buttonCards[currentIndex].btn.onClick.RemoveAllListeners();
            buttonCards[currentIndex].btn.onClick.AddListener(() => buttonCards[currentIndex].SelectCard(this, currentIndex));
        }
    }
    public void UseCard()
    {
        CardManager.Ins.uiGamePlay = this;
        for (int i = 0; i < buttonCards.Count; i++)
        {
            buttonCards[i].ChangeCard(UserData.Ins.GetEnumData<CardType>(UserData.KEY_BUTTON_CARDTYPE + i, CardType.Digits),
                                      UserData.Ins.GetEnumData<PoolType>(UserData.KEY_BUTTON_POOLTYPE + i, PoolType.None));
            buttonCards[i].TF.localPosition = new Vector3(positionButton[i].x, positionButton[i].y, 0);
            buttonCards[i].TF.localRotation = Quaternion.Euler(new Vector3(0, 0, positionButton[i].z));
            buttonCards[i].gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        GameManager.Ins.ChangeState(GameState.GAMEPLAY);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCard != null/* && hasCard*/)
        {
            if (Input.GetMouseButtonDown(0))  // Kiểm tra xem đã bấm chuột trái hay chưa
            {
                Vector3 mousePosition = Input.mousePosition;  // Lấy vị trí chuột trên màn hình
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);  // Tạo một tia từ vị trí chuột trên màn hình
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))  // Kiểm tra xem tia va chạm với một đối tượng trong không gian 3D không
                {
                    Vector3 hitPosition = hit.point;  // Lấy vị trí va chạm trên không gian 3D của game
                    rangeCard = SimplePool.Spawn<RangeCard>(PoolType.RangeCard, new Vector3(hitPosition.x, 0.5f, hitPosition.z), Quaternion.Euler(0, 0, 0));
                    rangeCard.OnInit(this, currentCard.isPlayer, currentCard.isBot);
                }
            }
            if (Input.GetMouseButton(0))
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
                    SimplePool.Despawn(rangeCard);
                    rangeCard = null;
                    if (targets.Count > 0)
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
                        if (currentButtonCard != null && !isChangeCard)
                        {
                            currentButtonCard.rect.DOLocalMove(new Vector3(positionButton[indexCurrentButton].x, positionButton[indexCurrentButton].y, 0), 1f);
                            //currentButtonCard.rect.localPosition = new Vector3(positionButton[indexCurrentButton].x, positionButton[indexCurrentButton].y, 0);
                            currentButtonCard.rect.DOLocalRotate(new Vector3(0, 0, positionButton[indexCurrentButton].z), 1f);
                            currentButtonCard = null;
                            currentCard = null;

                        }
                    }

                    //hasCard = false;
                }
            }
        }
    }

    public void ChangeCard()
    {
        isChangeCard = false;
    }
    public void SelectCard(ButtonCard buttonCard, int n)
    {
        if (currentButtonCard != null)
        {
            //currentButtonCard.rect.DOKill();
            currentButtonCard.rect.DOLocalMove(new Vector3(positionButton[indexCurrentButton].x, positionButton[indexCurrentButton].y, 0), 1f);
            //currentButtonCard.rect.localPosition = new Vector3(positionButton[indexCurrentButton].x, positionButton[indexCurrentButton].y, 0);
            currentButtonCard.rect.DOLocalRotate(new Vector3(0, 0, positionButton[indexCurrentButton].z), 1f);
        }
        currentButtonCard = buttonCard;
        indexCurrentButton = n;
        buttonCard.rect.DOLocalMove(new Vector3(buttonCard.rect.localPosition.x, -300, buttonCard.rect.localPosition.z), 1f);
        //buttonCard.rect.localPosition = new Vector3(buttonCard.rect.localPosition.x, -300, buttonCard.rect.localPosition.z);
        buttonCard.rect.DOLocalRotate(Vector3.zero, 1f);
        currentCard = (Card)Data.Ins.cardData.GetData(buttonCard.cardType, buttonCard.type);
        //hasCard = true;
        isChangeCard = true;
        Invoke("ChangeCard", 0.001f);
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
