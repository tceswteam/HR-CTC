
#region " Namespace "
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
#endregion

namespace HRCTCApp.DAL
{
    public class StoredProcedure
    {
        public const string DBName = "DBConn";

        //QAQC Form
        public const string spr_GetActivityListQAQC = "[Masters].[spr_GetActivityListQAQC]";
        public const string spr_GetReviewListQAQC = "[Masters].[spr_GetReviewListQAQC]";
        public const string spr_GetBU = "[Masters].[spr_GetBU]";
        public const string spr_GetGenerationList = "[Masters].[spr_GetGenerationList]";
        public const string spr_GetDocumentListQAQC = "[Masters].[spr_GetDocumentListQAQC]";
        public const string spr_GetDocumentListVendorGeneration = "[Masters].[spr_GetDocumentListVendorGeneration]";
        public const string spr_GetDeliverable = "[Masters].[spr_GetDeliverable]";
        //Insert for QAQC
        public const string spr_InsertQAQCDetail = "[dbo].[spr_InsertQAQCDetail]";
        public const string spr_InsertNewProjectDetails = "[dbo].[spr_InsertNewProjectDetails]";
        //Update for QAQC
        public const string spr_UpdateQAQCDetail = "[dbo].[spr_UpdateQAQCDetail]";
        //View for QAQC
        public const string spr_GetQAQCDetails = "[dbo].[spr_GetQAQCDetails]";
        public const string spr_GetQAQCDetailsByDocID = "[dbo].[spr_GetQAQCDetailsByDocID]";

        //Operational Support
        public const string spr_GetSupportTypeOperational = "[Masters].[spr_GetSupportTypeOperational]";
        public const string spr_GetDesginationList = "[Masters].[spr_GetDesginationList]";
        public const string spr_GetCurrency = "[Masters].[spr_GetCurrency]";
        //Insert for Operational support
        public const string spr_InsertOperationalSupportDetail = "[dbo].[spr_InsertOperationalSupportDetail]";
        // Upadte for Operational support
        public const string spr_UpdateOperationalSupportDetail = "[dbo].[spr_UpdateOperationalSupportDetail]";
        //Grid View For Operational support
        public const string spr_GetOperationalSupportDetails = "[dbo].[spr_GetOperationalSupportDetails]";
        //Form View 
        public const string spr_GetOperationalSupportDetailsByDocID = "[dbo].[spr_GetOperationalSupportDetailsByDocID]";
     

        //Value Addition And Innovation Form
        public const string spr_GetSupportListValueAddition = "[Masters].[spr_GetSupportListValueAddition]";
        public const string spr_GetTypeVIList = "[Masters].[spr_GetTypeVIList]";
        public const string spr_GetAcceptanceListValueAddition = "[Masters].[spr_GetAcceptanceListValueAddition]";
        public const string spr_GetCategoryList = "[Masters].[spr_GetCategoryList]";
        //Insert and Update 
        public const string spr_InsertVAIDetail = "[dbo].[spr_InsertVAIDetail]";
        //Grid View
        public const string spr_GetVAIDetails = "[dbo].[spr_GetVAIDetails]";
        //Form View 
        public const string spr_GetVAIDetailsByDocID = "[dbo].[spr_GetVAIDetailsByDocID]";
        //Binding Client Name 
        public const string spr_GetClientName = "[Masters].[spr_GetClientName]";

        //for Awards Recognition and Appreciation
        public const string spr_GetDocumentList = "[Masters].[spr_GetDocumentList]";
        // Grid View For Award Recognition and Appreciation
        public const string spr_GetAppreciationAwardDetails = "[dbo].[spr_GetAppreciationAwardDetails]";
        //Insert and Update
        public const string spr_InsertAwardAppreciationDetail = "[dbo].[spr_InsertAwardAppreciationDetail]";
        //Form View
        public const string spr_GetAppreciationAwardDetailsByDocID = "[dbo].[spr_GetAppreciationAwardDetailsByDocID]";
        public const string spr_GetTypeList = "[Masters].[spr_GetTypeList]";

        //for Resource Development
        public const string spr_GetResourceDevelopmentActivityList = "[Masters].[spr_GetResourceDevelopmentActivityList]";
        //Insert
        public const string spr_InsertResourceDevDetail= "[dbo].[spr_InsertResourceDevDetail]";
        //Form View
        public const string spr_GetResourceDevDetailsByDocID = "[dbo].[spr_GetResourceDevDetailsByDocID]";
        //Grid View
        public const string spr_GetResourceDevDetails = "[dbo].[spr_GetResourceDevDetails]";

