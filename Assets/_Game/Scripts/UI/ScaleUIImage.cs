using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUIImage : MonoBehaviour
{
    public RectTransform rect; // Tham chiếu đến UI Image
    public float targetScale = 1.2f; // Giá trị phóng to mục tiêu

    void Start()
    {
        ScaleContinuously();
    }

    void ScaleContinuously()
    {
        // Sử dụng DOTween để liên tục phóng to và thu nhỏ kích thước của UI Image
        rect.DOScale(targetScale, 1f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            // Khi hoàn thành việc phóng to, thu nhỏ, thực hiện đảo ngược
            rect.DOScale(1f, 1f).SetEase(Ease.OutSine).OnComplete(ScaleContinuously);
        });
    }
}
