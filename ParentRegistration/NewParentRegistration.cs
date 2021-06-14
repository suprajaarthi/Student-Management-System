

using Hubstream.Development.Platform;
using Hubstream.Development.Platform.Database;
using StudentsProgressmanagement.AllDetails;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudentsProgressmanagement.UserRegistration.ParentRegistration
{
    [ModuleServiceProvider(typeof(INewStudentRegistration))]
    public sealed class NewStudentRegistration : Module, INewStudentRegistration
    {
        public sealed class NewStudentRegistration : Module, INewStudentRegistration
        {
            #region Module Dependencies
            /// <summary>
            /// Database Connection Factory
            /// </summary>
            [ModuleDependency]
            private readonly IDatabaseConnectionFactory databaseConnectionFactory = null;
            #endregion

            #region Configuration Dependencies
            /// <summary>
            /// Database Configuration
            /// </summary>
            [ConfigurationDependency]
            private readonly ConnectionStringConfiguration connectionString = null;
            #endregion


            #region constructor
            ///<summary>
            ///Constructor
            /// </summary>
            public NewStudentRegistration() : base(typeof(NewStudentRegistration).Name) { }
            #endregion

            ///<summary>
            ///<see cref="INewStudentRegistration.AddNewStudentAsync(StudentDetails studentDetails)"/>
            /// </summary>


            #region Public methods
            public async Task AddNewStudentAsync(StudentDetails studentDetails)
            {
                using (IDatabaseConnection databaseConnection = this.databaseConnectionFactory.
                CreateDatabaseConnection(connectionString.ConnectionString))
                {
                    DepartmentDetails departmentDetails = new DepartmentDetails();
                    await databaseConnection.ConnectAsync();
                    IDatabaseCommand databaseCommand = databaseConnection.CreateCommand();
                    List<SqlParameter> parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("StudentNameParam", studentDetails.StudentName));
                    parameter.Add(new SqlParameter("StudentRollIDParam", studentDetails.StudentID));
                    parameter.Add(new SqlParameter("SemesterParam", studentDetails.Semester));
                    parameter.Add(new SqlParameter("StudentContactNumberParam", studentDetails.StudentContactNumber));
                    parameter.Add(new SqlParameter("StudentMailIDParam", studentDetails.StudentMailID));
                    parameter.Add(new SqlParameter("StudentPasswordParam", studentDetails.StudentPassword));
                    parameter.Add(new SqlParameter("DepartmentIDParam", departmentDetails.DepartmentID));

                    String InsertCommand = "INSERT INTO Teacher" +
                        "(StudentID,StudentName, DepartmentID,StudentContactNumber, StudentMailID,StudentPassword) " +
                        "VALUES (@StudentRollIDParam,@StudentNameParam,@DepartmentIDParam,@StudentContactNumberParam,StudentMailIDParam,HASHBYTES" +
                        "('SHA1', @StudentPasswordParam))";
                    await databaseCommand.ExecuteNonQueryAsync(InsertCommand, parameter.ToArray());

                }
            }



            #endregion

        }
    }
}
