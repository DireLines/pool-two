using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag { None };

public class TagHandler : MonoBehaviour
{
    public List<Tag> tags = new List<Tag>();
}

public static class TagExtension
{
    public static bool HasTag(this GameObject g, Tag tag)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        return (handler) ? handler.tags.Contains(tag) : false;
    }

    public static bool HasTags(this GameObject g, List<Tag> tags)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        foreach (Tag tag in tags)
            if (!handler.tags.Contains(tag))
                return false;
        return true;
    }

    public static bool HasAnyTag(this GameObject g, List<Tag> tags)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        foreach (Tag tag in tags)
            if (handler.tags.Contains(tag))
                return true;
        return false;
    }
}
