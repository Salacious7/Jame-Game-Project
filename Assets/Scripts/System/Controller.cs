using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controller", menuName = "ScriptableObject/Controller")]
public class Controller : ScriptableObject
{
    public Swan Swan { get; set; }
}
