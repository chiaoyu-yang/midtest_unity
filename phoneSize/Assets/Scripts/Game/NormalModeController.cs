using UnityEngine;

public class NormalModeController : CardGameController
{
    public override int GridRows => 3;
    public override int GridCols => 4;
    public override int[] Numbers => new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
}