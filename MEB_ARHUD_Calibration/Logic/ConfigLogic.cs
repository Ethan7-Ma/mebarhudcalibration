using MEB_ARHUD_Calibration.Common;
using System.Collections.Generic;

namespace MEB_ARHUD_Calibration.Logic {
    class ConfigLogic {
        private static ConfigLogic instance = null;

        public static ConfigLogic GetInstance() {
            if (instance == null)
                instance = new ConfigLogic();
            return instance;
        }

        private ConfigLogic() {

        }

        public List<string> List_CameraConfigFileName = new List<string>();

        public void SaveNextCarInfo(ProjectType type, string vin) {
            XMLUtil.UpdateNextCarInfoToSystemConfigXml(@"Config\SysConfig.xml", type, vin);
        }

        public void LoadSystemConfig() {
            XMLUtil.InitConfigsFromSystemConfigXml(@"Config\SysConfig.xml");
        }

        public void LoadCameraConfig() {
            List_CameraConfigFileName.Add(@"Config\ID3\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID4X\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID6X\icImagingControlDeviceState.txt");

            List_CameraConfigFileName.Add(@"Config\ID3N\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID4XN\icImagingControlDeviceState.txt");
            List_CameraConfigFileName.Add(@"Config\ID6XN\icImagingControlDeviceState.txt");

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

            int[] camera_Center_ID3N = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID3N\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID3N, camera_Center_ID3N[0]);
            Config.Camera_MoveY.Add(ProjectType.ID3N, camera_Center_ID3N[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID3N, camera_Center_ID3N[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID3N, camera_Center_ID3N[3]);

            int[] camera_Center_ID4N = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID4XN\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID4XN, camera_Center_ID4N[0]);
            Config.Camera_MoveY.Add(ProjectType.ID4XN, camera_Center_ID4N[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID4XN, camera_Center_ID4N[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID4XN, camera_Center_ID4N[3]);

            int[] camera_Center_ID6N = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\ID6XN\config.xml");
            Config.Camera_MoveX.Add(ProjectType.ID6XN, camera_Center_ID6N[0]);
            Config.Camera_MoveY.Add(ProjectType.ID6XN, camera_Center_ID6N[1]);
            Config.Camera_OffsetX.Add(ProjectType.ID6XN, camera_Center_ID6N[2]);
            Config.Camera_OffsetY.Add(ProjectType.ID6XN, camera_Center_ID6N[3]);

            int[] camera_Center_AudiN = XMLUtil.GetCameraCenterFromProjectConfigXml(@"Config\AUDIN\config.xml");
            Config.Camera_MoveX.Add(ProjectType.AUDIN, camera_Center_AudiN[0]);
            Config.Camera_MoveY.Add(ProjectType.AUDIN, camera_Center_AudiN[1]);
            Config.Camera_OffsetX.Add(ProjectType.AUDIN, camera_Center_AudiN[2]);
            Config.Camera_OffsetY.Add(ProjectType.AUDIN, camera_Center_AudiN[3]);
        }

        public void SaveID3CameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID3] = X;
            Config.Camera_MoveY[ProjectType.ID3] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID3\config.xml", X, Y);
        }

        public void SaveID4XCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID4X] = X;
            Config.Camera_MoveY[ProjectType.ID4X] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID4X\config.xml", X, Y);
        }

        public void SaveID6XCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID6X] = X;
            Config.Camera_MoveY[ProjectType.ID6X] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID6X\config.xml", X, Y);
        }

        public void SaveAUDICameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.AUDI] = X;
            Config.Camera_MoveY[ProjectType.AUDI] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\AUDI\config.xml", X, Y);
        }

        public void SaveID3NCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID3N] = X;
            Config.Camera_MoveY[ProjectType.ID3N] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID3N\config.xml", X, Y);
        }

        public void SaveID4XNCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID4XN] = X;
            Config.Camera_MoveY[ProjectType.ID4XN] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID4XN\config.xml", X, Y);
        }

        public void SaveID6XNCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.ID6XN] = X;
            Config.Camera_MoveY[ProjectType.ID6XN] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\ID6XN\config.xml", X, Y);
        }

        public void SaveAUDINCameraCalibration(int X, int Y) {
            Config.Camera_MoveX[ProjectType.AUDIN] = X;
            Config.Camera_MoveY[ProjectType.AUDIN] = Y;
            XMLUtil.SetCameraCenterFromProjectConfigXml(@"Config\AUDIN\config.xml", X, Y);
        }


        public void SaveID3NeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID3", NeedTest + "");
        }

        public void SaveID4XNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID4X", NeedTest + "");
        }

        public void SaveID6XNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID6X", NeedTest + "");
        }

        public void SaveAUDINeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "AUDI", NeedTest + "");
        }

        public void SaveID3NNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID3N", NeedTest + "");
        }

        public void SaveID4XNNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID4XN", NeedTest + "");
        }

        public void SaveID6XNNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "ID6XN", NeedTest + "");
        }

        public void SaveAUDINNeedTest(bool NeedTest) {
            XMLUtil.UpdateAttrToXml(@"Config\SysConfig.xml", "NeedTest", "AUDIN", NeedTest + "");
        }
    }
}
