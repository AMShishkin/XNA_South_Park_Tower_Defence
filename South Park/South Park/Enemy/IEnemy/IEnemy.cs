using Microsoft.Xna.Framework;

namespace South_Park
{
    interface IEnemy
    {
        Vector2 Location { get; set; }

        EnemyCondition Condition { get; set; }

     //   EnemyType Type { get; set; }
   
        sbyte ColumnIndex { get; set; }
    
        sbyte Direction { get; set; }
       
        sbyte RowIndex { get; set; }

        ArmorType ArmorTypeOne { get; set; }
        ArmorType ArmorTypeTwo { get; set; }



       
        
       
        float Step { get; set; }
        
        float Health { get; set; }
        
        bool CheckCollision(Rectangle mask);
     
        void Stopped();
    }
}