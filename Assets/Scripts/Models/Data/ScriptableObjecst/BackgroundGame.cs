using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BackgroundGame", menuName = "ScriptableObject/BackgroundGame")]
public class BackgroundGame : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public Sprite Sprite => _sprite;
}