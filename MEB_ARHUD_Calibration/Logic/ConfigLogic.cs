using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Data;

namespace MEB_ARHUD_Calibration.Logic
{
    class ConfigLogic
    {
        private static ConfigLogic instance = null;

        public static ConfigLogic GetInstance()
        {
            if (instance == null)
                instance = new ConfigLogic();
            return instance;
        }

        private ConfigLogic()
        {

        }

        public List<string> List_CameraSerial = new List<string>();
        public List<string> List_CameraConfigFileName = new List<string>();

        public void SaveNextCarInfo(ProjectType type, string vin)
        {
            XMLUtil.UpdateNextCarInfoToSystemConfigXml(@"Config\SysConfig.xml", type, vin);
        }

        public void InitSystemInfos()
        {
            XMLUtil.InitConfigsFromSystemConfigXml(@"Config\SysConfig.xml");
        }

        public void InitCameraDeviceInfo()
        {
            List_CameraConfigFileName.Add(@"Config\ID3\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID4X\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID6X\icImagingControlDeviceState.txt");

            List_CameraSerial.Add("24124037");
            List_CameraSerial.Add("24124166");
            List_CameraSerial.Add("24124169");

            int[] camera_Center_ID3 = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID3\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID3, camera_Center_ID3[0]);
            Config.Camera_MoveY.Add(ProjectType.ID3, camera_Center_ID3[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID3, camera_Center_ID3[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID3, camera_Center_ID3[3]);

            int[] camera_Center_ID4 = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID4X\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID4X, camera_Center_ID4[0]);
            Config.Camera_MoveY.Add(ProjectType.ID4X, camera_Center_ID4[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID4X, camera_Center_ID4[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID4X, camera_Center_ID4[3]);

            int[] camera_Center_ID6 = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID6X\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID6X, camera_Center_ID6[0]);
            Config.Camera_MoveY.Add(ProjectType.ID6X, camera_Center_ID6[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID6X, camera_Center_ID6[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID6X, camera_Center_ID6[3]);

            int[] camera_Center_Audi = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\AUDI\config.xml");
            Config.Camera_MoveX.Add(ProjectType.AUDI, camera_Center_Audi[0]);
            Config.Camera_MoveY.Add(ProjectType.AUDI, camera_Center_Audi[1]);
            Config.Camera_OffsetX.Add(ProjectType.AUDI, camera_Center_Audi[2]);
            Config.Camera_OffsetY.Add(ProjectType.AUDI, camera_Center_Audi[3]);
        }

        public void SaveID3CameraCalibration(int X, int Y)
        {
            Config.Camera_MoveX[ProjectType.ID3] = X;
            Config.Camera_MoveY[ProjectType.ID3] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID3\config.xml", X, Y);
        }

        public void SaveID4XCameraCalibration(int X, int Y)
        {
            Config.Camera_MoveX[ProjectType.ID4X] = X;
            Config.Camera_MoveY[ProjectType.ID4X] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID4X\config.xml", X, Y);
        }

        public void SaveID6XCameraCalibration(int X, int Y)
        {
            Config.Camera_MoveX[ProjectType.ID6X] = X;
            Config.Camera_MoveY[ProjectType.ID6X] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID6X\config.xml", X, Y);
        }

        public void SaveAUDICameraCalibration(int X, int Y)
        {
            Config.Camera_MoveX[ProjectType.AUDI] = X;
            Config.Camera_MoveY[ProjectType.AUDI] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\AUDI\config.xml", X, Y);
        }

        public void SaveID3CameraOffset(int X, int Y)
        {
            Config.Camera_OffsetX[ProjectType.ID3] = X;
            Config.Camera_OffsetY[ProjectType.ID3] = Y;
            XMLUtil.SetCameraOffsetFromProjectConfigXml(@"Config\ID3\config.xml", X, Y);
        }

        public void SaveID4XCameraOffset(int X, int Y)
        {
            Config.Camera_OffsetX[ProjectType.ID4X] = X;
            Config.Camera_OffsetY[ProjectType.ID4X] = Y;
            XMLUtil.SetCameraOffsetFromProjectConfigXml(@"Config\ID4X\config.xml", X, Y);
        }

        public void SaveID6XCameraOffset(int X, int Y)
        {
            Config.Camera_OffsetX[ProjectType.ID6X] = X;
            Config.Camera_OffsetY[ProjectType.ID6X] = Y;
            XMLUtil.SetCameraOffsetFromProjectConfigXml(@"Config\ID6X\config.xml", X, Y);
        }

        public void SaveAUDICameraOffset(int X, int Y)
        {
            Config.Camera_OffsetX[ProjectType.AUDI] = X;
            Config.Camera_OffsetY[ProjectType.AUDI] = Y;
            XMLUtil.SetCameraOffsetFromProjectConfigXml(@"Config\AUDI\config.xml", X, Y);
        }

        public void SaveID3NeedTest(bool NeedTest)
        {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID3", NeedTest + "");
        }

        public void SaveID4XNeedTest(bool NeedTest)
        {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID4X", NeedTest + "");
        }

        public void SaveID6XNeedTest(bool NeedTest)
        {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID6X", NeedTest + "");
        }

        public void SaveAUDINeedTest(bool NeedTest)
        {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "AUDI", NeedTest + "");
        }
    }
}
