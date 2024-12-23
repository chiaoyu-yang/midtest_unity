using UnityEngine;

public class EasyModeController : CardGameController
{
    public override int GridRows => 2;
    public override int GridCols => 4;
    public override int[] Numbers => new int[] { 0, 0, 1, 1, 2, 2, 3, 3 };
}