using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag { None, Ball, Blocking, Feature, Greenery, Structure };

public class TagHandler : MonoBehaviour
{
    [SerializeField]
    List<Tag> inspector_tags = new List<Tag>();
    public HashSet<Tag> tags = new HashSet<Tag>();

    private void Awake()
    {
        tags.UnionWith(inspector_tags);
    }
}

public static class TagExtension
{
    public static bool HasTag(this GameObject g, Tag tag)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        return (handler) ? handler.tags.Contains(tag) : false;
    }

    public static bool HasTags(this GameObject g, params Tag[] tags)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        if (!handler) return false;
        foreach (Tag tag in tags)
            if (!handler.tags.Contains(tag))
                return false;
        return true;
    }

    public static bool HasAnyTag(this GameObject g, params Tag[] tags)
    {
        TagHandler handler = g.GetComponent<TagHandler>();
        if (!handler) return false;
        foreach (Tag tag in tags)
            if (handler.tags.Contains(tag))
                return true;
        return false;
    }
}
