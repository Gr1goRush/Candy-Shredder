using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObject/Sound")]
public class Sound : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _music;
    [SerializeField] private bool _isLoop;

    public string Name => _name;
    public AudioClip Music => _music;
    public bool IsLoop => _isLoop;
}
