

public class EnabledState : IState
{
    private readonly Entity entity;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTOR
    \*------------------------------------------------------------------*/

    public EnabledState(Entity entity) {
        this.entity = entity;
    }

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/


    /*------------------------------------------------------------------*\
    |*							IState IMPLEMENTATION
    \*------------------------------------------------------------------*/

    public void Update() { }

    public void OnEntry() {
    }

    public void OnExit() {
    }

    public override string ToString() => "Enabled";


}
