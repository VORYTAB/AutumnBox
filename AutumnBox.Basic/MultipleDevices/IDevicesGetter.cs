/* =============================================================================*\
*
* Filename: IDevicesGetter.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 02:45:52(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// �ƺ�ûʲô�ô��Ľӿ�,���������
    /// </summary>
    public interface IDevicesGetter
    {
        /// <summary>
        /// ��ȡ�豸
        /// </summary>
        /// <returns></returns>
        DevicesList GetDevices();
    }
}