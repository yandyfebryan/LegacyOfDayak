using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Section
{
    public string sectionNameEN; // English version of the section name
    public string sectionNameID; // Indonesian version of the section name
    public List<Thumbnail> thumbnails;
}