        //for Technology Development
        public const string spr_GetTechnologyDevelopmentList = "[Masters].[spr_GetTechnologyDevelopmentList]";
        public const string spr_GetTechnologyDetailsByDocID = "[dbo].[spr_GetTechnologyDetailsByDocID]";
        public const string spr_GetTechnologyDetails = "[dbo].[spr_GetTechnologyDetails]";
        public const string spr_InsertTechnologyDevelopmentDetail = "[dbo].[spr_InsertTechnologyDevelopmentDetail]";
        public const string spr_GetTechnologyDevelopmentWorkList = "[Masters].[spr_GetTechnologyDevelopmentWorkList]";
        public const string spr_GetTechDevSupportList = "[Masters].[spr_GetTechDevSupportList]";

        //for Knowledge Management
        public const string spr_GetKMActivityList = "[Masters].[spr_GetKMActivityList]";
        public const string spr_GetKMDetailsByDocID = "[dbo].[spr_GetKMDetailsByDocID]";
        public const string spr_GetKMDetails = "[dbo].[spr_GetKMDetails]";
        public const string spr_InsertKMDetail = "[dbo].[spr_InsertKMDetail]";
        public const string spr_GetKMWorkList = "[Masters].[spr_GetKMWorkList]";

       
       //Help Desk Form
        public const string spr_InsertHelpDeskDetail = "dbo.spr_InsertHelpDeskDetail";
        public const string spr_GetHelpDeskDetailsByDocID = "dbo.spr_GetHelpDeskDetailsByDocID";
        public const string spr_GeHelpDeskViewDetails = "dbo.spr_GeHelpDeskViewDetails";
        
        //Delete For Grid View
        public const string spr_DeleteQAQCDetailsByDocID = "dbo.spr_DeleteQAQCDetailsByDocID";
        public const string spr_DeleteOSDetailsByDocID = "dbo.spr_DeleteOSDetailsByDocID";
        public const string spr_DeleteVAIDetailsByDocID = "dbo.spr_DeleteVAIDetailsByDocID";
        public const string spr_DeleteAwardAppreciationDetailsByDocID = "dbo.spr_DeleteAwardAppreciationDetailsByDocID";
        public const string spr_DeleteTDDetailsByDocID = "dbo.spr_DeleteTDDetailsByDocID";
        public const string spr_DeleteRDDetailsByDocID = "dbo.spr_DeleteRDDetailsByDocID";
        public const string spr_DeleteNAIBDetailsByDocID = "dbo.spr_DeleteNAIBDetailsByDocID";
        public const string spr_DeleteKMDetailsByDocID = "dbo.spr_DeleteKMDetailsByDocID";

        //Delete for Reports 
        public const string spr_DeleteReportByDocID = "dbo.spr_DeleteReportByDocID";
         
       
       
        public const string spr_GetLkpData = "[Masters].[spr_GetLkpData]";
        public const string spr_UserAcl_Validate = "[Masters].[spr_UserAcl_Validate]";
        public const string spr_UserACL_GetList = "[Masters].[spr_UserACL_GetList]";
        public const string spr_GetRoleList = "[Masters].[spr_GetRoleList]";
        public const string spr_UserACL_Save = "[Masters].[spr_UserACL_Save]";
        public const string spr_UserACL_Get = "[Masters].[spr_UserACL_Get]";



        public const string spr_Roles_GetList = "[Masters].[spr_Roles_GetList]";
        public const string spr_GetMultiLkpData = "[Masters].[spr_GetMultiLkpData]";

        public const string spr_UpdateAttachment = "[dbo].[spr_UpdateAttachment]";

        public const string spr_WF_GetApprover = "[Masters].[spr_WF_GetApprover]";
        public const string spr_WF_GetNextApprover = "[Masters].[spr_WF_GetNextApprover]";

        public const string spr_GetAttachmentByAttachID = "[Docs].[spr_GetAttachmentByAttachID]";

        public const string spr_Authenticate = "[dbo].[spr_User_AuthUser]";

        public const string spr_Common_List = "[Masters].[spr_Common_List]";

        public const string spr_Common_Get = "[Masters].[spr_Common_Get]";

        public const string spr_Common_Save = "[Masters].[spr_Common_Save]";

