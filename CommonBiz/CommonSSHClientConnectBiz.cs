using System;
using System.Collections.Generic;
using System.Text;
using biz.CommonBiz;
using Renci.SshNet;

namespace biz.CommonBiz
{
    public class CommonSSHClientConnectBiz : IDisposable
    {
        private static string _SshHost;
        private static int _SshPort;
        private static string _SshPassword;
        private static string _SshUser;
        private static string _ForwardBoundHost;
        private static UInt32 _ForwardBoundPort;
        private static string _ForwardHost;
        private static UInt32 _ForwardPort;
        private static bool _IsUse;
        private SshClient _Client = null;
        private ForwardedPortLocal _FwPort = null;

        public CommonSSHClientConnectBiz()
        {
            Init();
        }

        private void Init()
        {
            _SshHost = CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:sshHost").Value;
            _SshPort = Convert.ToInt32(CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:sshPort").Value);
            _SshPassword = CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:sshPassword").Value;
            _SshUser = CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:sshUser").Value;
            _ForwardBoundHost = CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:forwardBoundHost").Value;
            _ForwardBoundPort = Convert.ToUInt32(CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:forwardBoundPort").Value);
            _ForwardHost = CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:forwardHost").Value;
            _ForwardPort = Convert.ToUInt32(CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:forwardPort").Value);
            _IsUse = Convert.ToBoolean(CommonConfigurationBuilderBiz.Configuration.GetSection("sshConnectionString:IsUse").Value);

            if(_IsUse)
            {
                SshClientConnect();
                PortStart();
            }
        }

        /// <summary>
        /// sshClientConnect
        /// </summary>
        private void SshClientConnect()
        {
            _Client = new SshClient(_SshHost, _SshPort, _SshUser, _SshPassword);
            if (!_Client.IsConnected)
            {
                _Client.Connect();
            }
        }

        /// <summary>
        /// PortStart
        /// </summary>
        private void PortStart()
        {
            IEnumerable<ForwardedPort> portList = _Client.ForwardedPorts;

            _FwPort = new ForwardedPortLocal(_ForwardBoundHost, _ForwardBoundPort, _ForwardHost, _ForwardPort);

            _Client.AddForwardedPort(_FwPort);
            _FwPort.Start();


        }


        public void Dispose()
        {
            if (_IsUse)
            {
                //_FwPort.Stop();
                _Client.Disconnect();
                _Client.Dispose();
            }
        }
    }
}
