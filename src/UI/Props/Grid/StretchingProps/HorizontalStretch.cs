using System;
using System.Linq;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the width of the widget til the width of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class HorizontalStretch : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; } = 2;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// It gets the width of the parent (if it has one) and replaces the widget's width with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            if (widget.IsRoot) return;
            var (x, y, _, h) = widget.Space;
            if (!widget.Props.Contains<VerticalStretch>())
                h = widget.Children.Any() ? widget.Children.Max(c => c.Space.Height) : h;

            var (nw, nonFinishedHorSiblings) = ApplyUtils.StretchedWidthUsingWidthSiblings(widget);
            nonFinishedHorSiblings.ForEach(s =>
            {
                var hp = (IApplicableProp) s.Props.GetByProp<HorizontalStretch>().First();
                hp.Applied += (sender, args) =>
                {
                    ApplicationDone = false;
                    WidgetsSpaceHelper.UpdateSpace(widget, new RectangleF(x, y, nw - s.Space.Width, h));
                    ApplicationDone = true;
                };
            });
            WidgetsSpaceHelper.UpdateSpace(widget,
                new RectangleF(x, y, nw, h));
            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}