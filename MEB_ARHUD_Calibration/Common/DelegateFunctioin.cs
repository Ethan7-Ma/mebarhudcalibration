using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MEB_ARHUD_Calibration.Common
{
    public delegate void CameraNewFrameDelegateFunction(Bitmap bitmap);
    public delegate void ShowMessageDelegateFunction(string message);
    public delegate void ResultsStateUpdateDelegateFunction();
    public delegate void DeviceStateChangedDelegateFunction(bool connected);
    public delegate void TestFinishedDelegateFunction(bool success);
    public delegate void TestSerialCountUpdateDelegateFunction(int count);

    class DelegateFunctioin
    {
        
    }
}
