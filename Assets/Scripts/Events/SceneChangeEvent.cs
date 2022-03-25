using SimpleEventBus.Events;

public class SceneChangeEvent : EventBase
{
    public string Tag { get; }
    public SceneChangeEvent(string tag)
    {
        Tag = tag;
    }
}