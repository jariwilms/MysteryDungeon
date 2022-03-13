using System.Collections.Generic;

namespace MysteryDungeon.Core.Interface
{
    class WidgetGroup
    {
        public List<Widget> Widgets { get; }
        public bool IsVisible;

        public WidgetGroup()
        {

        }

        public void AddWidget(Widget widget, int depth = 0) //Todo: add widget depth so only the front most can consume the input
        {
            Widgets.Add(widget); //voorlopig
        }

        public void RemoveWidget(Widget widget)
        {

        }
    }
}
