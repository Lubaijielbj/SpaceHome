using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SpaceHome.BLLContainer
{
    public class UnityIOC
    {
        public static T Resolve<T>()
        {
            //读取Unity.config配置文件
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Unity.config");

            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");

            //创建仓库，映射对象
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration(section, "UserContainer");
            //section.Configure(container, "UserContainer");

            return container.Resolve<T>();
        }
    }
}
