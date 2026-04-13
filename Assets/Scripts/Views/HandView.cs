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
    public float maxCards = 5f;

    public bool isFull => cards.Count >= maxCards;
    public bool hasSpace => cards.Count < maxCards;
    private bool wasCardDrawPlayed = false;

    public IEnumerator AddCard (CardView cardView)
    {
        if (!hasSpace)
        {
            yield break;
        }

        cards.Add(cardView);

        cardView.OnCardPlayed += RemoveCard;
        cardView.Card.OnCardDrawPlayed += OnCardDrawPlayed;
        cardView.Card.OnCardPlayedComplete += OnCardPlayedComplete;

        yield return UpdateCardPositions(0.15f);
    }

    private void RemoveCard(CardView cardView)
    {
        cards.Remove(cardView);

        cardView.OnCardPlayed -= RemoveCard;
        cardView.Card.OnCardDrawPlayed -= OnCardDrawPlayed;
        cardView.Card.OnCardPlayedComplete -= OnCardPlayedComplete;

        StartCoroutine(UpdateCardPositions(0.15f));
    }

    public IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;

        float cardSpacing = 2f / maxCards;
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

    private void OnCardDrawPlayed()
    {
        wasCardDrawPlayed = true;
    }

    private void OnCardPlayedComplete()
    {
        if (wasCardDrawPlayed)
        {
            wasCardDrawPlayed = false;
            return;
        }

        ClearHand();
    }

    public void ClearHand()
    {
        foreach (CardView card in cards)
        {
            Destroy(card.gameObject);
        }

        cards.Clear();
    }
}
