namespace South_Park
{
    enum Condition
    {
        Alive,
        Dead,
    }

    enum MovementCondition
    {
        Moving,
        Stopped,
    }



    enum MovementDirectionCondition
    {
        MovingLeft,
        MovingLeftUp,
        MovingUp,
        MovingRightUp,
        MovingRight,
        MovingRightDown,
        MovingDown,
        MovingLeftDown,
    }



    enum CollisionCondition
    {
        Faces,
        NotFaces,
    }







    enum CellsMovementDirectionCondition
    {
        DirectionAbsent,
        DirectionLeft,
        DirectionRight,
        DirectionLeftUp,
        DirectionLeftDown,
        DirectionRightUp,
        DirectionRightDown,
        DirectionUp,
        DirectionDown,
    }

    enum CellsCondition
    {
        Free,
        OccupiedByTower,
        OccupiedByObstacle,
        DefeatZoneHero,
        DefeatZoneTower,
        DefeatZoneTowerAndHero,

        Start,
        Finish,
    }
}