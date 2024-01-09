using UnityEngine;

[CreateAssetMenu(fileName = "Gallery", menuName = "ScriptableObjects/Gallery", order = 1)]
public class Gallery : ScriptableObject
{
    public Sprite galleryImage;
    public string galleryDescription;
}

