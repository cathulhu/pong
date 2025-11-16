using Godot;
using System;

public partial class Menu : Control
{
    public void Quit()
    {
        GetTree().Quit();
    }

    public void StartGame()
    {
        GetTree().ChangeSceneToFile("./game_scene.tscn");
    }
}
