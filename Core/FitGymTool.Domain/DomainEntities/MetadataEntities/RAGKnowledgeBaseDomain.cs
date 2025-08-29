using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitGymTool.Domain.DomainEntities.MetadataEntities;

/// <summary>
/// The RAG knowledge base domain model.
/// </summary>
[BsonIgnoreExtraElements]
public class RAGKnowledgeBaseDomain
{
    /// <summary>
    /// The Id.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application overview.
    /// </summary>
    /// <value>
    /// The application overview.
    /// </value>
    [BsonElement("application_overview")]
    public ApplicationOverview ApplicationOverview { get; set; } = new();

    /// <summary>
    /// Gets or sets the main navigation.
    /// </summary>
    /// <value>
    /// The main navigation.
    /// </value>
    [BsonElement("main_navigation")]
    public MainNavigation MainNavigation { get; set; } = new();

    /// <summary>
    /// Gets or sets the login process.
    /// </summary>
    /// <value>
    /// The login process.
    /// </value>
    [BsonElement("login_process")]
    public LoginProcess LoginProcess { get; set; } = new();

    /// <summary>
    /// Gets or sets the dashboard functionality.
    /// </summary>
    /// <value>
    /// The dashboard functionality.
    /// </value>
    [BsonElement("dashboard_functionality")]
    public DashboardFunctionality DashboardFunctionality { get; set; } = new();

    /// <summary>
    /// Gets or sets the member management.
    /// </summary>
    /// <value>
    /// The member management.
    /// </value>
    [BsonElement("member_management")]
    public MemberManagement MemberManagement { get; set; } = new();

    /// <summary>
    /// Gets or sets the fees management.
    /// </summary>
    /// <value>
    /// The fees management.
    /// </value>
    [BsonElement("fees_management")]
    public FeesManagement FeesManagement { get; set; } = new();

    /// <summary>
    /// Gets or sets the AI features.
    /// </summary>
    /// <value>
    /// The AI features.
    /// </value>
    [BsonElement("ai_features")]
    public AIFeaturesDomain AIFeatures { get; set; } = new();

    /// <summary>
    /// Gets or sets the bug reporting.
    /// </summary>
    /// <value>
    /// The bug reporting.
    /// </value>
    [BsonElement("bug_reporting")]
    public BugReporting BugReporting { get; set; } = new();

    /// <summary>
    /// Gets or sets the user interface elements.
    /// </summary>
    /// <value>
    /// The user interface elements.
    /// </value>
    [BsonElement("user_interface_elements")]
    public UserInterfaceElements UserInterfaceElements { get; set; } = new();

    /// <summary>
    /// Gets or sets the common user tasks.
    /// </summary>
    /// <value>
    /// The common user tasks.
    /// </value>
    [BsonElement("common_user_tasks")]
    public CommonUserTasks CommonUserTasks { get; set; } = new();

    /// <summary>
    /// Gets or sets the troubleshooting.
    /// </summary>
    /// <value>
    /// The troubleshooting.
    /// </value>
    [BsonElement("troubleshooting")]
    public Troubleshooting Troubleshooting { get; set; } = new();

    /// <summary>
    /// Gets or sets the best practices.
    /// </summary>
    /// <value>
    /// The best practices.
    /// </value>
    [BsonElement("best_practices")]
    public BestPractices BestPractices { get; set; } = new();
}

/// <summary>
/// The application overview domain.
/// </summary>
[BsonIgnoreExtraElements]
public class ApplicationOverview
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the purpose.
    /// </summary>
    /// <value>
    /// The purpose.
    /// </value>
    [BsonElement("purpose")]
    public string Purpose { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user types.
    /// </summary>
    /// <value>
    /// The user types.
    /// </value>
    [BsonElement("user_types")]
    public List<string> UserTypes { get; set; } = [];

    /// <summary>
    /// Gets or sets the access method.
    /// </summary>
    /// <value>
    /// The access method.
    /// </value>
    [BsonElement("access_method")]
    public string AccessMethod { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the authentication.
    /// </summary>
    /// <value>
    /// The authentication.
    /// </value>
    [BsonElement("authentication")]
    public string Authentication { get; set; } = string.Empty;
}

/// <summary>
/// The main navigation domain.
/// </summary>
[BsonIgnoreExtraElements]
public class MainNavigation
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the menu items.
    /// </summary>
    /// <value>
    /// The menu items.
    /// </value>
    [BsonElement("menu_items")]
    public List<MenuItem> MenuItems { get; set; } = [];
}

/// <summary>
/// The menu item domain.
/// </summary>
[BsonIgnoreExtraElements]
public class MenuItem
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the route.
    /// </summary>
    /// <value>
    /// The route.
    /// </value>
    [BsonElement("route")]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    /// <value>
    /// The icon.
    /// </value>
    [BsonElement("icon")]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access.
    /// </summary>
    /// <value>
    /// The access.
    /// </value>
    [BsonElement("access")]
    public string Access { get; set; } = string.Empty;
}