        public const string spr_GetEmpData = "[Masters].[spr_GetEmpData]";

        public const string spr_GetJobDtls = "[Masters].[spr_GetJobDtls]";

        public const string spr_GetCurrentFY = "[Masters].[spr_GetCurrentFY]";
        public const string spr_GetViewByStatus = "Docs.spr_GetViewByStatus";
        public const string spr_GetDoc = "WF.spr_GetDoc";
        public const string spr_GetDocACL = "WF.spr_GetDocACL";
        public const string spr_SaveDoc = "WF.spr_SaveDoc";
        public const string spr_ClearDocACL = "WF.spr_ClearDocACL";
        public const string spr_AddDocACL = "WF.spr_AddDocACL";
        public const string spr_SendMail = "WF.spr_SendMail";
        public const string spr_SaveCommonWFDoc = "WF.spr_SaveCommonWFDoc";
        public const string spr_IsActionByEntryExistsInACL = "WF.spr_IsActionByEntryExistsInACL";

        #region Mail

        public const string spr_GetMailTemplate = "[Mail].[spr_GetMailTemplate]";
        public const string spr_GetEmailID = "[Mail].[spr_GetEmailID]";
        public const string spr_GetEmpNameByEmailID = "[Mail].[spr_GetEmpNameByEmailID]";
        public const string spr_AddMailToOutbox = "[Mail].[spr_AddMailToOutbox]";
        public const string spr_GetEmpCodeListFromSP = "[Mail].[spr_GetEmpCodeListFromSP]";
        public const string spr_MailTemplate_GetList = "[Mail].[spr_MailTemplate_GetList]";
        public const string spr_MailTemplate_Save = "[Mail].[spr_MailTemplate_Save]";
        public const string spr_MailTemplateValidate = "[Mail].[spr_MailTemplateValidate]";
        public const string spr_MailTemplate_Get = "[Mail].[spr_MailTemplate_Get]";
        public const string spr_GetURL = "[Mail].[spr_GetURL]";

        #endregion Mail

        public const string spr_Attachment = "[Docs].[spr_Attachment]";
        public const string spr_Attachment_Delete = "[Docs].[spr_Attachment_Delete]";
        public const string spr_GetAttachment = "[Docs].[spr_GetAttachment]";

        public const string spr_GetWFDoc = "[WF].[spr_GetWFDoc]";


        //NAIB
        public const string spr_InsertNAIBDetail = "[dbo].[spr_InsertNAIBDetail]";
        public const string spr_GetNAIBDetailsByDocID = "[dbo].[spr_GetNAIBDetailsByDocID]";
        public const string spr_GetNAIBDetails = "[dbo].[spr_GetNAIBDetails]";
        public const string spr_GetNAIBWorkPerformedList = "[Masters].[spr_GetNAIBWorkPerformedList]";
        public const string spr_GetNAIBActivityList = "[Masters].[spr_GetNAIBActivityList]";
        public const string spr_GetStatusList = "[Masters].[spr_GetStatusList]";
        public const string spr_GetJobNo = "[Masters].[spr_GetJobNo]";
        public const string spr_GetNAIBListByPDocID = "[dbo].[spr_GetNAIBListByPDocID]";
        public const string spr_DeleteNAIBList = "[dbo].[spr_DeleteNAIBList]";
        public const string spr_InsertNAIBList = "[dbo].[spr_InsertNAIBList]";

        //QAQCList
        public const string spr_InsertQAQCList = "[dbo].[spr_InsertQAQCList]";
        public const string spr_DeleteQAQCList = "[dbo].[spr_DeleteQAQCList]";
        public const string spr_GetQAQCListByPDocID = "[dbo].[spr_GetQAQCListByPDocID]";
        public const string spr_GetBUNameOnJobCode = "[dbo].[spr_GetBUNameOnJobCode]";

        //OD List
        public const string spr_InsertODList = "[dbo].[spr_InsertODList]";
        public const string spr_DeleteODList = "[dbo].[spr_DeleteODList]";
        public const string spr_GetODListByPDocID = "[dbo].[spr_GetODListByPDocID]";

        //VAI List
        public const string spr_InsertVAIList = "[dbo].[spr_InsertVAIList]";
        public const string spr_InsertVAIBenefitList = "[dbo].[spr_InsertVAIBenefitList]";
        public const string spr_DeleteVAIList = "[dbo].[spr_DeleteVAIList]";
        public const string spr_GetVAIistByPDocID = "[dbo].[spr_GetVAIListByPDocID]";
        public const string spr_GetVAIBenefitListByPDocID = "[dbo].[spr_GetVAIBenefitListByPDocID]";
        
