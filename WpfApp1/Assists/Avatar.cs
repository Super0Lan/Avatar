using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Events;

namespace WpfApp1.Assists
{
    [TemplatePart(Name = ElementRoot, Type = typeof(Image))]
    public class Avatar : ContentControl
    {
        private const string ElementRoot = "PART_Content";

        private Image _image;

        public Avatar() : base()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _image = GetTemplateChild(ElementRoot) as Image;
            if (_image != null)
            {
                _image.ImageFailed += _image_ImageFailed;
            }
        }

        private void _image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            RaiseEvent(new RoutedExceptionEventArgs(ImageFailedEvent,this,e.ErrorException));
        }

        /// <summary>
        /// ImageFailedEvent is a routed event.
        /// </summary>
        public static readonly RoutedEvent ImageFailedEvent = EventManager.RegisterRoutedEvent("ImageFailed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedExceptionEventArgs>), typeof(Avatar));

        /// <summary>
        /// Raised when there is a failure in image.
        /// </summary>
        public event EventHandler<RoutedExceptionEventArgs> ImageFailed
        {
            add { AddHandler(ImageFailedEvent, value); }
            remove { RemoveHandler(ImageFailedEvent, value); }
        }

        #region 尺寸
        public string Size
        {
            get { return (string)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(string), typeof(Avatar), new PropertyMetadata("large", SizePropertyChanged));

        private static void SizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Avatar avatar)
            {
                if (double.TryParse(e.NewValue?.ToString(), out double size))
                {
                    avatar.Width = avatar.Height = size;
                }
            }
        }
        #endregion

        #region 形状
        public Shape Shape
        {
            get { return (Shape)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(Shape), typeof(Avatar), new PropertyMetadata(Shape.Circle));
        #endregion

        #region 图片头像的资源地址
        public ImageSource Src
        {
            get { return (ImageSource)GetValue(SrcProperty); }
            set { SetValue(SrcProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Src.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SrcProperty =
            DependencyProperty.Register("Src", typeof(ImageSource), typeof(Avatar), new PropertyMetadata(null));
        #endregion

        #region 描述如何调整内容大小以填充为其分配的空间。

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(Avatar), new PropertyMetadata(Stretch.UniformToFill));

        #endregion

        #region 表示矩形的角的半径。
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Avatar), new PropertyMetadata(new CornerRadius(4)));
        #endregion


    }

    public enum Shape
    {
        Circle,
        Square,
    }
}
