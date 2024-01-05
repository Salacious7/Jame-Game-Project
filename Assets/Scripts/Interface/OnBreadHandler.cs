using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OnBreadHandler
{
    void OnSuccess(Bread bread);
    void OnFailed(int amount);
}
