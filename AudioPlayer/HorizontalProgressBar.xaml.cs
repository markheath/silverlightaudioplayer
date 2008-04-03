using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AudioPlayer
{
    public partial class HorizontalProgressBar : UserControl
    {
        public static readonly DependencyProperty FillBrushProperty
            = DependencyProperty.Register("Fill",typeof(Brush),typeof(HorizontalProgressBar),            
            new PropertyChangedCallback(FillBrushPropertyChanged));
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register("Value", typeof(double), typeof(HorizontalProgressBar),
            new PropertyChangedCallback(ValuePropertyChanged));


        public HorizontalProgressBar()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(HorizontalProgressBar_SizeChanged);
        }

        void HorizontalProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            progressRectClipGeometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
        }

        public Brush Fill
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }
        
        static void FillBrushPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((HorizontalProgressBar)dependencyObject).RectProgress.Fill = (Brush)args.NewValue;
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        static void ValuePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            HorizontalProgressBar bar = (HorizontalProgressBar)dependencyObject;
            bar.RectProgress.Width = (double)args.NewValue * bar.ActualWidth;
        }

    }
}
