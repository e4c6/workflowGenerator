using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace Coded_Service_Builder_GPT.Generations
{
    public class IpCheckService : IIpCheckService
    {
        public string GetExternalIpFromIpify()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://api.ipify.org").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetExternalIpFromIpecho()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://ipecho.net/plain").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetExternalIpFromIpinfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://ipinfo.io/ip").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}