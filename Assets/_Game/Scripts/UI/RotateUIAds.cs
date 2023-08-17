using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateUIAds : MonoBehaviour
{
    public RectTransform uiObject; // Tham chiếu đến đối tượng UI cần quay
    public float fanAngle = 45f; // Góc quạt
    public float duration = 1f; // Thời gian để hoàn thành quay
    public Text textCoin;
    public UIWin uiWin;
    private void Start()
    {
        RotateFan();
    }
    private void Update()
    {
        float n = Mathf.Abs(uiObject.eulerAngles.z);
        if (n > 180f){
            n -= 360;
        }
        if (Mathf.Abs(n) > 35f)
        {
            textCoin.text = "+" + 100;
            uiWin.coinWin = 100;
        }
        else if (Mathf.Abs(n) > 25f)
        {
            textCoin.text = "+" + 150;
            uiWin.coinWin = 150;
        }
        else if (n > 15f)
        {
            textCoin.text = "+" + 200;
            uiWin.coinWin = 200;
        }
        else
        {
            textCoin.text = "+" + 250;
            uiWin.coinWin = 250;
        }
    }
    private void RotateFan()
    {
        // Sử dụng DOTween để thực hiện quay theo góc quạt
        uiObject.DORotate(new Vector3(0, 0, fanAngle), duration/*, RotateMode.LocalAxisAdd*/).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // Khi hoàn thành việc quay, tiếp tục quay ngược lại
                uiObject.DORotate(new Vector3(0, 0, -fanAngle), duration/*, RotateMode.LocalAxisAdd*/).SetEase(Ease.Linear)
                    .OnComplete(RotateFan);
            });
    }
}