/// <summary>
/// The login process domain.
/// </summary>
[BsonIgnoreExtraElements]
public class LoginProcess
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the steps.
    /// </summary>
    /// <value>
    /// The steps.
    /// </value>
    [BsonElement("steps")]
    public List<LoginStep> Steps { get; set; } = [];

    /// <summary>
    /// Gets or sets the requirements.
    /// </summary>
    /// <value>
    /// The requirements.
    /// </value>
    [BsonElement("requirements")]
    public List<string> Requirements { get; set; } = [];
}

/// <summary>
/// The login step domain.
/// </summary>
[BsonIgnoreExtraElements]
public class LoginStep
{
    /// <summary>
    /// Gets or sets the step.
    /// </summary>
    /// <value>
    /// The step.
    /// </value>
    [BsonElement("step")]
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the action.
    /// </summary>
    /// <value>
    /// The action.
    /// </value>
    [BsonElement("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// The dashboard functionality domain.
/// </summary>
[BsonIgnoreExtraElements]
public class DashboardFunctionality
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the components.
    /// </summary>
    /// <value>
    /// The components.
    /// </value>
    [BsonElement("components")]
    public List<DashboardComponent> Components { get; set; } = [];
}

/// <summary>
/// The dashboard component domain.
/// </summary>
[BsonIgnoreExtraElements]
public class DashboardComponent
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the displays.
    /// </summary>
    /// <value>
    /// The displays.
    /// </value>
    [BsonElement("displays")]
    public List<string> Displays { get; set; } = [];

    /// <summary>
    /// Gets or sets the user actions.
    /// </summary>
    /// <value>
    /// The user actions.
    /// </value>
    [BsonElement("user_actions")]
    public List<string> UserActions { get; set; } = [];
}

/// <summary>
/// The member management domain.
/// </summary>
[BsonIgnoreExtraElements]
public class MemberManagement
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the main features.
    /// </summary>
    /// <value>
    /// The main features.
    /// </value>
    [BsonElement("main_features")]
    public List<MemberFeature> MainFeatures { get; set; } = [];
}

/// <summary>
/// The member feature domain.
/// </summary>
[BsonIgnoreExtraElements]
public class MemberFeature
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets how to access.
    /// </summary>
    /// <value>
    /// How to access.
    /// </value>
    [BsonElement("how_to_access")]
    public string HowToAccess { get; set; } = string.Empty;



    /// <summary>
    /// Gets or sets the validation rules.
    /// </summary>
    /// <value>
    /// The validation rules.
    /// </value>
    [BsonElement("validation_rules")]
    public List<string> ValidationRules { get; set; } = [];

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    /// <value>
    /// The success message.
    /// </value>
    [BsonElement("success_message")]
    public string SuccessMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the features.
    /// </summary>
    /// <value>
    /// The features.
    /// </value>
    [BsonElement("features")]
    public List<string> Features { get; set; } = [];

    /// <summary>
    /// Gets or sets the displayed information.
    /// </summary>
    /// <value>
    /// The displayed information.
    /// </value>
    [BsonElement("displayed_information")]
    public List<string> DisplayedInformation { get; set; } = [];

    /// <summary>
    /// Gets or sets the editable fields.
    /// </summary>
    /// <value>
    /// The editable fields.
    /// </value>
    [BsonElement("editable_fields")]
    public List<string> EditableFields { get; set; } = [];

    /// <summary>
    /// Gets or sets the non-editable fields.
    /// </summary>
    /// <value>
    /// The non-editable fields.
    /// </value>
    [BsonElement("non_editable_fields")]
    public List<string> NonEditableFields { get; set; } = [];

    /// <summary>
    /// Gets or sets the status options.
    /// </summary>
    /// <value>
    /// The status options.
    /// </value>
    [BsonElement("status_options")]
    public List<string> StatusOptions { get; set; } = [];
}

/// <summary>
/// The member step domain.
/// </summary>
[BsonIgnoreExtraElements]
public class MemberStep
{
    /// <summary>
    /// Gets or sets the step.
    /// </summary>
    /// <value>
    /// The step.
    /// </value>
    [BsonElement("step")]
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the fields.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    [BsonElement("fields")]
    public List<string> Fields { get; set; } = [];

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// The fees management domain.
/// </summary>
[BsonIgnoreExtraElements]
public class FeesManagement
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the main features.
    /// </summary>
    /// <value>
    /// The main features.
    /// </value>
    [BsonElement("main_features")]
    public List<FeesFeature> MainFeatures { get; set; } = [];
}

