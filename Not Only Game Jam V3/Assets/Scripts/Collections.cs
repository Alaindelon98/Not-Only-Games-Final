public enum S_ActorState
{
    Idle,
    Patrol,
    MoveTowards,
    LookAtSmartPhone,
    RunAway,
    BullyActionIndividual,
    BullyActionGroupal,
    LookAtSmartphone
}
public enum S_JohnState
{
    Idle,
    Patrol,
    MoveTowards,
    DefaultAction,
    RunAway,
    BullyAction
}

public enum S_GameState
{
    ExitPlayGround,
    StartPlayGround,
    ActorsMakeActions,
    StartGame,
    EndGame,
    Tutorial

}
public enum s_PhoneState
{
    TakingPicture,
    PicturePosted,
    Hiding
}