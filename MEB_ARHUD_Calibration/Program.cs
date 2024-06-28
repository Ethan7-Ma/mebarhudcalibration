using System;
using System.Windows.Forms;

namespace MEB_ARHUD_Calibration {
    static class Program {
        [STAThread]
        static void Main() {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Main());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Console.WriteLine($"CurrentDomain throw Exeption {e.ExceptionObject}");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            Console.WriteLine($"Application throw Exeption {e.Exception}");
        }
    }
}
