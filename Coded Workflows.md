// Sources:
// https://docs.uipath.com/studio/standalone/2023.10/user-guide/coded-workflow
// https://docs.uipath.com/studio/standalone/2023.10/user-guide/registering-custom-services
// https://docs.uipath.com/studio/standalone/2023.10/user-guide/working-with-input-and-output-parameters

# Coded Workflows

# **Coded Workflow**

Coded workflows are the same as low-code workflows, the only difference being that you build them using separate interfaces:

- **Workflows** have a visual design interface.
- **Coded workflows** have a code-based interface.

Additionally, you can integrate coded workflows with low-code activities and workflows, and use a hybrid automation approach. This enables you to combine the benefits of code-based automation with the visual design of low-code components.

# **Structure**

Coded automations feature a structured design with namespaces, helper classes, and entry point methods. The framework of coded automations allows you to write the automations using the C# programming language.

Follow the detailed structure of a coded automation as described in the following sections.

When you create a coded automation, a namespace is automatically generated using the name of the Studio project. For instance, if your Studio project is named "My project", the namespace for all coded automations will be "Myproject".

Additionally, if you create a coded automation inside a folder in your Studio project, then the namespace will be the name of the project and the name of the folder. For instance, if your Studio project is named "My project", and the folder is named "place", then the namespace will be "Myproject.place".

Both

**coded workflow**

and

**coded test case**

automations use the

```
CodedWorkflow
```

partial class from the

**UiPath.CodedWorkflows**

package. This class gives the automation access to necessary interfaces for services (equal to activity packages), based on the installed packages in your project.

**NOTE:**

The

**UiPath.CodedWorkflows**

package is automatically included when you import an activity package that supports coded automations, such as

**UiPath.System.Activities 23.10**

or higher.

# **`CodedWorkflow`**

Coded automations inherit the `CodedWorkflow` partial class, creating a relationship of type `CodedAutomationExample : CodedWorkflow`. This means that the `CodedAutomationExample` class inherits attributes, methods, and functionality from the `CodedWorkflow` class. Essentially, it can access and utilize the features defined in the `CodedWorkflow` class, which provides a foundation for the coded automation, making it easier to build upon and customize the automation's behavior.

The `CodedWorkflow` class is declared as a partial class, allowing you to extend its functionalities by defining the same partial `CodedWorkflow` class in a code source file. This way, you can add new fields and methods to further customize the behavior of your coded automations. You can use this approach to implement a **Before** and **After** interface, specifically for coded test cases.

Additionally, the `CodedWorkflow` partial class inherits the `CodedWorkflowBase` class.

