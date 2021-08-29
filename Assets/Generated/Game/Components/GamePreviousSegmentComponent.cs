//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PreviousSegmentComponent previousSegment { get { return (PreviousSegmentComponent)GetComponent(GameComponentsLookup.PreviousSegment); } }
    public bool hasPreviousSegment { get { return HasComponent(GameComponentsLookup.PreviousSegment); } }

    public void AddPreviousSegment(GameEntity newValue) {
        var index = GameComponentsLookup.PreviousSegment;
        var component = (PreviousSegmentComponent)CreateComponent(index, typeof(PreviousSegmentComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePreviousSegment(GameEntity newValue) {
        var index = GameComponentsLookup.PreviousSegment;
        var component = (PreviousSegmentComponent)CreateComponent(index, typeof(PreviousSegmentComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePreviousSegment() {
        RemoveComponent(GameComponentsLookup.PreviousSegment);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPreviousSegment;

    public static Entitas.IMatcher<GameEntity> PreviousSegment {
        get {
            if (_matcherPreviousSegment == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PreviousSegment);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPreviousSegment = matcher;
            }

            return _matcherPreviousSegment;
        }
    }
}