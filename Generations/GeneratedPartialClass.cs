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
    public partial class CodedWorkflow : CodedWorkflowBase
    {
        public IIpCheckService IpCheckService { get => serviceContainer.Resolve<IIpCheckService>(); }

        protected override void RegisterServices(ICodedWorkflowsServiceLocator serviceLocator)
        {
            serviceLocator.RegisterType<IIpCheckService, IpCheckService>();
        }
    }
}