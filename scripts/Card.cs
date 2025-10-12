using Godot;
using System;

public partial class Card : Area2D
{
	[Export] public int Rank { get; set; }
	[Export] public string Suit { get; set; }

	private Sprite2D sprite;
	private bool isSelectable = false;

	public bool IsSelectable
	{
		get => isSelectable;
		set
		{
			isSelectable = value;
			Modulate = value ? Colors.White : new Color(0.7f, 0.7f, 0.7f);
		}
	}

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		InputEvent += OnInputEvent;
		LoadCardTexture();
	}

	private void LoadCardTexture()
	{

		string rankStr = Rank switch
		{
			1 => "A",
			11 => "J",
			12 => "Q",
			13 => "K",
			_ => Rank.ToString("D2")
		};

		string path = $"res://assets/textures/cards/card_{Suit}_{rankStr}.png";
		sprite.Texture = GD.Load<Texture2D>(path);
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.Pressed &&
			mouseEvent.ButtonIndex == MouseButton.Left &&
			isSelectable)
		{
			EmitSignal(SignalName.CardClicked, this);
		}
	}

	[Signal]
	public delegate void CardClickedEventHandler(Card card);
}
