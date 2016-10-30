using UnityEngine;

namespace Assets.Scripts.Menus.ContextMenu
{
    public interface IContextMenuScript
    {
        void CloseMenu();
        void OnBtn();
        void OffBtn();
    }
}