using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFODisplay: Command
    {
        public UFODisplay(SpriteBatchMan pSpriteBatchMan, SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, GameObject.Name name, GameSprite.Name spriteName, Composite pTree)
        {
            this.pSpriteBatch = pSpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = pSpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            //this.type = type;
            this.gameObjectName = name;
            this.spriteName = spriteName;
            this.pTree = pTree;
            this.flag = true;
        }

        public override void Execute(float deltaTime, TimeEvent.Name name)
        {
            GameObject pUFO = null;
            switch (this.flag)
            {
                case false:
                    pUFO = new LeftUFO(gameObjectName, spriteName, 800, 600);
                    this.flag = true;
                    break;

                case true:
                    pUFO = new RightUFO(gameObjectName, spriteName, 100, 600);
                    this.flag = false;
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            this.pTree.Add(pUFO);

            // Attached to Group
            pUFO.ActivateGameSprite(this.pSpriteBatch);
            pUFO.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            Random rnd = new Random();
            int num = rnd.Next(20, 50);
            TimerMan.Add(name, this, num);
        }


        //data
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        //private UFOCategory.Type type;
        private GameObject.Name gameObjectName;
        private GameSprite.Name spriteName;
        private Composite pTree;
        private bool flag;
    }
}
