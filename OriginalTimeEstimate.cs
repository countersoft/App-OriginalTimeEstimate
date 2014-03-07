using System;
using System.Collections.Generic;
using Countersoft.Gemini.Commons.Entity;
using Countersoft.Gemini.Extensibility.Events;
using Countersoft.Gemini.Commons.Dto;
using System.Diagnostics;
using Countersoft.Gemini.Extensibility.Apps;

namespace OriginalTimeEstimate
{
    public class AppConstants
    {
        // Unique to your app
        public const string AppId = "6E0C7294-6494-4715-9EB1-B0B7C296E426";
    }

    [AppType(AppTypeEnum.Event),
    AppGuid(AppConstants.AppId),
    AppName("Original Time Estimate"),
    AppDescription("Saves the original estimate")]
    public class OriginalTimeEstimate : AbstractIssueListener
    {
        public override IssueDto BeforeCreateFull(IssueDtoEventArgs args)
        {
            return updateCustomField(args);
        }

        public override IssueDto BeforeUpdateFull(IssueDtoEventArgs args)
        {
            return updateCustomField(args);
        }

        public IssueDto updateCustomField(IssueDtoEventArgs args)
        {
            var CustomFieldName = "Original Estimate";
            
            var customFieldData = args.Issue.CustomFields[CustomFieldName];
            
            if (customFieldData != null && customFieldData.Entity.Data == string.Empty && (args.Issue.EstimatedHours > 0 || args.Issue.EstimatedMinutes > 0))
            {
                customFieldData.Entity.Data = string.Format("{0}h {1}m", args.Issue.EstimatedHours, args.Issue.EstimatedMinutes);
            }

            return args.Issue;
        }

    }
}

