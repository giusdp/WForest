using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Props.Grid.ItemProps;
using WForest.Utilities;
using WForest.Widgets.Interfaces;
using static Tests.Utils.HelperMethods;

namespace Tests.PropTests
{
    [TestFixture]
    public class ItemBaseTests
    {
        private ItemBase _itemBase;
        private IWidget _root;
        private IWidget _child;

        public ItemBaseTests()
        {
            _itemBase = new ItemBase();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
            _child = WidgetFactory.Container(20, 20);
        }

        [SetUp]
        public void BeforeEach()
        {
            _itemBase = new ItemBase();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
            _child = WidgetFactory.Container(20, 20);
            _root.AddChild(_child);
        }

        [Test]
        public void ApplyOn_NoRowOrCol_ThrowsIncompatibleWidget()
        {
            Assert.That(() => _itemBase.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void OnRow_PutChildAtBottom()
        {
            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(_child.Space, Is.EqualTo(new RectangleF(0, 700, 20, 20)));
        }

        [Test]
        public void OnCol_PutChildAtRight()
        {
            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(_child.Space, Is.EqualTo(new RectangleF(1260, 0, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEnd_PutsInLowerRightCorner()
        {
            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyEnd());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(_child.Space, Is.EqualTo(new RectangleF(1260, 700, 20, 20)));
        }

        [Test]
        public void OnColWithJustifyEnd_PutsInLowerRightCorner()
        {
            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.JustifyEnd());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(_child.Space, Is.EqualTo(new RectangleF(1260, 700, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEndTwoWidgets_DoesNotFuckItUp()
        {
            var c1 = WidgetFactory.Container(30, 40);
            _root.AddChild(c1);
            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyEnd());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(_child.Space, Is.EqualTo(new RectangleF(1230, 700, 20, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(1250, 680, 30, 40)));
        }

        [Test]
        public void OnMultipleRows_OffsetsCorrectly()
        {
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
            var c = WidgetFactory.Container(800, 20);
            var c1 = WidgetFactory.Container(300, 40);
            var c2 = WidgetFactory.Container(300, 40);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);
            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyEnd());
            _root.WithProp(PropFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(180, 660, 800, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(980, 640, 300, 40)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(980, 680, 300, 40)));
        }
    }
}