/// <summary>
/// The fees feature domain.
/// </summary>
[BsonIgnoreExtraElements]
public class FeesFeature
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets how to access.
    /// </summary>
    /// <value>
    /// How to access.
    /// </value>
    [BsonElement("how_to_access")]
    public string HowToAccess { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the displays.
    /// </summary>
    /// <value>
    /// The displays.
    /// </value>
    [BsonElement("displays")]
    public List<string> Displays { get; set; } = [];

    /// <summary>
    /// Gets or sets the user actions.
    /// </summary>
    /// <value>
    /// The user actions.
    /// </value>
    [BsonElement("user_actions")]
    public List<string> UserActions { get; set; } = [];

    /// <summary>
    /// Gets or sets the benefits.
    /// </summary>
    /// <value>
    /// The benefits.
    /// </value>
    [BsonElement("benefits")]
    public List<string> Benefits { get; set; } = [];

    /// <summary>
    /// Gets or sets the status indicators.
    /// </summary>
    /// <value>
    /// The status indicators.
    /// </value>
    [BsonElement("status_indicators")]
    public List<string> StatusIndicators { get; set; } = [];



    /// <summary>
    /// Gets or sets the required information.
    /// </summary>
    /// <value>
    /// The required information.
    /// </value>
    [BsonElement("required_information")]
    public List<string> RequiredInformation { get; set; } = [];

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    /// <value>
    /// The success message.
    /// </value>
    [BsonElement("success_message")]
    public string SuccessMessage { get; set; } = string.Empty;
}

/// <summary>
/// The fees step domain.
/// </summary>
[BsonIgnoreExtraElements]
public class FeesStep
{
    /// <summary>
    /// Gets or sets the step.
    /// </summary>
    /// <value>
    /// The step.
    /// </value>
    [BsonElement("step")]
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// The AI features domain.
/// </summary>
[BsonIgnoreExtraElements]
public class AIFeaturesDomain
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the main features.
    /// </summary>
    /// <value>
    /// The main features.
    /// </value>
    [BsonElement("main_features")]
    public List<AIFeatureDomain> MainFeatures { get; set; } = [];
}

/// <summary>
/// The AI feature domain.
/// </summary>
[BsonIgnoreExtraElements]
public class AIFeatureDomain
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets how to access.
    /// </summary>
    /// <value>
    /// How to access.
    /// </value>
    [BsonElement("how_to_access")]
    public string HowToAccess { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the capabilities.
    /// </summary>
    /// <value>
    /// The capabilities.
    /// </value>
    [BsonElement("capabilities")]
    public List<string> Capabilities { get; set; } = [];

    /// <summary>
    /// Gets or sets the features.
    /// </summary>
    /// <value>
    /// The features.
    /// </value>
    [BsonElement("features")]
    public List<string> Features { get; set; } = [];

    /// <summary>
    /// Gets or sets the sample queries.
    /// </summary>
    /// <value>
    /// The sample queries.
    /// </value>
    [BsonElement("sample_queries")]
    public List<string> SampleQueries { get; set; } = [];

    /// <summary>
    /// Gets or sets the user interactions.
    /// </summary>
    /// <value>
    /// The user interactions.
    /// </value>
    [BsonElement("user_interactions")]
    public List<string> UserInteractions { get; set; } = [];

    /// <summary>
    /// Gets or sets how it works.
    /// </summary>
    /// <value>
    /// How it works.
    /// </value>
    [BsonElement("how_it_works")]
    public List<string> HowItWorks { get; set; } = [];

    /// <summary>
    /// Gets or sets the displays.
    /// </summary>
    /// <value>
    /// The displays.
    /// </value>
    [BsonElement("displays")]
    public List<string> Displays { get; set; } = [];
}

/// <summary>
/// The bug reporting domain.
/// </summary>
[BsonIgnoreExtraElements]
public class BugReporting
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets how to access.
    /// </summary>
    /// <value>
    /// How to access.
    /// </value>
    [BsonElement("how_to_access")]
    public string HowToAccess { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the process.
    /// </summary>
    /// <value>
    /// The process.
    /// </value>
    [BsonElement("process")]
    public List<BugReportStep> Process { get; set; } = [];

    /// <summary>
    /// Gets or sets the severity levels.
    /// </summary>
    /// <value>
    /// The severity levels.
    /// </value>
    [BsonElement("severity_levels")]
    public List<string> SeverityLevels { get; set; } = [];

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    /// <value>
    /// The success message.
    /// </value>
    [BsonElement("success_message")]
    public string SuccessMessage { get; set; } = string.Empty;
}

/// <summary>
/// The bug report step domain.
/// </summary>
[BsonIgnoreExtraElements]
public class BugReportStep
{
    /// <summary>
    /// Gets or sets the step.
    /// </summary>
    /// <value>
    /// The step.
    /// </value>
    [BsonElement("step")]
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the fields.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    [BsonElement("fields")]
    public List<string> Fields { get; set; } = [];
}

