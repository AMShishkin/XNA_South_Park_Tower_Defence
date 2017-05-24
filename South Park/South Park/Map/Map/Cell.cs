using System;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class Cell
    {
        private System.Drawing.Size size;
        private Rectangle bounds;


        public Cell(Vector2 location, sbyte i, sbyte j, CellsCondition cellCondition,
            CellsMovementDirectionCondition cellMovemenDirectionCondition, EventHandler collisionWithFireZone)
        {
            size = new System.Drawing.Size(80, 80);

            this.Location = location;
            this.i = i;
            this.j = j;
            this.CellCondition = cellCondition;
            this.CellMovementDirectionCondition = cellMovemenDirectionCondition;
            this.CollisionWithFireZone = collisionWithFireZone;

            bounds = new Rectangle((int)this.Location.X, (int)this.Location.X, 80, 80);
        }

        
        public Vector2 Location;
        
        public sbyte i;
        
        public sbyte j;





        public event EventHandler CollisionWithFireZone;


        public void OnCollision(IEnemy enemy)
        {
            if (CollisionWithFireZone != null) this.CollisionWithFireZone(enemy, null);
        }

        

        public CellsCondition CellCondition;

        public CellsMovementDirectionCondition CellMovementDirectionCondition;



   
        public System.Drawing.Size Size
        {
            get { return size; }
        }
    
        public Rectangle Rectangle
        {
            get 
            {
                bounds.X = (int)Location.X;
                bounds.Y = (int)Location.Y;
                return bounds; 
            }
        }
    }
}
