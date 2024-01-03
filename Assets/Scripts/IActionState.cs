using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionState
{
    void Fight();
    void UseItem();
    void SpecialPower();
    void Defend();
}
