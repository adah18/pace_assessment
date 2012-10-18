using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.IO;
using System.Data.SqlClient;
using System.Web;

namespace PAOnlineAssessment.Classes
{
        
   public class GlobalDec
    {
        public static int UserGroupID, UserID;
        public static string Username, Password,
                      Lastname, Firstname, Middlename,
                      DateCreated, UserCreated,
                      LastUpdateDate, LastUpdateUser, Description, Status, DateToday;
        public static bool isActivated;        
    }

   public class CurrentUser
   {
       public static string _UID;
       public static string _UGroup, _Stat;
   }

   public class CurrentStudent
   {
       public int StudentID, LevelID, SectionID;
       public string StudentNumber, SchoolYear;
   }

   public class LoginUser
   {
       public int UserID;
       public string UserGroupID,  Username, Password, AccessRights,
                     Lastname, Firstname, DateCreated, UserCreated,
                     LastUpdateDate, LastUpdateUser, Description, Status, DateToday;
       
   }

   public class RetrieveUser
   {
       public string Username, Password, Firstname, Lastname, EmailAddress;
   }

   public class GlobalForms
   {
       public string frm_questionpool_maintenance_main,
                     frm_questionpool_maintenance_upload,
                     frm_admin_dashboard,
                     frm_pending_student_accounts,
                     frm_student_maintenance_main,
                     frm_student_maintenance_addupdate,
                     frm_user_maintenance_main,
                     frm_activate_email,
                     frm_signup,
                     frm_default_dashboard,
                     frm_index,
                     frm_forgot_password,
                     frm_login,
                     frm_logout,
                     frm_instructor_dashboard,
                     frm_questionpool_maintenance_manual,
                     frm_assessmenttype_maintenance_main,
                     frm_assessmenttype_maintenance_addupdate,
                     frm_questionpool_maintenance_update,
                     frm_student_dashboard,
                     frm_change_password,
                     frm_available_assessments,
                     frm_quizcreation_main,
                     frm_quizcreation_addupdate,
                     frm_quizcreation_update,
                     frm_instructor_subjects,
                     frm_take_assessment,
                     frm_preview_assessment,
                     frm_review_assessment,
                     frm_review_assessment_list,
                     frm_assessment_success,
                     frm_history_assessment_main,
                     frm_history_assessment_view,
                     frm_assessment_admin_add,
                     frm_assessment_admin_update,
                     frm_instructor_studentsview,
                     frm_student_gradesview,
                     frm_advisers_studentsview,
                     frm_makeup_exam_list,                     
                     frm_quarter_maintenance, frm_parent_dashboard, frm_parent_change_password, frm_parent_edit_profile, frm_parent_select_child, frm_parent_view_grades,
                     frm_quarter_edit, frm_school_year_maintenance, frm_topic_maintenance_main, frm_usergroup_maintenance_main;
   }

    
}
