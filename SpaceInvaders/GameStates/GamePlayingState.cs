using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GamePlayingState : GameState
    {
        // ToDO - create a Snd manager
        GameSprite pShip;
        public static int playLives = 3;
        SpriteBatchMan pSpriteBatchMan;
        public static int numOfAliens=55;
        UFOGroup pUFOGroup;
        public override void Handle()
        {

        }

        // strategy()
        public override void LoadContent()
        {
            pSpriteBatchMan = new SpriteBatchMan(1, 1);
            playLives = 3;
            numOfAliens = 55;
            //---------------------------------------------------------------------------------------------------------
            // Manager initialization
            //---------------------------------------------------------------------------------------------------------

            TimerMan.Create(3, 1);
            TextureMan.Create(2, 1);
            ImageMan.Create(5, 2);
            GameSpriteMan.Create(4, 2);
            BoxSpriteMan.Create(3, 1);
            ProxySpriteMan.Create(10, 1);
            if (GameObjectMan.pInstance != null)
            {
                GameObjectMan.Destroy();
            }
            GameObjectMan.Create(3, 1);
            ColPairMan.Create(1, 1);
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);


            //---------------------------------------------------------------------------------------------------------
            // Sound Experiment
            //---------------------------------------------------------------------------------------------------------

            // start up the engine
           // SpaceInvaders.GetInstance().sndEngine = new IrrKlang.ISoundEngine();

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Add(Texture.Name.Shields, "birds_N_shield.tga");
            TextureMan.Add(Texture.Name.Aliens, "aliensNew.tga");
            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            FontMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);
            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            ImageMan.Add(Image.Name.CrabUp, Texture.Name.Aliens, 322.5f, 27.0f, 150.0f, 111.0f);
            ImageMan.Add(Image.Name.CrabDown, Texture.Name.Aliens, 324.0f, 180.0f, 150.0f, 114.0f);
            ImageMan.Add(Image.Name.OctopusOut, Texture.Name.Aliens, 54f, 28.5f, 168f, 111f);
            ImageMan.Add(Image.Name.OctopusIn, Texture.Name.Aliens, 54f, 180f, 168f, 111f);
            ImageMan.Add(Image.Name.SquidOut, Texture.Name.Aliens, 612f, 25.5f, 117f, 117f);
            ImageMan.Add(Image.Name.SquidIn, Texture.Name.Aliens, 612f, 180f, 117f, 117f);
            ImageMan.Add(Image.Name.Missile, Texture.Name.Shields, 73, 53, 5, 4);
            ImageMan.Add(Image.Name.Ship, Texture.Name.Shields, 10, 93, 30, 18);
            ImageMan.Add(Image.Name.Wall, Texture.Name.Shields, 40, 185, 20, 10);
            ImageMan.Add(Image.Name.BombStraight, Texture.Name.Shields, 111, 101, 5, 49);
            ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Shields, 132, 100, 20, 50);
            ImageMan.Add(Image.Name.BombCross, Texture.Name.Shields, 219, 103, 19, 47);
            ImageMan.Add(Image.Name.Brick, Texture.Name.Shields, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shields, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shields, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shields, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Shields, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Shields, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shields, 55, 215, 10, 5);
            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens, 84, 505, 225, 96);
            ImageMan.Add(Image.Name.Splash, Texture.Name.Aliens, 573, 486, 183, 115);
            ImageMan.Add(Image.Name.TopSplash, Texture.Name.Aliens, 403, 487, 113, 113);
            ImageMan.Add(Image.Name.PlayerEnd, Texture.Name.Aliens, 558, 336, 222, 110);


            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------


            GameSpriteMan.Add(GameSprite.Name.Crab, Image.Name.CrabUp, 500.0f, 300.0f, 18.0f, 18.0f);
            GameSpriteMan.Add(GameSprite.Name.Octopus, Image.Name.OctopusOut, 50, 200, 20, 20);
            GameSpriteMan.Add(GameSprite.Name.Squid, Image.Name.SquidOut, 300, 400, 16, 16);

            GameSpriteMan.Add(GameSprite.Name.Missile, Image.Name.Missile, 0, 0, 2, 10);
            pShip = GameSpriteMan.Add(GameSprite.Name.ShipImage, Image.Name.Ship, 40, 20, 30, 14);
            GameSpriteMan.Add(GameSprite.Name.Ship, Image.Name.Ship, 500, 100, 30, 14);
            GameSpriteMan.Add(GameSprite.Name.WallHorizontal, Image.Name.Wall, 448, 900, 850, 30);
            GameSpriteMan.Add(GameSprite.Name.WallVertical, Image.Name.Wall, 50, 448, 30, 950);
            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 50, 448, 20, 14);

            GameSpriteMan.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 200, 200, 10, 20);
            GameSpriteMan.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, 100, 100, 5, 20);
            GameSpriteMan.Add(GameSprite.Name.BombDagger, Image.Name.BombCross, 100, 100, 10, 20);

            GameSpriteMan.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 10, 5);
            GameSpriteMan.Add(GameSprite.Name.Splash, Image.Name.Splash, 0, 0, 18, 18);
            GameSpriteMan.Add(GameSprite.Name.TopSplash, Image.Name.TopSplash, 0, 0, 20, 14);
            GameSpriteMan.Add(GameSprite.Name.PlayerEnd, Image.Name.PlayerEnd, 0, 0, 40, 20);


            //---------------------------------------------------------------------------------------------------------
            // Create BoxSprite
            //---------------------------------------------------------------------------------------------------------

            BoxSpriteMan.Add(BoxSprite.Name.Box1, 550.0f, 500.0f, 50.0f, 150.0f, new Azul.Color(1.0f, 1.0f, 1.0f, 1.0f));
            BoxSpriteMan.Add(BoxSprite.Name.Box2, 550.0f, 100.0f, 50.0f, 100.0f);


            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            SpriteBatch pSB_Box = pSpriteBatchMan.Add(SpriteBatch.Name.Boxes);
            SpriteBatch pSB_Aliens = pSpriteBatchMan.Add(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Texts = pSpriteBatchMan.Add(SpriteBatch.Name.Texts);
            SpriteBatch pSB_Shields = pSpriteBatchMan.Add(SpriteBatch.Name.Shields);
            SpriteBatch pSB_Walls = pSpriteBatchMan.Add(SpriteBatch.Name.Walls);
            SpriteBatch pSB_UFOs = pSpriteBatchMan.Add(SpriteBatch.Name.UFOs);
            SpriteBatch pSB_Bombs = pSpriteBatchMan.Add(SpriteBatch.Name.Bombs);
            pSB_Aliens.Attach(pShip);
            pSB_Walls.display = false;
            pSB_Box.display = false;


            //---------------------------------------------------------------------------------------------------------
            //Font
            //---------------------------------------------------------------------------------------------------------
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "SCORE<1>  HIGH-SCORE  SCORE<2>", Glyph.Name.Consolas36pt, 200, 680);
            FontMan.Add(pSpriteBatchMan, Font.Name.ScoreOne, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 240, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.HighestScore, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 440, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.ScoreTwo, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 650, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.PlayerLives, SpriteBatch.Name.Texts, "X"+playLives, Glyph.Name.Consolas36pt, 52, 20);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            Simulation.SetState(Simulation.State.Realtime);

            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            //pBombRoot.ActivateCollisionSprite(pSB_Box);



            GameObjectMan.Attach(pBombRoot);

            //---------------------------------------------------------------------------------------------------------
            // Walls
            //---------------------------------------------------------------------------------------------------------

            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pSB_Walls);
            //pWallGroup.ActivateCollisionSprite(pSB_Box);

            WallFactory WF = new WallFactory(pSpriteBatchMan, SpriteBatch.Name.Walls, SpriteBatch.Name.Boxes, pWallGroup);
            WF.Create( WallCategory.Type.Bottom, GameObject.Name.WallBottom, 448, 20, 850, 30);
            WF.Create(WallCategory.Type.Top, GameObject.Name.WallTop, 448, 650, 850, 30);
            WF.Create(WallCategory.Type.Left, GameObject.Name.WallLeft, 50, 448, 30, 950);
            WF.Create(WallCategory.Type.Right, GameObject.Name.WallRight, 846, 448, 30, 950);


            GameObjectMan.Attach(pWallGroup);


            //---------------------------------------------------------------------------------------------------------
            // Missile
            //---------------------------------------------------------------------------------------------------------

            // Missile Root
            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pMissileGroup.ActivateGameSprite(pSB_Aliens);
            pMissileGroup.ActivateCollisionSprite(pSB_Box);
            GameObjectMan.Attach(pMissileGroup);



            //---------------------------------------------------------------------------------------------------------
            // Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GameObjectMan.Attach(pShipRoot);
            pMissileGroup.ActivateGameSprite(pSB_Aliens);
            pMissileGroup.ActivateCollisionSprite(pSB_Box);

            ShipMan.Create(pSpriteBatchMan);

            //pShipRoot.Print();

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            // Create the factory ... prototype
            Composite pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 300.0f, 300.0f);
            GameObjectMan.Attach(pShieldRoot);
            pShieldRoot.ActivateCollisionSprite(pSB_Box);
            pShieldRoot.ActivateGameSprite(pSB_Shields);

            ShieldFactory SF = new ShieldFactory(pSpriteBatchMan, SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);
            float brickWidth = 10.0f;
            float brickHeight = 5.0f;
            for (int i = 0; i < 4; i++)
            {
                float start_x = 150.0f + 180 * i;
                float start_y = 100.0f;
                float off_x = 0;
                SF.SetParent(pShieldRoot);
                GameObject pShieldGrid = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

                {
                    int j = 0;

                    GameObject pColumn;

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);
                    SF.SetParent(pColumn);

                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldGrid);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                }

            }
            // load by column


            //pShieldRoot.Print();

            //Debug.WriteLine("-------------------");

            //ForwardIterator pFor = new ForwardIterator(pShieldRoot);

            //Component pNode = pFor.First();
            //while (!pFor.IsDone())
            //{
            //    pNode.DumpNode();

            //    pNode = pFor.Next();
            //}

            //Debug.WriteLine("-------------------");

            //ReverseIterator pRev = new ReverseIterator(pShieldRoot);

            //Component pNode2 = pRev.First();
            //while (!pRev.IsDone())
            //{
            //    pNode2.DumpNode();

            //    pNode2 = pRev.Next();
            //}

            //Debug.WriteLine("-------------------");


            //---------------------------------------------------------------------------------------------------------
            // display 55 aliens on the screen 
            //---------------------------------------------------------------------------------------------------------

            //create the AlienRoot
            Composite pAlienRoot = (Composite)new AlienRoot(GameObject.Name.AlienRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            AlienFactory AF = new AlienFactory(pSpriteBatchMan, SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes, pAlienRoot);
            GameObjectMan.Attach(pAlienRoot);

            GameObject pAlienGrid = AF.Create(AlienCategory.Type.AlienGrid, GameObject.Name.AlienRoot);

            //create the AlienCloumn
            GameObject pAlienColumn;

            for (int i = 0; i < 11; i++)
            {
                AF.SetParent(pAlienGrid);
                pAlienColumn = AF.Create(AlienCategory.Type.AlienColumn, GameObject.Name.AlienColumn);

                AF.SetParent(pAlienColumn);
                AF.Create(AlienCategory.Type.Squid, GameObject.Name.Squid, 300.0f + 25 * i, 550.0f);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, 300.0f + 25 * i, 525.0f);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, 300.0f + 25 * i, 500.0f);
                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, 300.0f + 25 * i, 475.0f);
                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, 300.0f + 25 * i, 450.0f);
            }


            //create AnimationSprite and attach 2 images to the animation sprite
            //and add them to the TimerManager
            //float time;

            AnimationSprite pAniSquid = new AnimationSprite(GameSprite.Name.Squid);
            pAniSquid.Attach(Image.Name.SquidIn);
            pAniSquid.Attach(Image.Name.SquidOut);
            TimerMan.Add(TimeEvent.Name.SquidAnimation, pAniSquid, 1F);
            AnimationSprite pAniCrab = new AnimationSprite(GameSprite.Name.Crab);
            pAniCrab.Attach(Image.Name.CrabDown);
            pAniCrab.Attach(Image.Name.CrabUp);
            TimerMan.Add(TimeEvent.Name.CrabAnimation, pAniCrab, 1F);
            AnimationSprite pAniOcto = new AnimationSprite(GameSprite.Name.Octopus);
            pAniOcto.Attach(Image.Name.OctopusIn);
            pAniOcto.Attach(Image.Name.OctopusOut);
            TimerMan.Add(TimeEvent.Name.OctopusAnimation, pAniOcto, 1F);
            Movement pAlienMove = new Movement(pAlienRoot, pSB_Bombs, pSB_Box, pBombRoot);
            TimerMan.Add(TimeEvent.Name.AlienGridMovement, pAlienMove, 1F);

            //---------------------------------------------------------------------------------------------------------
            // UFO
            //---------------------------------------------------------------------------------------------------------
            pUFOGroup = new UFOGroup(GameObject.Name.UFOGroup, GameSprite.Name.NullObject, 1.0f, 1.0f);
            GameObjectMan.Attach(pUFOGroup);
            UFODisplay pUFODisplay = new UFODisplay(pSpriteBatchMan, SpriteBatch.Name.UFOs, SpriteBatch.Name.Boxes, GameObject.Name.UFO, GameSprite.Name.UFO, pUFOGroup);
            TimerMan.Add(TimeEvent.Name.DisplayUFO, pUFODisplay, 40f);
            //UFODisplay pUFODisplayRight = new UFODisplay(SpriteBatch.Name.UFOs, SpriteBatch.Name.Boxes, GameObject.Name.UFO, GameSprite.Name.UFO, UFOCategory.Type.RightMovingUFO, pUFOGroup);
            //TimerMan.Add(TimeEvent.Name.DisplayUFO, pUFODisplayRight, 20f);


            //GameObject pUFO = new LeftUFO(GameObject.Name.UFO, GameSprite.Name.UFO, 800, 700);
            ////GameObjectMan.Attach(pUFO);
            //pUFO.ActivateGameSprite(pSB_UFOs);
            //pUFO.ActivateCollisionSprite(pSB_Box);
            // UFO pUFO=new UFO(GameObject.Name.UFO, GameSprite.Name.UFO,)
            //pUFOGroup.ActivateGameSprite(pSB_UFOs);
            //pUFOGroup.ActivateCollisionSprite(pSB_Box);
            //GameObjectMan.Attach(pUFOGroup);


            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------
            ColPair pColPair;
            // associate in a collision pair

            //bomb vs ship(player)
            pColPair = ColPairMan.Add(ColPair.Name.Bombs_Player, pBombRoot, pShipRoot);
            pColPair.Attach(new CheckPlayerStatusObserver(pSpriteBatchMan));
            pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "explosion.wav"));
            pColPair.Attach(new SwitchToSplashObserver(GameSprite.Name.PlayerEnd, pSpriteBatchMan));
            pColPair.Attach(new RemoveBombObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipEndObserver());

            // Missile vs Wall 
          
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new RemoveMissileObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "shoot.wav"));
            //pColPair.Attach(new SwitchToSplashObserver(GameSprite.Name.TopSplash));

            // Bomb vs Bottom
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, pBombRoot, pWallGroup);
            pColPair.Attach(new RemoveBombObserver(pSpriteBatchMan));

            // Missile vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
            pColPair.Attach(new RemoveMissileObserver(pSpriteBatchMan));
            pColPair.Attach(new RemoveBrickObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipReadyObserver());
            //pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "fastinvader1.wav"));
            // pColPair.Attach(new SndObserver(sndEngine, "fastinvader1.wav"));

            //bomb vs shield
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            pColPair.Attach(new RemoveBrickObserver(pSpriteBatchMan));
            pColPair.Attach(new RemoveBombObserver(pSpriteBatchMan));
            //pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "fastinvader1.wav"));

            //alien vs wall
            pColPair = ColPairMan.Add(ColPair.Name.Aleins_Wall, pAlienRoot, pWallGroup);
            pColPair.Attach(new GridObserver());

            //UFO vs wall
            pColPair = ColPairMan.Add(ColPair.Name.UFO_Wall, pUFOGroup, pWallGroup);
            pColPair.Attach(new RemoveUFOObserver(pSpriteBatchMan));

            //Aliens vs missile
            pColPair = ColPairMan.Add(ColPair.Name.Aliens_Missiles, pAlienRoot, pMissileGroup);
            //pColPair.Attach(new RemoveAlienObserver());
            pColPair.Attach(new RemoveMissileObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "shoot.wav"));
            pColPair.Attach(new SwitchToSplashObserver(GameSprite.Name.Splash, pSpriteBatchMan));
            pColPair.Attach(new AddScoreObserver());

            //missile vs UFO
            pColPair = ColPairMan.Add(ColPair.Name.Missiles_UFOs, pMissileGroup, pUFOGroup);
            pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "shoot.wav"));
            pColPair.Attach(new SwitchToSplashObserver(GameSprite.Name.Splash, pSpriteBatchMan));
            pColPair.Attach(new RemoveMissileObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new AddScoreObserver());

            //missile vs bomb
            pColPair = ColPairMan.Add(ColPair.Name.Bombs_Missiles, pBombRoot, pMissileGroup);
            pColPair.Attach(new SndObserver(SpaceInvaders.GetInstance().sndEngine, "shoot.wav"));
            pColPair.Attach(new SwitchToSplashObserver(GameSprite.Name.Splash, pSpriteBatchMan));
            pColPair.Attach(new RemoveMissileObserver(pSpriteBatchMan));
            pColPair.Attach(new ShipReadyObserver());
           

            ////aliengrid vs brickshields
            //pColPair = ColPairMan.Add(ColPair.Name.Aliens_Shields, pAlienRoot, pShieldRoot);
            //pColPair.Attach(new RemoveShieldsObserver());
        }
        public override void Update(float time)
        {
            // Add your update below this line: ----------------------------
            //in order to render the ship on screen
            pShip.Update();

            //Update the player 1 score
            Font pScoreOne = FontMan.Find(Font.Name.ScoreOne);
            Debug.Assert(pScoreOne != null);
            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            pScoreOne.UpdateMessage("" + pSI.scoreOne);

            //update the player lives
            Font pLives = FontMan.Find(Font.Name.PlayerLives);
            Debug.Assert(pLives != null);
            pLives.UpdateMessage("X" + playLives);

            // Snd update - keeps everything moving and updating smoothly
            SpaceInvaders.GetInstance().sndEngine.Update();

            // Single Step, Free running...
            Simulation.Update(time);

            // Input
            InputMan.Update();

            if (Iterator.GetChild(pUFOGroup) != null)
            {
                SpaceInvaders.GetInstance().sndEngine.Play2D("ufo_highpitch.wav");
            }

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events
                TimerMan.Update(Simulation.GetTotalTime());

                // Do the collision checks
                ColPairMan.Process();

                // walk through all objects and push to flyweight
                GameObjectMan.Update();

                // Delete any objects here...
                DelayedObjectMan.Process();
                
            }

            
        }
        public override void Draw()
        {
            // draw all objects
            pSpriteBatchMan.Draw();
        }
        public override void UnLoadContent()
        {
            TimerMan.Destroy();
            TextureMan.Destroy();
            ImageMan.Destroy(); 
            GameSpriteMan.Destroy();
            BoxSpriteMan.Destroy();
            pSpriteBatchMan.Destroy();
            ProxySpriteMan.Destroy();
            GlyphMan.Destroy();

            //GameObjectMan.Destroy();
            ColPairMan.Destroy();
            FontMan.Destroy();
            ShipMan.Destroy();
        }
    }
}

