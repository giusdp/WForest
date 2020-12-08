using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Properties.Shaders;
using WForest.UI.Widgets;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTree
{
    public class WidgetTreeVisitor
    {
        private readonly InteractionHelper _interactionHelper;

        public WidgetTreeVisitor()
        {
            _interactionHelper = new InteractionHelper();
        }

        public static void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            void DrawWidgets(List<Tree<Widget>> widgets)
            {
                if (widgets.Count == 0) return;

                var (rounded, nonRounded) = RoundedPartition(widgets.Select(w => (WidgetTree) w).ToList());

                nonRounded.ForEach(w => w.DrawWidget(spriteBatch));

                if (!rounded.Any()) return;

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    effect: ShaderDb.Rounded);

                foreach (var (w, r) in rounded.Select(w => (w, w.Properties.OfType<Rounded>().First())))
                {
                    r.ApplyParameters(w.Data.Space.Width, w.Data.Space.Height, r.Radius);
                    w.DrawWidget(spriteBatch);
                }

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            }

            TreeVisitor<Widget>.ApplyToTreeLevelByLevel(widgetTree, DrawWidgets);
        }

        public void ApplyPropertiesOnTree(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTreeFromLeaves(widgetTree, w => ((WidgetTree) w).ApplyProperties());
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
            => _interactionHelper.CheckHovering(widgetTree, mouseLoc);

        private static (List<WidgetTree>, List<WidgetTree>) RoundedPartition(List<WidgetTree> widgets)
        {
            var roundedWidgets = new List<WidgetTree>();
            var nonRoundedWidgets = new List<WidgetTree>();
            foreach (var widget in widgets)
            {
                if (widget.Properties.OfType<Rounded>().Any()) roundedWidgets.Add(widget);
                else nonRoundedWidgets.Add(widget);
            }

            return (roundedWidgets, nonRoundedWidgets);
        }
    }
}