using Godot;
using System;

public partial class Main : Node2D
{
    private PyramidBoard pyramidBoard;

    public override void _Ready()
    {
        pyramidBoard = GetNode<PyramidBoard>("PyramidBoard");
    }
}