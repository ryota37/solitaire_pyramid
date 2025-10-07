using Godot;
using System;

public partial class PyramidSolitaire : Node2D
{
    private const int CardWidth = 80;
    private const int CardHeight = 120;
    private const int HorizontalSpacing = 40;
    private const int VerticalSpacing = 30;
    
    public override void _Ready()
    {
        CreatePyramid();
    }
    
    private void CreatePyramid()
    {
        int cardIndex = 0;
        
        for (int row = 0; row < 7; row++)
        {
            int cardsInRow = row + 1;
            float rowWidth = cardsInRow * CardWidth + (cardsInRow - 1) * HorizontalSpacing;
            float startX = -rowWidth / 2;
            
            for (int col = 0; col < cardsInRow; col++)
            {
                var card = CreateCard(cardIndex);
                float x = startX + col * (CardWidth + HorizontalSpacing);
                float y = row * (CardHeight / 2 + VerticalSpacing);
                card.Position = new Vector2(x, y);
                AddChild(card);
                cardIndex++;
            }
        }
    }
    
    private Node2D CreateCard(int index)
    {
        var cardScene = GD.Load<PackedScene>("res://Card.tscn");
        var card = cardScene.Instantiate<Node2D>();
        // カードの値を設定
        return card;
    }
}