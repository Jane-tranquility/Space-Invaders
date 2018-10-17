using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitAlienColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            //Debug.WriteLine("update: {0}", this);

            base.BaseUpdateBoundingBox(this);
            Debug.WriteLine(" col bbox: {0},{1}", this.poColObj.poColRect.width, this.poColObj.poColRect.height);
            base.Update();
        }

        public Bomb ActivateBomb(Random rnd)
        {
            //Random rnd = new Random();
            int number = rnd.Next(0, 3);
            GameSprite.Name name = GameSprite.Name.BombStraight + number;
            

            Bomb pBomb = null; 
            switch (name)
            {
                case GameSprite.Name.BombStraight:
                    pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), this.x, this.y - (float)0.5 * this.poColObj.poColRect.height);
                    break;

                case GameSprite.Name.BombDagger:
                    pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), this.x, this.y - (float)0.5 * this.poColObj.poColRect.height);
                    break;

                case GameSprite.Name.BombZigZag:
                    pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), this.x, this.y - (float)0.5 * this.poColObj.poColRect.height);
                    break;
                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            this.bomb = pBomb;

            return pBomb;
        }


        // Data
        private Bomb bomb;

    }
}
