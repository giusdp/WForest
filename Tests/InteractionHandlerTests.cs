using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.WidgetTree;
using WForest.Utilities;

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionHelperTests
    {
        [Test]
        public void IsHovered_LocationInside_ReturnsTrue()
        {
            var w = new WidgetTree(WidgetFactory.Container(new Rectangle(0,0,540,540)));
            var b = WidgetInteractionSetter.GetHoveredWidget(w, new Point(332, 43)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void IsHovered_NotInside_ReturnsFalse()
        {
            var w= new WidgetTree(WidgetFactory.Container(new Rectangle(0,0,540,540)));
            var b = WidgetInteractionSetter.GetHoveredWidget(w, new Point(332, 678)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.False);
        }
    }
}