        //RD List
        public const string spr_InsertRDList = "[dbo].[spr_InsertRDList]";
        public const string spr_DeleteRDList = "[dbo].[spr_DeleteRDList]";
        public const string spr_GetRDListByPDocID = "[dbo].[spr_GetRDListByPDocID]";

        //KM List
        public const string spr_InsertKMList = "[dbo].[spr_InsertKMList]";
        public const string spr_DeleteKMList = "[dbo].[spr_DeleteKMList]";
        public const string spr_GetKMListByPDocID = "[dbo].[spr_GetKMListByPDocID]";


        //TD List
        public const string spr_InsertTDList = "[dbo].[spr_InsertTDList]";
        public const string spr_DeleteTDList = "[dbo].[spr_DeleteTDList]";
        public const string spr_GetTDListByPDocID = "[dbo].[spr_GetTDListByPDocID]";

        public const string spr_WF_MARMember_GetNextAppr = "[Masters].[spr_WF_MARMember_GetNextAppr]";
        public const string spr_GetUserRoles = "[Masters].[spr_GetUserRoles]";
        public const string spr_MARMember_GetViewByStatus = "[dbo].[spr_MARMember_GetViewByStatus]";

        //DTCH MAR Member WorkFlow
        public const string spr_GetDTCHMemberDetails = "[dbo].[spr_GetDTCHMemberDetails]";
        public const string spr_GetMARMemberBUSpecificDetails = "[dbo].[spr_GetMARMemberBUSpecificDetails]";
        public const string spr_InsertMARMemDTCHDetails = "[dbo].[spr_InsertMARMemDTCHDetails]";
        public const string spr_InsertMARMemDTCHDetailsList = "[dbo].[spr_InsertMARMemDTCHDetailsList]";
        public const string spr_DeleteMARMemDTCHDetailsList = "[dbo].[spr_DeleteMARMemDTCHDetailsList]";
        public const string spr_GetDTCHMemberDetailsByDocID = "[dbo].[spr_GetDTCHMemberDetailsByDocID]";
        public const string spr_GetDTCHMemberDetailsListByPDocID = "[dbo].[spr_GetDTCHMemberDetailsListByPDocID]";
        public const string spr_DTCHMARMemberDetails_SetDocFlag = "[dbo].[spr_DTCHMARMemberDetails_SetDocFlag]";
        public const string spr_UpdateMarMemberApprovedFlag = "[dbo].[spr_UpdateMarMemberApprovedFlag]";
        public const string spr_UpdateMARMemDTCHDetailsList = "[dbo].[spr_UpdateMARMemDTCHDetailsList]";
        public const string spr_InsertMARMemBUSecificDetailsList = "[dbo].[spr_InsertMARMemBUSecificDetailsList]";
        public const string spr_DeleteMARMemBUSpecificDetailsList = "[dbo].[spr_DeleteMARMemBUSpecificDetailsList]";
        public const string spr_GetCheckListDetail = "[Masters].[spr_GetCheckListDetail]";
        public const string spr_GetMemberApprovedRequestByEmpCode = "[dbo].[spr_GetMemberApprovedRequestByEmpCode]";

        //BU MAR Member WorkFlow
        public const string spr_GetBULeadMemberDetails = "[dbo].[spr_GetBULeadMemberDetails]";
        public const string spr_InsertMARMemBULDetails = "[dbo].[spr_InsertMARMemBULDetails]";
        public const string spr_InsertMARMemBULDetailsList = "[dbo].[spr_InsertMARMemBULDetailsList]";
        public const string spr_DeleteMARMemBULDetailsList = "[dbo].[spr_DeleteMARMemBULDetailsList]";
        public const string spr_GetBULMemberDetailsByDocID = "[dbo].[spr_GetBULMemberDetailsByDocID]";
        public const string spr_GetBULMemberDetailsListByPDocID = "[dbo].[spr_GetBULMemberDetailsListByPDocID]";
        public const string spr_BULMARMemberDetails_SetDocFlag = "[dbo].[spr_BULMARMemberDetails_SetDocFlag]";
        //public const string spr_UpdateMarMemberApprovedFlag = "[dbo].[spr_UpdateMarMemberApprovedFlag]";
        public const string spr_UpdateMARMemBULDetailsList = "[dbo].[spr_UpdateMARMemBULDetailsList]";


