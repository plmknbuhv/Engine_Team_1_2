using UnityEngine.UIElements;

public class GGMSplitView : TwoPaneSplitView
{
    public new class UxmlFactory : UxmlFactory<GGMSplitView, UxmlTraits> { }
    public new class UxmlTraits : TwoPaneSplitView.UxmlTraits { }
}