/// <summary>
/// The user interface elements domain.
/// </summary>
[BsonIgnoreExtraElements]
public class UserInterfaceElements
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the elements.
    /// </summary>
    /// <value>
    /// The elements.
    /// </value>
    [BsonElement("elements")]
    public List<UIElement> Elements { get; set; } = [];
}

/// <summary>
/// The UI element domain.
/// </summary>
[BsonIgnoreExtraElements]
public class UIElement
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the features.
    /// </summary>
    /// <value>
    /// The features.
    /// </value>
    [BsonElement("features")]
    public List<string> Features { get; set; } = [];

    /// <summary>
    /// Gets or sets the types.
    /// </summary>
    /// <value>
    /// The types.
    /// </value>
    [BsonElement("types")]
    public List<string> Types { get; set; } = [];
}

/// <summary>
/// The common user tasks domain.
/// </summary>
[BsonIgnoreExtraElements]
public class CommonUserTasks
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tasks.
    /// </summary>
    /// <value>
    /// The tasks.
    /// </value>
    [BsonElement("tasks")]
    public List<UserTask> Tasks { get; set; } = [];
}

/// <summary>
/// The user task domain.
/// </summary>
[BsonIgnoreExtraElements]
public class UserTask
{
    /// <summary>
    /// Gets or sets the task.
    /// </summary>
    /// <value>
    /// The task.
    /// </value>
    [BsonElement("task")]
    public string Task { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the frequency.
    /// </summary>
    /// <value>
    /// The frequency.
    /// </value>
    [BsonElement("frequency")]
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the steps.
    /// </summary>
    /// <value>
    /// The steps.
    /// </value>
    [BsonElement("steps")]
    public List<string> Steps { get; set; } = [];

    /// <summary>
    /// Gets or sets the tips.
    /// </summary>
    /// <value>
    /// The tips.
    /// </value>
    [BsonElement("tips")]
    public List<string> Tips { get; set; } = [];

    /// <summary>
    /// Gets or sets the insights.
    /// </summary>
    /// <value>
    /// The insights.
    /// </value>
    [BsonElement("insights")]
    public List<string> Insights { get; set; } = [];

    /// <summary>
    /// Gets or sets the limitations.
    /// </summary>
    /// <value>
    /// The limitations.
    /// </value>
    [BsonElement("limitations")]
    public List<string> Limitations { get; set; } = [];

    /// <summary>
    /// Gets or sets the example queries.
    /// </summary>
    /// <value>
    /// The example queries.
    /// </value>
    [BsonElement("example_queries")]
    public List<string> ExampleQueries { get; set; } = [];
}

/// <summary>
/// The troubleshooting domain.
/// </summary>
[BsonIgnoreExtraElements]
public class Troubleshooting
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issues.
    /// </summary>
    /// <value>
    /// The issues.
    /// </value>
    [BsonElement("issues")]
    public List<TroubleshootingIssue> Issues { get; set; } = [];
}

/// <summary>
/// The troubleshooting issue domain.
/// </summary>
[BsonIgnoreExtraElements]
public class TroubleshootingIssue
{
    /// <summary>
    /// Gets or sets the problem.
    /// </summary>
    /// <value>
    /// The problem.
    /// </value>
    [BsonElement("problem")]
    public string Problem { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the possible causes.
    /// </summary>
    /// <value>
    /// The possible causes.
    /// </value>
    [BsonElement("possible_causes")]
    public List<string> PossibleCauses { get; set; } = [];

    /// <summary>
    /// Gets or sets the solutions.
    /// </summary>
    /// <value>
    /// The solutions.
    /// </value>
    [BsonElement("solutions")]
    public List<string> Solutions { get; set; } = [];

    /// <summary>
    /// Gets or sets the cause.
    /// </summary>
    /// <value>
    /// The cause.
    /// </value>
    [BsonElement("cause")]
    public string Cause { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the solution.
    /// </summary>
    /// <value>
    /// The solution.
    /// </value>
    [BsonElement("solution")]
    public string Solution { get; set; } = string.Empty;
}

/// <summary>
/// The best practices domain.
/// </summary>
[BsonIgnoreExtraElements]
public class BestPractices
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the practices.
    /// </summary>
    /// <value>
    /// The practices.
    /// </value>
    [BsonElement("practices")]
    public List<PracticeCategory> Practices { get; set; } = [];
}

/// <summary>
/// The practice category domain.
/// </summary>
[BsonIgnoreExtraElements]
public class PracticeCategory
{
    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    /// <value>
    /// The category.
    /// </value>
    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendations.
    /// </summary>
    /// <value>
    /// The recommendations.
    /// </value>
    [BsonElement("recommendations")]
    public List<string> Recommendations { get; set; } = [];
}
