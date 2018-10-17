using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallFactory
    {
        public WallFactory(SpriteBatchMan pSpriteBatchMan, SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = pSpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = pSpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }

        public void SetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }

        //~WallFactory()
        //{
        //    this.pSpriteBatch = null;
        //}

        public GameObject Create( WallCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f, float width = 0.0f, float height = 0.0f)
        {
            GameObject pWall = null;

            switch (type)
            {
                case WallCategory.Type.Bottom:
                    pWall = new WallBottom(gameName, GameSprite.Name.WallHorizontal, posX, posY, width, height);
                    break;

                case WallCategory.Type.Top:
                    pWall = new WallTop(gameName, GameSprite.Name.WallHorizontal, posX, posY, width, height);
                    break;

                case WallCategory.Type.Left:
                    pWall = new WallLeft(gameName, GameSprite.Name.WallVertical, posX, posY, width, height);
                    break;

                case WallCategory.Type.Right:
                    pWall = new WallRight(gameName, GameSprite.Name.WallVertical, posX, posY, width, height);
                    break;

                case WallCategory.Type.WallGroup:
                    pWall = new WallGroup( gameName, GameSprite.Name.NullObject, posX, posY);
                    break;
                
                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add to the tree
            this.pTree.Add(pWall);

            // Attached to Group
            pWall.ActivateGameSprite(this.pSpriteBatch);
            pWall.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pWall;
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;
    }
}