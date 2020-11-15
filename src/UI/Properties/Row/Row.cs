namespace PiBa.UI.Properties.Row
{
    public class Row : IProperty
    {
        public int Priority { get; } = 0;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count <= 1)
                return;

            RowHandler.SetWidgetsInRows(widgetNode);
        }
    }
}