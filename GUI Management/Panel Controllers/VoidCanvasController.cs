using UnityEngine;

//CANVAS CONTROLLER PER I PANEL VUOTI CHE DEVONO ANCORA ESSERE IMPLEMENTATI
public class VoidCanvasController : PanelController
{
    public override PanelControllerReturn OnActionPressed() => PanelControllerReturn.None;
    public override PanelControllerReturn OnBackPressed() => PanelControllerReturn.None;
    public override PanelControllerReturn OnPanelMovement(Vector2 movement) => PanelControllerReturn.None;
    public override PanelControllerReturn OnTabMovement(float movement) => PanelControllerReturn.None;
}