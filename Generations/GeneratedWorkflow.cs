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
    public class IpCheckWorkflow : CodedWorkflow
    {
        [Workflow]
        public (string IpFromIpify, string IpFromIpecho, string IpFromIpinfo) Execute()
        {
            string ipFromIpify = IpCheckService.GetExternalIpFromIpify();
            string ipFromIpecho = IpCheckService.GetExternalIpFromIpecho();
            string ipFromIpinfo = IpCheckService.GetExternalIpFromIpinfo();

            return (ipFromIpify, ipFromIpecho, ipFromIpinfo);
        }
    }
}