using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    public RectTransform rect;
    public Image image;
    public Text hp;
    public Transform target;

    public CanvasGroup canvasGroup;

    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    Camera Camera;

    private void Awake()
    {
        Camera = CameraFollow.Ins.Camera;
    }

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(target.position);
        Vector3 targetPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        rect.anchoredPosition = targetPoint;
    }

    public void OnInit(Character character)
    {
        SetHp(character.hp);
        SetTarget(character.indicatorPoint);
        SetColor(character is Bot ? LevelManager.Ins.materials[0].color : LevelManager.Ins.materials[1].color);
        SetAlpha(1);
        //SetAlpha(GameManager.Ins.IsState(GameState.GAMEPLAY) ? 1 : 0);
    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
    public void SetHp(int hp)
    {
        this.hp.text = hp.ToString();
    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
