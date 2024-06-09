using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[CreateAssetMenu(fileName ="Question", menuName = "Objects/Question",order = 1)]
public class Question : ScriptableObject
{
    public string questionData;
    public string category;
    public string[] answers;

}