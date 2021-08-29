//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity deltaTimeEntity { get { return GetGroup(GameMatcher.DeltaTime).GetSingleEntity(); } }
    public DeltaTimeComponent deltaTime { get { return deltaTimeEntity.deltaTime; } }
    public bool hasDeltaTime { get { return deltaTimeEntity != null; } }

    public GameEntity SetDeltaTime(float newValue) {
        if (hasDeltaTime) {
            throw new Entitas.EntitasException("Could not set DeltaTime!\n" + this + " already has an entity with DeltaTimeComponent!",
                "You should check if the context already has a deltaTimeEntity before setting it or use context.ReplaceDeltaTime().");
        }
        var entity = CreateEntity();
        entity.AddDeltaTime(newValue);
        return entity;
    }

    public void ReplaceDeltaTime(float newValue) {
        var entity = deltaTimeEntity;
        if (entity == null) {
            entity = SetDeltaTime(newValue);
        } else {
            entity.ReplaceDeltaTime(newValue);
        }
    }

    public void RemoveDeltaTime() {
        deltaTimeEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DeltaTimeComponent deltaTime { get { return (DeltaTimeComponent)GetComponent(GameComponentsLookup.DeltaTime); } }
    public bool hasDeltaTime { get { return HasComponent(GameComponentsLookup.DeltaTime); } }

    public void AddDeltaTime(float newValue) {
        var index = GameComponentsLookup.DeltaTime;
        var component = (DeltaTimeComponent)CreateComponent(index, typeof(DeltaTimeComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDeltaTime(float newValue) {
        var index = GameComponentsLookup.DeltaTime;
        var component = (DeltaTimeComponent)CreateComponent(index, typeof(DeltaTimeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDeltaTime() {
        RemoveComponent(GameComponentsLookup.DeltaTime);
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

    static Entitas.IMatcher<GameEntity> _matcherDeltaTime;

    public static Entitas.IMatcher<GameEntity> DeltaTime {
        get {
            if (_matcherDeltaTime == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DeltaTime);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDeltaTime = matcher;
            }

            return _matcherDeltaTime;
        }
    }
}
