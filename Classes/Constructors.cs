using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PAOnlineAssessment.Classes
{
   public class Constructors : DataAccessObject
    {       

        public class User
        {
            public int UserID,UserGroupID;
            public string UserGroupDescription, Username, Password,
                   FirstName, LastName, Status, 
                   UserCreated, DateCreated, 
                   UpdateDate, UpdateUser;           
        }

        public class StudentAccount
        {
            public int StudentID, LevelID;
            public string StudentNumber, Firstname, Lastname,
                   Password, EmailAddress, EmailVerified,
                   AdminVerified, Status, UserCreated,
                   DateCreated, LastUpdateUser, LastUpdateDate;
        }
       
        public class RegistrationTerm
        {
            public int LevelID,SectionID,SubjectID;
            public string SchoolYear, LevelDescription, SectionDescription,SubjectDescription;
        }

        public class Subject
        {
            public int SubjectID, LevelID;
            public string Description,DateCreated,UserCreated,LastUpdateDate,LastUpdateUser;
            public float SubjectWeight;
        }

        public class QuestionPool
        {
            public int QuestionPoolID, SubjectID, LevelID, ImageID, TopicID;
            public string SubjectDescription, LevelDescription, Question,
                          CorrectAnswer, CorrectAnswerRemark, Choice1,
                          Choice1Remark, Choice2, Choice2Remark,
                          Choice3, Choice3Remark, Choice4,
                          Choice4Remark, Status, UserCreated,
                          DateCreated, LastUpdateUser, LastUpdateDate,Quarter,ImageFileName;
        }

        public class AssessmentType
        {
            public int AssessmentTypeID;
            public string Description, Status, UserCreated,
                          DateCreated, LastUpdateUser, LastUpdateDate;
        }

        public class Assessment
        {
            public int AssessmentID,AssessmentTypeID,SubjectID,LevelID,UserID;
            public string Title, Introduction, DateStart,DateEnd,TimeStart,TimeEnd,Status,UserCreated, SchoolYear,
                          DateCreated, LastUpdateUser, LastUpdateDate,Quarter,RandomQuestion,RandomAnswer,Schedule,ScheduleStatus,MakeUp,NoMakeUp;
        }

        public class AssessmentDetails
        {
            public int AssessmentID, QuestionPoolID, Points;
        }

        public class AssessmentFeedback
        {
            public int AssessmentID;
            public string GradeBoundary, Feedback;
        }

        public class AssessmentView
        {
            public int AssessmentID, AssessmentTypeID, UserID, LevelID, SubjectID;
            public string  AssessmentTypeDescription, TeacherFirstname, TeacherLastname,LevelDescription,
                           SubjectDescription,Title,Introduction,DateStart,DateEnd,
                           TimeStart,TimeEnd,Status,UserCreated,DateCreated, SchoolYear,
                           LastUpdateUser,LastUpdateDate,Quarter,RandomQuestions,RandomAnswer,Schedule,ScheduleStatus,MakeUp,NoMakeUp;
        }

        public class GradingView
        {
            public int GradingID, TeacherID, LevelID,
                        SectionID, SubjectID;
            public string AdvisoryTeacherID, AssignedName, LevelSection, Description,
                        SchoolYear, DateCreated, UserCreated, AdvisoryTeacher;
        }

        public class StudentAnswers
        {
            public int StudentAnswerID, StudentID, AssessmentID, QuestionPoolID;
            public string SchoolYear, SelectedAnswer, LastUpdateUser, LastUpdateDate;
        }

        public class StudentRegistrationView
        {
            public int StudentID, CurrentLevelID, RegistrationID, LevelID, SectionID, SummerWorkshopID, PaymentTermsID;

            public string StudentNumber, FirstName, MiddleName, LastName, Discount, StudentStatus, Gender, SchoolYear, RegistrationStatus
      , ReservationDate, RegistrationDate, SummerRegistrationDate, UserReserved, UserRegistered, UserSummerRegistered, ReservationPayment
      , RegistrationPayment, SummerRegistrationPayment, LevelDescription, SectionDescription, isOldStudent, Address, Birthday
      , AdvisoryTeacher;
        }
        public class Quarter
        {
            public string Quarters, DateFrom, DateTo, SchoolYear, LastUpdateDate, LastUpdateUser, isCurrentSY;
        }

        public class Levels
        {
            public int LevelID;
            public string LevelDescription, Status;
        }

        public class Sections
        {
            public int SectionID, LevelID, UserID;
            public string SectionDescription, Status;

        }

        public class ParentView
        {
            public int ParentID, UserID, UserGroupID, YearLevel, Section;
            public string Username, Password, Firstname, Lastname, Status;
            public string CFirstname, CLastname;
        }

        public class ParentChildGrades
        {
            public int ChildID, StudentID, ParentUserID, LevelID, SectionID;
            public string Status;
        }

        public class DisplaySettings
        {
            public int Days;
            public string Registration;
        }

        public class Topics
        {
            public int TopicID, LevelID, SubjectID;

            public string Description, Status;
        }

        public class Usergroup
        {
            public int UserGroupID;
            public string Description, AccessRights, Status;
        }


       // Constructors for Requirements
        public class RequirementHeader
        {
            public int RequirementHeaderID;
            public string RequirementDescription;
        }

        public class RequirementView
        {
            public int RequirementID,
                       RequirementHeaderID,
                       GradingID,
                       Quarter;

            public string DropLowest,
                          Percent,
                          Status,
                          Description,
                          SchoolYear;
        }


        public class Requirement
        {
            public int RequirementID, RequirementHeaderID, Quarter, GradingID;
            public string DropLowest;
            public decimal Percent;
        }


        public class RequirementSubitem
        {
            public int RequirementSubitemID, RequirementID, TotalPoints;
        }

        public class StudentGrades
        {
            public int RequirementSubitemID, RegistrationID, RequirementID, TotalPoints;
            public string Score;
        }

        public class ParentAccounts
        {
            public int ParentID, UserGroupID;
            public string Username, Password, EmailAddress, Firstname, Lastname, Status;
        }

        public class ParentChilds
        {
            public int ParentID, ParentUserID;
            public string Firstname, Lastname, YearLevel, Section;
        }

    }
}
