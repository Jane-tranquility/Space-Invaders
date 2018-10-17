using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        public AlienFactory(SpriteBatchMan pSpriteMan, SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = pSpriteMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = pSpriteMan.Find(collisionSpriteBatch);
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

        ~AlienFactory()
        {
            this.pSpriteBatch = null;
        }

        public GameObject Create(AlienCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pAlien = null;

            switch (type)
            {
                case AlienCategory.Type.Squid:
                    pAlien = new Squid(gameName, GameSprite.Name.Squid, posX, posY);
                    break;

                case AlienCategory.Type.Crab:
                    pAlien = new Crab(gameName, GameSprite.Name.Crab, posX, posY);
                    break;

                case AlienCategory.Type.Octopus:
                    pAlien = new Octopus(gameName, GameSprite.Name.Octopus, posX, posY);
                    break;

                case AlienCategory.Type.AlienColumn:
                    pAlien = new AlienColumn(gameName, GameSprite.Name.NullObject, posX, posY);
                    break;

                case AlienCategory.Type.AlienGrid:
                    pAlien = new AlienGrid(gameName, GameSprite.Name.NullObject, posX, posY);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add to the tree
            this.pTree.Add(pAlien);

            // Attached to Group
            pAlien.ActivateGameSprite(this.pSpriteBatch);
            pAlien.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pAlien;
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;
    }
}