using System.Collections.Generic;

namespace MysteryDungeon.Core.UI.Widgets
{
    internal class WidgetGroup
    {
        public List<Widget> Widgets { get; }
        public bool IsVisible;

        public WidgetGroup(bool isVisible = true)
        {
            IsVisible = isVisible;
        }

        public void AddWidget(Widget widget, int depth = 0) //Todo: add widget depth so only the front most can consume input
        {
            Widgets.Add(widget); //voorlopig
        }

        public void RemoveWidget(Widget widget)
        {

        }
    }
}
