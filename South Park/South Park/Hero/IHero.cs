using Microsoft.Xna.Framework;

namespace South_Park
{
    interface IHero
    {
        sbyte RowIndex { get; set; }

        sbyte ColumnIndex { get; set; }

        Condition Condition { get; set; }
        MovementCondition MovementCondition { get; set; }
        MovementDirectionCondition MDCondition { get; set; }
        CollisionCondition CollisionCondition { get; set;}
        string FCondition { get; set; }

        Vector2 Location { get; set; }


        short InactivityTime { get; set; }

        byte Level { get; set; }
 
        float Experience { get; set; }
  

        float Step { get; set; }
        


        Rectangle GetBounds();

        bool CheckCollision(Rectangle mask);
    }
}