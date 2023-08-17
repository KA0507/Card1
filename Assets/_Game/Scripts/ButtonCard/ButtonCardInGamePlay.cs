using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ButtonCardInGamePlay : ButtonCard, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public UIGamePlay uiGamePlay;
    public int index;

    public void OnInit(UIGamePlay uIGamePlay)
    {
        this.uiGamePlay = uIGamePlay;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        uiGamePlay.isClick = false;
        if (uiGamePlay.currentButtonCard != null)
        {
            uiGamePlay.currentButtonCard.rect.DOLocalMove(new Vector3(uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].x, uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].y, 0), 0.5f);
            uiGamePlay.currentButtonCard.rect.DOLocalRotate(new Vector3(0, 0, uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].z), 0.5f);
        }
        uiGamePlay.currentButtonCard = this;
        uiGamePlay.indexCurrentButton = index;
        uiGamePlay.currentCard = (Card)Data.Ins.cardData.GetData(uiGamePlay.currentButtonCard.cardType, uiGamePlay.currentButtonCard.type); 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!uiGamePlay.isDrag)
        {
            uiGamePlay.SelectCard(this);
            Invoke("ActionClick", 0.01f);
        }
        else
        {
            if (uiGamePlay.rangeCard != null)
            {
                if (uiGamePlay.targets.Count > 0 || uiGamePlay.currentCard.cardType == CardType.Support)
                {
                    CardManager.Ins.UseCard(uiGamePlay.targets, uiGamePlay.currentCard);
                    uiGamePlay.currentButtonCard.gameObject.SetActive(false);
                    if (uiGamePlay.CheckGamePlay())
                    {
                        uiGamePlay.buttonFighting.gameObject.SetActive(false);
                        Invoke("PlayGame", 1f);
                    }
                    uiGamePlay.currentCard = null;
                }
                else
                {
                    if (uiGamePlay.currentButtonCard != null&& !uiGamePlay.isClick)
                    {
                        uiGamePlay.currentButtonCard.rect.DOLocalMove(new Vector3(uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].x, uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].y, 0), 0.5f);
                        uiGamePlay.currentButtonCard.rect.DOLocalRotate(new Vector3(0, 0, uiGamePlay.positionButton[uiGamePlay.indexCurrentButton].z), 0.5f);
                        uiGamePlay.currentButtonCard = null;
                        uiGamePlay.currentCard = null;

                    }
                }
                SimplePool.Despawn(uiGamePlay.rangeCard);
                uiGamePlay.rangeCard = null;
            }
            uiGamePlay.isDrag = false;
        }        
    }

    public void PlayGame()
    {
        if (uiGamePlay != null)
        {
            uiGamePlay.PlayGame();
        }
    }

    public void ActionClick()
    {
        uiGamePlay.isClick = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        uiGamePlay.isClick = false;
        uiGamePlay.isDrag = true;
        if (uiGamePlay.rangeCard == null && Input.mousePosition.y > 600f)
        {
            uiGamePlay.SelectCard(this);
            Vector3 mousePosition = Input.mousePosition;  // Lấy vị trí chuột trên màn hình
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);  // Tạo một tia từ vị trí chuột trên màn hình   
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LevelManager.Ins.groundLayer))  // Kiểm tra xem tia va chạm với một đối tượng trong không gian 3D không
            {
                Vector3 hitPosition = hit.point;  // Lấy vị trí va chạm trên không gian 3D của game
                uiGamePlay.rangeCard = SimplePool.Spawn<RangeCard>(PoolType.RangeCard, new Vector3(hitPosition.x, 0.5f, hitPosition.z), Quaternion.Euler(0, 0, 0));
                uiGamePlay.rangeCard.OnInit(uiGamePlay, uiGamePlay.currentCard.isPlayer, uiGamePlay.currentCard.isBot);
            }
        }
        else if (uiGamePlay.rangeCard != null)
        {
            Vector3 mousePosition = Input.mousePosition;  // Lấy vị trí chuột trên màn hình
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);  // Tạo một tia từ vị trí chuột trên màn hình   
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LevelManager.Ins.groundLayer))  // Kiểm tra xem tia va chạm với một đối tượng trong không gian 3D không
            {
                Vector3 hitPosition = hit.point;  // Lấy vị trí va chạm trên không gian 3D của game
                uiGamePlay.rangeCard.TF.position = new Vector3(hitPosition.x, 0.5f, hitPosition.z);
            }
        }            
    }
}
