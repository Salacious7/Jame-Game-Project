using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OnEventHandler
{
    public void OnSuccess();
    public void OnFailed(int amount);
}
