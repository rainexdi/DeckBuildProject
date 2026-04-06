using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    private readonly List<CardView> cards = new();
    public float maxCards = 10f;

    public bool isFull => cards.Count >= maxCards;
    public bool hasSpace => cards.Count < maxCards;

    public IEnumerator AddCard (CardView cardView)
    {
        if (!hasSpace)
        {
            yield break;
        }

        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }

    public IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;

        float cardSpacing = 1f / maxCards;
        float firstCardPosition = 0.5f - (cardSpacing * (cards.Count - 1)) / 2f;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {          
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up,forward).normalized);
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotateQuaternion(rotation, duration);
        }
        yield return new WaitForSeconds(duration);
    }

    public bool CanDrawMore(int cardsToDraw)
    {
        return cards.Count < cardsToDraw && cards.Count < maxCards;
    }
}
