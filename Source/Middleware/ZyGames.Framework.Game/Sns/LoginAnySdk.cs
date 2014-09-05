﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceModel.Extensions;
using ServiceStack.Text;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Configuration;
using ZyGames.Framework.Game.Context;

namespace ZyGames.Framework.Game.Sns
{
    /// <summary>
    /// AnySDK
    /// </summary>
    public class LoginAnySdk : AbstractLogin
    {
        private string _retailID = string.Empty;
        private string _pid = string.Empty;
        private string _retailUser = string.Empty;
        private string AppId = string.Empty;
        private string AppKey = string.Empty;
        private string _code = string.Empty;
        private string Url = string.Empty;
        private string _aceessTokenUrl = string.Empty;
        private string _appSecret = string.Empty;
        private string _token = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZyGames.Framework.Game.Sns.LoginTencent"/> class.
        /// </summary>
        public LoginAnySdk(string retailID, string retailUser, string token, string pid) 
        {
            this._retailID = pid;
            this._retailUser = retailUser;
            this._token = token;
            GameChannel gameChannel = ZyGameBaseConfigManager.GameSetting.GetChannelSetting(ChannelType.channelAnySDK);
            if (gameChannel != null)
            {
                Url = gameChannel.Url;
            }
        }
        public override string GetRegPassport()
        {
            return this.PassportID;
        }

        public override bool CheckLogin()
        {
            try
            {
                if (!string.IsNullOrEmpty(_retailUser))
                {
                    string[] arr = SnsManager.LoginByRetail(_retailID, _retailUser);
                    this.UserID = arr[0];
                    this.PassportID = arr[1];
                    QihooUserID = _retailUser;
                    SessionID = GetSessionId();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                new BaseLog().SaveLog(e);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class AnySDKResult
        {
            /// <summary>
            /// 用于表达验证请求成功与否
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 保存渠道平台返回的验证信息
            /// </summary>
            public AnySDKResultdata data { get; set; }
            /// <summary>
            /// 包含渠道编号、用户SDK标识，渠道用户uid
            /// </summary>
            public string common { get; set; }
            /// <summary>
            /// 为空，开发商服务器可以在ext域中存放用户信息（比如开发商服务器内部设定用户标识）下发给客户端
            /// </summary>
            public string ext { get; set; }

        }

        /// <summary>
        /// 
        /// </summary>
        public class AnySDKResultdata
        {
            /// <summary>
            /// 错误编号
            /// </summary>
            public string error_no { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string error { get; set; }
            /// <summary>
            /// 编号
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// avatar
            /// </summary>
            public string avatar { get; set; }
            /// <summary>
            /// sex
            /// </summary>
            public string sex { get; set; }
            /// <summary>
            /// area
            /// </summary>
            public string area { get; set; }
            /// <summary>
            /// nick
            /// </summary>
            public string nick { get; set; }
            /// <summary>
            /// access_token
            /// </summary>
            public string access_token { get; set; }
            /// <summary>
            /// refresh_token
            /// </summary>
            public string refresh_token { get; set; }
        }

    }
}
