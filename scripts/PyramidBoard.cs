using Godot;
using System;
using System.Collections.Generic;

public partial class PyramidBoard : Node2D
{
    private const int CardWidth = 80;
    private const int CardHeight = 120;
    private const int HorizontalSpacing = 40;
    private const int VerticalSpacing = 30;

    private PackedScene cardScene;
    private Node2D cardsContainer;
    private Card[,] pyramid = new Card[7, 7];

    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card.tscn");
        cardsContainer = GetNode<Node2D>("CardsContainer");
        CreatePyramid();
    }

    private void CreatePyramid()
    {
        var deck = CreateShuffledDeck();
        int cardIndex = 0;

        for (int row = 0; row < 7; row++)
        {
            int cardsInRow = row + 1;
            float rowWidth = cardsInRow * CardWidth + (cardsInRow - 1) * HorizontalSpacing;
            float startX = -rowWidth / 2;

            for (int col = 0; col < cardsInRow; col++)
            {
                var card = cardScene.Instantiate<Card>();
                card.Rank = deck[cardIndex].Rank;
                card.Suit = deck[cardIndex].Suit;

                float x = startX + col * (CardWidth + HorizontalSpacing);
                float y = row * (CardHeight / 2 + VerticalSpacing);
                card.Position = new Vector2(x, y);

                cardsContainer.AddChild(card);
                pyramid[row, col] = card;

                card.CardClicked += OnCardClicked;
                cardIndex++;
                GD.Print($"Creating card at row {row}, col {col}, position ({x}, {y})");
            }
        }

        UpdateSelectableCards();
    }

    private void UpdateSelectableCards()
    {
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col <= row; col++)
            {
                if (pyramid[row, col] != null)
                {
                    pyramid[row, col].IsSelectable = !IsCardCovered(row, col);
                }
            }
        }
    }

    private bool IsCardCovered(int row, int col)
    {
        if (row >= 6) return false;

        bool leftExists = pyramid[row + 1, col] != null;
        bool rightExists = pyramid[row + 1, col + 1] != null;

        return leftExists || rightExists;
    }

    private void OnCardClicked(Card card)
    {
        // カード選択処理
        GD.Print($"Card clicked: {card.Suit} {card.Rank}");
    }

    private System.Collections.Generic.List<(int Rank, string Suit)> CreateShuffledDeck()
    {
        var deck = new System.Collections.Generic.List<(int, string)>();
        string[] suits = { "hearts", "diamonds", "clubs", "spades" };

        for (int i = 0; i < 28; i++)
        {
            int rank = (i % 13) + 1;
            string suit = suits[i / 13];
            deck.Add((rank, suit));
        }

        // シャッフル処理
        var random = new Random();
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }

        return deck;
    }
}