        //DTCH WorkFlow
        public const string spr_GetDTCHDetails = "[dbo].[spr_GetDTCHDetails]";
        public const string spr_GetDTCHBUSpecificDetails = "dbo.spr_GetDTCHBUSpecificDetails";
        public const string spr_InsertDTCHDetails = "[dbo].[spr_InsertDTCHDetails]";
        public const string spr_InsertDTCHDetailsList = "[dbo].[spr_InsertDTCHDetailsList]";
        public const string spr_DeleteDTCHDetailsList = "[dbo].[spr_DeleteDTCHDetailsList]";
        public const string spr_GetDTCHDetailsByDocID = "[dbo].[spr_GetDTCHDetailsByDocID]";
        public const string spr_GetDTCHDetailsListByPDocID = "[dbo].[spr_GetDTCHDetailsListByPDocID]";
        public const string spr_DTCHDetails_SetDocFlag = "[dbo].[spr_DTCHDetails_SetDocFlag]";
        //public const string spr_UpdateMarMemberApprovedFlag = "[dbo].[spr_UpdateMarMemberApprovedFlag]";
        public const string spr_UpdateDTCHDetailsList = "[dbo].[spr_UpdateDTCHDetailsList]";

        //BUL Details WorkFlow
        public const string spr_GetBULeadDetails = "[dbo].[spr_GetBULeadDetails]";
        public const string spr_InsertBULeadDetails = "[dbo].[spr_InsertBULeadDetails]";
        public const string spr_InsertBULeadDetailsList = "[dbo].[spr_InsertBULeadDetailsList]";
        public const string spr_DeleteBULeadDetailsList = "[dbo].[spr_DeleteBULeadDetailsList]";
        public const string spr_GetBULeadDetailsByDocID = "[dbo].[spr_GetBULeadDetailsByDocID]";
        public const string spr_GetBULeadDetailsListByPDocID = "[dbo].[spr_GetBULeadDetailsListByPDocID]";
        public const string spr_BULeadDetails_SetDocFlag = "[dbo].[spr_BULeadDetails_SetDocFlag]";
        //public const string spr_UpdateMarMemberApprovedFlag = "[dbo].[spr_UpdateMarMemberApprovedFlag]";
        public const string spr_UpdateBULeadDetailsList = "[dbo].[spr_UpdateBULeadDetailsList]";
        public const string spr_InsertBUSecificDetailsList = "[dbo].[spr_InsertBUSecificDetailsList]";
        public const string spr_DeleteBUSpecificDetailsList = "[dbo].[spr_DeleteBUSpecificDetailsList]";

        //DyCTO Details WorkFlow
        public const string spr_GetDyCTODetails = "[dbo].[spr_GetDyCTODetails]";
        public const string spr_GetDyCTOBUSpecificDetails = "dbo.spr_GetDyCTOBUSpecificDetails";
        public const string spr_InsertDyCTODetails = "[dbo].[spr_InsertDyCTODetails]";
        public const string spr_InsertDyCTODetailsList = "[dbo].[spr_InsertDyCTODetailsList]";
        public const string spr_DeleteDyCTODetailsList = "[dbo].[spr_DeleteDyCTODetailsList]";
        public const string spr_GetDyCTODetailsByDocID = "[dbo].[spr_GetDyCTODetailsByDocID]";
        public const string spr_GetDyCTODetailsListByPDocID = "[dbo].[spr_GetDyCTODetailsListByPDocID]";
        public const string spr_DyCTODetails_SetDocFlag = "[dbo].[spr_DyCTODetails_SetDocFlag]";
        //public const string spr_UpdateMarMemberApprovedFlag = "[dbo].[spr_UpdateMarMemberApprovedFlag]";
        public const string spr_UpdateDyCTODetailsList = "[dbo].[spr_UpdateDyCTODetailsList]";
        public const string spr_InsertDyCTOBUSpecificDetailsList = "[dbo].[spr_InsertDyCTOBUSpecificDetailsList]";
        public const string spr_DeleteDyCTOBUSpecificDetailsList = "[dbo].[spr_DeleteDyCTOBUSpecificDetailsList]";

        public const string spr_GetReportingMonth = "[Masters].[spr_GetReportingMonth]";

