using UnityEngine;

public class NormalModeController : CardGameController
{
    public override int GridRows => 2;
    public override int GridCols => 3;
    public override int[] Numbers => new int[] { 0, 0, 1, 1, 2, 2 };
}