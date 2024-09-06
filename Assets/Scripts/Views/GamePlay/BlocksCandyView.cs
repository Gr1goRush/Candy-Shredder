using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCandyView : MonoBehaviour
{
    [SerializeField] private List<Sprite> _candyImages;
    [SerializeField] private List<ListCandyLineView> _blocksCandyLine;

    [Header("Начальное кол-во отображаемых линий с конфетами")]
    [SerializeField] private int _countStart = 3;
    [Header("Значение прироста линий конфет за один шаг")]
    [SerializeField] private int _increment = 1;

    public int CountStart => _countStart;
    public int Increment => _increment;
    public float IncrementPerSecond { get; set; }
    public List<ListCandyLineView> ListCandyLine => _blocksCandyLine;

    public Sprite GetRandomCandyImage()
    {
        var indexRandom = Random.Range(0, _candyImages.Count);
        return _candyImages[indexRandom];
    }
}
