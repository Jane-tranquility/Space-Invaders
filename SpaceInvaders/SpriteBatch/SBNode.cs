using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SBNode_Link : DLink
    {

    }

    public class SBNode : SBNode_Link
    {

        public SBNode()
            : base()
        {
            this.pSpriteBase = null;
            this.pBackSBNodeMan = null;
        }

        //~SBNode()
        //{
        //    this.pSpriteBase = null;
        //}

        //public void Set(GameSprite.Name name)
        //{
        //    // Go find it
        //    this.pSpriteBase = GameSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(BoxSprite.Name name)
        //{
        //    // Go find it
        //    this.pSpriteBase = BoxSpriteMan.Find(name);
        //    Debug.Assert(this.pSpriteBase != null);
        //}

        //public void Set(ProxySprite pNode)
        //{
        //    // associate it
        //    Debug.Assert(pNode != null);
        //    this.pSpriteBase = pNode;
        //}

        public void Set(SpriteBase pNode, SBNodeMan _pSBNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSBNode(this);

            Debug.Assert(_pSBNodeMan != null);
            this.pBackSBNodeMan = _pSBNodeMan;

        }

        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }

        public SBNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSBNodeMan != null);
            return this.pBackSBNodeMan.GetSpriteBatch();
        }

        public void Wash()
        {
            this.pSpriteBase = null;
        }

        // Data: ----------------------------------------------
        private SpriteBase pSpriteBase;
        private SBNodeMan pBackSBNodeMan;
    }
}
