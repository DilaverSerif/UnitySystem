using System.Collections;
using System.Collections.Generic;
using _SYSTEMS_._Building_System_;
using UnityEngine;

public class TestRaf : Workshop
{
    protected override void LevelUp(int level)
    {
        Debug.Log("Level Up");
    }

    protected override void ThrowToGround()
    {
        throw new System.NotImplementedException();
    }

    protected override void ThrowToBag()
    {
        throw new System.NotImplementedException();
    }
}