        //Reports
        public const string spr_GetVAIReports = "[dbo].[spr_GetVAIReports]";
        public const string spr_GetReportsOfSugCNBUInterface = "[dbo].[spr_GetReportsOfSugCNBUInterface]";
        public const string spr_GetReportsOfAwards = "[dbo].[spr_GetReportsOfAwards]";
        public const string spr_GetReportsOfPatents = "[dbo].[spr_GetReportsOfPatents]";
        public const string spr_GetReportsOfPapers = "[dbo].[spr_GetReportsOfPapers]";
        public const string spr_GetReportsOfRepresentExternalOrg = "[dbo].[spr_GetReportsOfRepresentExternalOrg]";
        public const string spr_GetReportOnBusinessAcquisition = "[dbo].[spr_GetReportOnBusinessAcquisition]";
        public const string spr_GetReportingMonthForReports = "[Masters].[spr_GetReportingMonthForReports]";



        //Key Measure Master Deatils
        public const string spr_GetKeyMeasureList = "[Masters].[spr_GetKeyMeasureList]";
        public const string spr_GetKeyMeasureUnit = "[Masters].[spr_GetKeyMeasureUnit]";

        //CheckList 
        public const string spr_InsertCheckListDetails = "[dbo].[spr_InsertCheckListDetails]";
        public const string spr_DeleteCheckListDetails = "[dbo].[spr_DeleteCheckListDetails]";
        
        //validation of report
        public const string spr_ValidateReports = "[dbo].[spr_ValidateReports]";

        //DH Performance Report
        public const string spr_getDTCHComparativeAnalysis = "[dbo].[spr_getDTCHComparativeAnalysis]";
        public const string spr_getDTCHComparativeAnalysisByDept = "[dbo].[spr_getDTCHComparativeAnalysisByDept]";

        //check approval count
        public const string spr_CheckApprovalCount = "[dbo].[spr_CheckApprovalCount]";


        public const string spr_GetMyApprovedMARMemberBULeadDetails = "[dbo].[spr_GetMyApprovedMARMemberBULeadDetails]";
        public const string spr_GetApprovedMARBULeadDetailsByRepMonth = "[dbo].[spr_GetApprovedMARBULeadDetailsByRepMonth]";

        public const string spr_GetDashboardCount = "[dbo].[spr_GetDashboardCount]";

        //AddMemberDetails
        public const string spr_GetMasterDTCH = "[Masters].[spr_GetMasterDTCH]";
        public const string spr_InsertMemberDetails = "[dbo].[spr_InsertMemberDetails]";
        public const string spr_GetMemberDetailsByMemberCode = "[dbo].[spr_GetMemberDetailsByMemberCode]";
        public const string spr_ValidateMemberDetails = "[dbo].[spr_ValidateMemberDetails]";

        //AddBULDetails
        public const string spr_GetBULDetailsMaster = "[Masters].[spr_GetBULDetailsMaster]";
        public const string spr_UpdateBULDetails = "[dbo].[spr_UpdateBULDetails]";
        public const string spr_GetBULDetailsByBUCode = "[dbo].[spr_GetBULDetailsByBUCode]";

        //Pending Member Name
        public const string spr_GetActivityPendingMemberNameList = "[dbo].[spr_GetActivityPendingMemberNameList]";

        //Update VAI comments
        public const string spr_UpdateVAIComments = "[dbo].[spr_UpdateVAIComments]";
        
        //View
        public const string spr_View_GetQAQCList = "[dbo].[spr_View_GetQAQCList]";
        public const string spr_View_GetODList = "[dbo].[spr_View_GetODList]";
        public const string spr_View_GetVAIList = "[dbo].[spr_View_GetVAIList]";
        public const string spr_View_GetTDList = "[dbo].[spr_View_GetTDList]";
        public const string spr_View_GetRDList = "[dbo].[spr_View_GetRDList]";
        public const string spr_View_GetNAIBList = "[dbo].[spr_View_GetNAIBList]";
        public const string spr_View_GetKMList = "[dbo].[spr_View_GetKMList]";
        public const string spr_View_GetAppreciationAwardDetails = "[dbo].[spr_View_GetAppreciationAwardDetails]";

        public const string spr_GetMemberApprovedRequest = "[dbo].[spr_GetMemberApprovedRequest]";
        public const string spr_ValidateMemberReportGenerated = "[dbo].[spr_ValidateMemberReportGenerated]";

    }
}
