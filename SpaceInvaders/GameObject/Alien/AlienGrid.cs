using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGrid : Composite
    {
        public AlienGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 1);

            this.delta = 20.0f;
            this.flag = true;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitAlienGrid(this);
        }

        public override void VisitWallGroup(WallGroup w)
        {
            // AlienGrid vs WallGroup
            //     Debug.WriteLine("collide: {0} with {1}", this, w);

            // WallRight vs Grid
            GameObject pGameObj = (GameObject)Iterator.GetChild(w);
            ColPair.Collide(this, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            //Debug.WriteLine("update: {0}", this);

            base.BaseUpdateBoundingBox(this);
            //     Debug.WriteLine("grid bbox: {0},{1}", this.poColObj.poColRect.width, this.poColObj.poColRect.height);

            base.Update();
        }

        public override void MoveGrid()
        {
            ForwardIterator pFor = new ForwardIterator(this);
            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;

                pNode = pFor.Next();
            }
        }

        public override void MoveDownGrid()
        {
            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.y -= 30f;

                pNode = pFor.Next();
            }
        }

        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }

        // Data: ---------------
        private float delta;
        public bool flag;
    }
}