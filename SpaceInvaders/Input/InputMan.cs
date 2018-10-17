using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputMan
    {
        private InputMan()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();
            this.pSubjectOne = new InputSubject();
            this.pSubjectTwo = new InputSubject();
            this.pSubjectThree=new InputSubject();

            this.privSpaceKeyPrev = false;
        }

        private static InputMan PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputMan();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }

        public static InputSubject GetKeyOneSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectOne;
        }

        public static InputSubject GetKeyTwoSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectTwo;
        }

        public static InputSubject GetKeyThreeSubject()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectThree;
        }

        public static void Update()
        {
            InputMan pMan = InputMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
            {
                pMan.pSubjectOne.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
            {
                pMan.pSubjectTwo.Notify();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_3) == true)
            {
                pMan.pSubjectThree.Notify();
            }
            // SpaceKey: (with key history) -----------------------------------------------------------
            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.pSubjectSpace.Notify();
            }

            pMan.privSpaceKeyPrev = spaceKeyCurr;

        }

        // Data: ----------------------------------------------
        private static InputMan pInstance = null;
        private bool privSpaceKeyPrev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectOne;
        private InputSubject pSubjectTwo;
        private InputSubject pSubjectThree;
    }
}
