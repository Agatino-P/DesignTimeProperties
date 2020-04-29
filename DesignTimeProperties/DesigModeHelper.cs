using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesignModeHelpers
{
    public static class DModeProp
    {
        private static bool? inDesignMode;

  
        private static bool InDesignMode
        {
            get
            {
                if (inDesignMode == null)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;

                    inDesignMode = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;

                    if (!inDesignMode.GetValueOrDefault(false) && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
                        inDesignMode = true;
                }

                return inDesignMode.GetValueOrDefault(false);
            }
        }


        public static readonly DependencyProperty BackgroundProperty = DependencyProperty
            .RegisterAttached("Background", typeof(Brush), typeof(DModeProp), new PropertyMetadata(BackgroundChanged));

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty
            .RegisterAttached("FontSize", typeof(double), typeof(DModeProp), new PropertyMetadata(14d,FontSizeChanged) );


        public static Brush GetBackground(DependencyObject dependencyObject)
        {
            return (Brush)dependencyObject.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject dependencyObject, Brush value)
        {
            dependencyObject.SetValue(BackgroundProperty, value);
        }

        private static void BackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!InDesignMode)
                return;

            d.SetValue(Control.BackgroundProperty, e.NewValue);
        }


        public static double GetFontSize(DependencyObject dependencyObject)
        {
            return (double)dependencyObject.GetValue(FontSizeProperty);
        }

        public static void SetFontSize(DependencyObject dependencyObject, double value)
        {
            dependencyObject.SetValue(FontSizeProperty, value);
        }

        private static void FontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!InDesignMode)
                return;

            d.SetValue(Control.FontSizeProperty, e.NewValue);
        }


    }
}