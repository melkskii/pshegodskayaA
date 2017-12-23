﻿using Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _8Ball
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 


    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Exception Simple", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
            //Logger.Log.Error("Unhandled Exception" + e.Exception);
        }


    }
}
