using System.Linq;
using Serilog;

namespace WForest.UI.Properties.Grid.ItemCenter
{
    public class ItemCenter : IProperty
    {
        public int Priority { get; } = 1;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widgetNode.Data} has no children to item-center.");
                return;
            }

            var rowProps = widgetNode.Properties.OfType<Row.Row>().ToList();

            if (rowProps.Any())
            {
                // CenterHelper.CenterByRow(widgetNode, rowProps.First().Rows);
            }
            else
            {
                var colProps = widgetNode.Properties.OfType<Column.Column>().ToList();
                if (colProps.Any())
                {
                    // CenterHelper.CenterByColumn(widgetNode, colProps.First().Columns);
                }
            }
        }
    }
}