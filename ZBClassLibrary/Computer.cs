using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace ZbClassLibrary
{



    /// <summary>
    ///Computer 的摘要说明
    /// </summary>

    public class Computer
    {
        public string CpuID; //CPU的ID
        public int CpuCount; //CPU的个数
        public string[] CpuMHZ;//CPU频率  单位：hz
        public string MacAddress;//计算机的MAC地址
        public string DiskID;//硬盘的ID
        public string DiskSize;//硬盘大小  单位：bytes
        public string IpAddress;//计算机的IP地址
        public string LoginUserName;//操作系统登录用户名
        public string ComputerName;//计算机名
        public string SystemType;//系统类型
        public string TotalPhysicalMemory; //总共的内存  单位：M 
        private static Computer _instance;
        public static Computer Instance()
        {
            if (_instance == null)
                _instance = new Computer();
            return _instance;
        }
        public Computer()
        {
            CpuID = GetCpuID();
            CpuCount = GetCpuCount();
            CpuMHZ = GetCpuMHZ();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            DiskSize = GetSizeOfDisk();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = " ";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        public static int GetCpuCount()
        {
            try
            {
                using (ManagementClass mCpu = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection cpus = mCpu.GetInstances();
                    return cpus.Count;
                }
            }
            catch
            {
            }
            return -1;
        }
        public static string[] GetCpuMHZ()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection cpus = mc.GetInstances();

            string[] mHz = new string[cpus.Count];
            int c = 0;
            ManagementObjectSearcher mySearch = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject mo in mySearch.Get())
            {
                mHz[c] = mo.Properties["CurrentClockSpeed"].Value.ToString();
                c++;
            }
            mc.Dispose();
            mySearch.Dispose();
            return mHz;
        }
        public static string GetSizeOfDisk()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moj = mc.GetInstances();
            foreach (ManagementObject m in moj)
            {
                return m.Properties["Size"].Value.ToString();
            }
            return "-1";
        }
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = " ";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        string GetIPAddress()
        {
            try
            {
                //获取IP地址 
                string st = " ";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo[ "IpAddress "].ToString(); 
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }

        }
        string GetDiskID()
        {
            try
            {
                //获取硬盘ID 
                String HDid = " ";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }

        }
        ///    <summary>  
        ///   操作系统的登录用户名 
        ///    </summary>  
        ///    <returns>  </returns>  
        string GetUserName()
        {
            try
            {
                string st = " ";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["UserName"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }

        }
        string GetSystemType()
        {
            try
            {
                string st = " ";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }

        }
        string GetTotalPhysicalMemory()
        {
            try
            {

                string st = " ";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }
        }
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow ";
            }
            finally
            {
            }
        }
    }

}