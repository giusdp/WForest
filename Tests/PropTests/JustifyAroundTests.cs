using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Props.Grid.JustifyProps;
using WForest.UI.Widgets.Interfaces;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class JustifyAroundTests
    {
        private JustifyAround _justifyAround;
        private IWidget _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyAround = new JustifyAround();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
        }

        [Test]
        public void ZeroChild_NothingHappens()
        {
            Assert.That(() => _justifyAround.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_ThrowsExc()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 130, 120));
            _root.AddChild(child);

            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 110)));

            Assert.That(() => _justifyAround.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void RowWithOneChild_PutsAtCenter()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 130, 120));
            _root.AddChild(child);
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(_justifyAround);

            ApplyProps(_root);
            var expected = new Rectangle(575, 0, 130, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ColWithOneChild_PutsAtCenter()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 130, 120));
            _root.AddChild(child);
            _root.WithProp(PropertyFactory.Column());
            _root.WithProp(_justifyAround);

            ApplyProps(_root);
            var expected = new Rectangle(0, 300, 130, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithTwoW_SpaceAround()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new Rectangle(0, 0, 120, 110));
            _root.AddChild(child);
            _root.AddChild(child1);

            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(_justifyAround);

            ApplyProps(_root);

            var exp = new Rectangle(343, 0, 130, 120);
            var exp1 = new Rectangle(817, 0, 120, 110);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
        }
    }
}