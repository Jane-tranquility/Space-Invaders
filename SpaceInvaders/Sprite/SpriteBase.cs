using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public SpriteBase()
            : base()
        {
            this.pBackSBNode = null;
        }

        //~SpriteBase()
        //{
        //}

        public SBNode GetSBNode()
        {
            Debug.Assert(this.pBackSBNode != null);
            return this.pBackSBNode;
        }
        public void SetSBNode(SBNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSBNode = pSpriteBatchNode;
        }

        abstract public void Update();
        abstract public void Render();

        // Data: -------------------------------------------

        // If you remove a SpriteBase initiated by gameObject... its hard to get the spriteBatchNode
        // so have a back pointer to it
        private SBNode pBackSBNode;

    }
}