[https://docs.uipath.com/api/binary/studio/2/278094/289667](https://docs.uipath.com/api/binary/studio/2/278094/289667)

# **`CodedWorkflowBase`**

The **`CodedWorkflowBase`** class holds the built-in functionalities that a coded automation inherits. This class contains methods and specific properties for managing workflow instances, runtime access, handling service containers, and configuring environment contexts. The `CodedWorkflowBase` class also offers another separate method for logging that you can customize yourself.

Check out the `CodedWorkflowBase` methods in the table below:

| Method | Description |
| --- | --- |
| serviceContainer(ICodedWorkflowServiceContainer) | Provides access to the dependency injection container that is specific to the current coded workflow. This container, known as the service container, allows you to retrieve instances of services that have been registered within it. |
| GetRunningJobInformation() | Retrieves information about the currently running job within the context of the coded workflow. The method accesses the RunningJobInformation property of the executorRuntime object, that holds information about job status, progress, parameters, and timestamps. |
| Log(string message, LogLevel level = LogLevel.Info, IDictionary<string, object> additionalLogFields = null) | Adds additional log fields to log messages with specified attributes. |
| RunWorkflow(string workflowFilePath, IDictionary<string, object> inputArguments = null, TimeSpan? timeout = null, bool isolated = false, InvokeTargetSession targetSession = InvokeTargetSession.Current) | Provides a structure to execute a workflow within the context of the given workflow runtime. It can set parameters, handle potential isolation, and initiate workflow execution. The returned task provides the results of the executed workflow, including its output and input/output arguments. |
| RunWorkflowAsync(string workflowFilePath, IDictionary<string, object> inputArguments = null, TimeSpan? timeout = null, bool isolated = false, InvokeTargetSession targetSession = InvokeTargetSession.Current) | Provides a structure to execute a workflow asynchronously within the context of the given workflow runtime. It can set parameters, handle potential isolation, and initiate workflow execution. The returned task provides the results of the executed workflow, including its output and input/output arguments. |
| DelayAsync(TimeSpan time) and DelayAsync(int delayMs) | Suspends execution for a specified period of time asynchronously. |
| Delay(TimeSpan time) and Delay(int delayMs) | Suspends execution for a specified period of time. |
| HttpClient BuildClient(string scope = "Orchestrator", bool force = true) | Builds an HTTP client with a specified scope and access token. |
| RegisterServices(ICodedWorkflowsServiceLocator serviceLocator) | Registers services (activity packages) to the coded workflow's service locator. You can override it when you want to inject custom services into the dependency injection container. Learn how to create and use custom services (coded activity packages) https://docs.uipath.com/studio/standalone/2023.10/user-guide/registering-custom-services. |

The entry point method for both coded workflows and coded test cases is named `Execute()` and is attributed as either `Workflow` or `TestCase`. You can change the name of the method, as long as you attribute it to either `Workflow` or `TestCase`.

You can only use one `Execute()` method (`[TestCase]` or `[Workflow]`) inside a file, that inherits the `Coded Workflow`class.

In this method, you can add input and/or output arguments, which are equivalent to **In**, **Out** or **In/Out** arguments in low-code automations. Go through the [Working with Input and Output arguments](https://docs.uipath.com/studio/standalone/2023.10/user-guide/working-with-input-and-output-parameters) tutorial to learn how to use arguments in coded automations.

This entry point method serves as the starting point for running the automations. This makes coded workflows and test cases easy to identify as entry points due to their `Execute()` method.

# **Registering custom services**

To enhance your coded automations, you can register custom services, equal to creating custom activity packages.

1. Create a code source file and define a public interface named as the service you wish to create.
    
    This interface should list the methods you intend to use with your custom service. Each method must be implemented in a separate class which inherits the same file where you're defining the interface.
    
    For this example, name the interface as `IMyService` and call the method inside it `DoMyMethod`.
    
    `{
        public interface IMyService
        {
            void DoMyMethod();
        }
    }`
    
2. Proceed to another code source file and implement the methods from the previously created interface.
    
    This class must inherit from the same code source file where the public interface was created.
    
    In this example, implement the `DoMyMethod` method to output the value of a variable.
    
    `public class MyService : IMyService
        {
            public void DoMyMethod()
            {
                var a = "hello world";
                Console.WriteLine(a);
            }
        }
    }`
    
3. Build a partial class to wrap your custom service, making it accessible from any coded workflow that inherits this class.The code snippet below shows an example of how the public partial class was implemented:
    1. Create another code source file and declare the public partial class named `Coded Workflow`. Make this class inherit the `CodedWorkflowBase` class.
    2. Create a get-only property for the custom service you created, which allows access to an instance of the service interface.
        
        The instance is obtained through dependency injection using the `serviceContainer.Resolve<IMyService>()` method. This provides a way to interact with the custom service within the partial class.
        
        `public IMyService myService { get => serviceContainer.Resolve<IMyService>()};`
        
    3. Expand your class further by adding the `RegisterServices` method.
        
        By invoking this method, you register your custom service within the UiPath service container.
        
        `protected override void RegisterServices(ICodedWorkflowsServiceLocator serviceLocator)
        		{
        			serviceLocator.RegisterType<IMyService, MyService>();
        		}`
        
    
    `public partial class CodedWorkflow : CodedWorkflowBase
        {
    		public IMyService myService { get => serviceContainer.Resolve<IMyService>() ; }
    		
    		protected override void RegisterServices(ICodedWorkflowsServiceLocator serviceLocator)
    		{
    			serviceLocator.RegisterType<IMyService, MyService>();
    		}`
    
    # **Adding arguments to coded automations**
    
    When designing coded automations, you can add input, output, and In/Out arguments alongside the entry point `Execute` method. Check out the scenarios below to understand how to add arguments to your coded automations.
    
    ### Adding input parameters
    
    1. To add input parameters, define them after the name of the entry point method.
    2. For example, you have a coded automation that represents a loan application taking input arguments only. The needed input arguments are the `customerName`, `loanAmount`, `loanRate` and `loanTerm`. Check out the code snippet below:
        
        `public void Execute (string customerName, decimal loanAmount, double loanRate, int loanTerm)`
        
    
    ### Adding output parameters
    
    1. To add output parameters, define them before the name of the entry point method.**NOTE:** If the method returns a single parameter, it will automatically be named `Output` by default, and you won't need to declare the name separately.
    2. For example, you have a coded automation that approves or denies a loan application based on the loan rate. This automation requires an input argument for the loan rate (`loanRate`), and an output argument showing if the loan application was approved or denied (`loanApproved`). Check out the code snippet below:
        
        `public bool Execute (int loanRate)`
        
    3. Let's take another example that outputs two parameters. Suppose you have a coded automation that returns the approved loan amount (`LoanAmountApproved`) and if the loan was approved (`IsLoanApproved`), based on the loan rate (`LoanRate`). Check out the code snippet below:
        
        `public (int LoanAmountApproved, bool IsLoanApproved) Execute(int LoanRate)`
        
    
    ### Adding In/Out parameters
    
    1. To add an argument of type In/Out, define the same argument both before and after the name of the entry point method.
    2. For example, you have a coded automation that takes an initial loan amount (`loanAmount`) and an interest rate (`interestRate`) as input and then calculates the updated loan amount after applying the interest rate and returns it. Also, it returns an argument stating the type of financial need that this loan amount would require.Check out the code snippet below:
        
        `public (double loanAmount, string financialNeed) Execute (double interestRate, double loanAmount)`