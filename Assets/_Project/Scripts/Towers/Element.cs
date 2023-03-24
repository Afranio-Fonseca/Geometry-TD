using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Element : ScriptableObject
{
    public string enchantmentName;
    public Color color;
    public Element[] advantages;
    public Element[] disadvantages;
}
