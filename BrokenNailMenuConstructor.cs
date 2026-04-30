using MenuChanger;
using MenuChanger.MenuElements;

namespace BrokenNail
{
    internal class BrokenNailMenuConstructor : ModeMenuConstructor
    {
        public override void OnEnterMainMenu(MenuPage modeMenu)
        {
            Broken_Nail.saveSettings.BrokenNailMode = false;
        }

        public override void OnExitMainMenu()
        {

        }

        public override bool TryGetModeButton(MenuPage modeMenu, out BigButton button)
        {
            button = new BigButton(modeMenu, Broken_Nail.SpriteManager.GetSprite("broken_nail") ,"Broken Nail");
            button.OnClick += () => StartGame();
            return true;
        }

        private static void StartGame()
        {
            MenuChangerMod.HideAllMenuPages();
            Broken_Nail.saveSettings.BrokenNailMode = true;

            UIManager.instance.ContinueGame();
            GameManager.instance.ContinueGame();
        }
    }
}
