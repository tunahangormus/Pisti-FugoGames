using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EndCardController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI endText;

    public void ShowEndCard()
    {
        gameOverPanel.transform.localScale = new Vector3(0, 0, 0);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetDelay(0.5f).SetEase(Ease.OutBack);
    }

    public void SetEndText(string text)
    {
        endText.text = text;
    }

    public void HideEndCard()
    {
        gameOverPanel.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameOverPanel.SetActive(false);
        });
    }
}
