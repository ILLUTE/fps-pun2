using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AvatarSelectionScreen : SelectionScreens
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private RectTransform screen;

    private Sequence sequence;
    public override void OnScreenClose()
    {
        if(sequence != null)
        {
            sequence.Kill();
        }

        sequence = DOTween.Sequence();

        sequence.Append(screen.DOAnchorPosX(screen.sizeDelta.x, 0.5f)).Join(canvasGroup.DOFade(0, 0.5f));
    }

    public override void OnScreenOpen()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }

        sequence = DOTween.Sequence();

        sequence.Append(screen.DOAnchorPosX(0, 0.5f)).Join(canvasGroup.DOFade(1, 0.5f));
    }
}
