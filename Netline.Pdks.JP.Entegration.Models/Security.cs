﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Netline.Pdks.JP.Entegration.Models
{
    public class SecurityHeader : MessageHeader
    {
        private readonly UsernameToken _usernameToken;

        public SecurityHeader(string id, string username, string password)
        {
            _usernameToken = new UsernameToken(id, username, password);
        }

        public override string Name
        {
            get { return "Security"; }
        }

        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"; }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UsernameToken));
            serializer.Serialize(writer, _usernameToken);
        }
    }


    [XmlRoot(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
    public class UsernameToken
    {
        public UsernameToken()
        {
        }

        public UsernameToken(string id, string username, string password)
        {
            Id = "SecurityToken-2d2abf11-970d-4057-9fd8-17bda1ce0d55";
            Username = username;
            Password = new Password() { Value = password };
      
        }

        [XmlAttribute(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
        public string Id { get; set; }

        [XmlElement]
        public string Username { get; set; }

        [XmlElement]
        public Password Password { get; set; }

    
        
    }

    public class Password
    {
        public Password()
        {
            Type = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText";
        }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
  
}
