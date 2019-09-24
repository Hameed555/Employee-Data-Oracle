using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace EmployeeDetails
{
    class OracleServer
    {
        private static readonly OracleConnection _oracleConnection = new OracleConnection(Config.ConnectionString);
        public static OracleCommand oracleCommand;
        public OracleServer()
        {
            try
            {
                _oracleConnection.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
        }
        // Create (Insert) && Getting (Insert)
        internal int ProcessEmployeeHistory(string fromDate, string toDate)
        {
            int id = 0;
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    OracleParameter[] param = new OracleParameter[2];
                    param[0] = oracleCommand.Parameters.Add(":fromDate", OracleDbType.Varchar2, fromDate, ParameterDirection.Input);
                    param[1] = oracleCommand.Parameters.Add(":toDate", OracleDbType.Varchar2, toDate, ParameterDirection.Input);
                    oracleCommand.CommandText = "INSERT INTO employment_history(employee_from,employee_to) Values (:fromDate,:toDate)";
                    oracleCommand.ExecuteNonQuery();
                    Console.WriteLine("\n**********************************************************************************");
                    Console.WriteLine("\n Inserting Data into Employment_History ");
                    oracleCommand.CommandText = "SELECT MAX(employeehistory_id) FROM employment_history";
                    id = Convert.ToInt32(oracleCommand.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
            return id;
        }

        internal void ProcessEmployeeData(string jobTitle, string employerData, string descData, int employeehistoryID)
        {
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    OracleParameter[] param = new OracleParameter[4];
                    param[0] = oracleCommand.Parameters.Add(":jobTitle", OracleDbType.Varchar2, jobTitle, ParameterDirection.Input);
                    param[1] = oracleCommand.Parameters.Add(":employerData", OracleDbType.Varchar2, employerData, ParameterDirection.Input);
                    param[2] = oracleCommand.Parameters.Add(":descData", OracleDbType.Varchar2, descData, ParameterDirection.Input);
                    param[3] = oracleCommand.Parameters.Add(":employeehistoryID", OracleDbType.Int32, employeehistoryID, ParameterDirection.Input);
                    oracleCommand.CommandText = "INSERT INTO employee_data(job_title,employer,description,employeehistory_id) Values (:jobTitle,:employerData,:descData,:employeehistoryID)";
                    oracleCommand.ExecuteNonQuery();
                    Console.WriteLine("\n Inserting Data into Employee_Data Table ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
        }

        // Update Operation && Delete Operation
        internal int JobIDExist(int jobID)
        {
            int id = 0;
            try
            {
                oracleCommand = _oracleConnection.CreateCommand();
                OracleParameter[] param = new OracleParameter[1];
                param[0] = oracleCommand.Parameters.Add(":jobID", OracleDbType.Int32, jobID, ParameterDirection.Input);
                oracleCommand.CommandText = " SELECT employeehistory_id From employee_data WHERE job_id = :jobID ";
                using (OracleDataReader dataReader = oracleCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Console.WriteLine("\n Data Already Exist...");
                        id = Convert.ToInt32(dataReader["employeehistory_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
            return id;
        }

        //Update Get Data
        internal void UpdateEmploymentHistory(string fromDate, string toDate, int employeehistoryID)
        {
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    OracleParameter[] param = new OracleParameter[2];
                    //param[0] = oracleCommand.Parameters.Add(":employeehistoryID", OracleDbType.Int32, employeehistoryID,ParameterDirection.Input);
                    param[0] = oracleCommand.Parameters.Add("':fromDate'", OracleDbType.Varchar2, fromDate, ParameterDirection.Input);
                    param[1] = oracleCommand.Parameters.Add("':toDate'", OracleDbType.Varchar2, toDate, ParameterDirection.Input);
                    oracleCommand.CommandText = " UPDATE employment_history SET employee_from = :fromDate, employee_to = :toDate " + 
                                                " WHERE employeehistory_id = '" + employeehistoryID + "' ";
                   oracleCommand.ExecuteNonQuery();
                    Console.WriteLine("\n Data Updated Successfully in Employment_History");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
        }

        internal void UpdateEmployee_Data(int jobID, string jobTitle, string employerData, string descData, int employeehistoryID)
        {
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    OracleParameter[] param = new OracleParameter[3];
                    param[0] = oracleCommand.Parameters.Add("':jobTitle'", OracleDbType.Varchar2, jobTitle, ParameterDirection.Input);
                    param[1] = oracleCommand.Parameters.Add("':employerData'", OracleDbType.Varchar2, employerData, ParameterDirection.Input);
                    param[2] = oracleCommand.Parameters.Add("':descData'", OracleDbType.Varchar2, descData, ParameterDirection.Input);
                    oracleCommand.CommandText = " UPDATE employee_data SET job_title = :jobTitle, employer = :employerData, description = :descData, employeehistory_id = '" + employeehistoryID + "' " +
                                                " WHERE job_id = '" + jobID + "' ";
                    oracleCommand.ExecuteNonQuery();
                    Console.WriteLine("\n Data Updated Successfully in Employee_Data");
                }
            }
            catch(OracleException ex)
            {
                Console.WriteLine("Error : "+ex.Message);
            }
        }

        //Delete Data
        internal void DeleteData(int jobID, int employeehistoryID)
        {
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    oracleCommand.CommandText = " DELETE FROM employee_data WHERE job_id = '" + jobID + "' ";
                    oracleCommand.ExecuteNonQuery();
                    oracleCommand.CommandText = " DELETE FROM employment_history WHERE employeehistory_id = '" + employeehistoryID + "' ";
                    oracleCommand.ExecuteNonQuery();
                    Console.WriteLine("\n Data Deleted Successfully...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
            }
        }

        // Display Operation  - Single View
        internal void SingleData(int jobID)
        {
            string header = "{0}{1}{2}{3}{4}{5}";
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    OracleParameter[] param = new OracleParameter[1]; 
                    param[0] = oracleCommand.Parameters.Add(":jobID", OracleDbType.Int32, jobID, ParameterDirection.Input);
                    oracleCommand.CommandText = " SELECT employee_data.job_id, employee_data.job_title, employee_data.employer, employee_data.description, employment_history.employee_from, employment_history.employee_to " +
                                                " FROM employee_data INNER JOIN employment_history ON employee_data.employeehistory_id = employment_history.employeehistory_id " +
                                                " WHERE job_id = :jobID ";                    
                    using (OracleDataReader dataReader = oracleCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------");
                            string str = "";
                            Console.WriteLine(header, "JobID".PadRight(15), "Jobtitle".PadRight(22), "Employer".PadRight(22), "Description".PadRight(26), "EmpFrom".PadRight(27), "EmpTo".PadRight(15));
                            Console.WriteLine();
                            for (int i = jobID; i == jobID; i++)
                            {
                                for (int j = 0; j < dataReader.FieldCount; j++)
                                {
                                    Console.Write(dataReader.GetValue(j) + "{0}", str.PadRight(15));
                                }
                            }
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in getting single record : {0}", ex.Message);
            }
        }

        // Display Operation - Multiple View
        internal void MultiView()
        {
            string header = "{0}{1}{2}{3}{4}{5}";
            try
            {
                using (oracleCommand = _oracleConnection.CreateCommand())
                {
                    oracleCommand.CommandText = " SELECT employee_data.job_id, employee_data.job_title, employee_data.employer, employee_data.description," +
                                                " employment_history.employee_from, employment_history.employee_to FROM employee_data " +
                                                " INNER JOIN employment_history ON employee_data.employeehistory_id = employment_history.employeehistory_id ";

                    using (OracleDataReader dataReader = oracleCommand.ExecuteReader())
                    {
                        if(dataReader.HasRows)
                        {
                            while(dataReader.Read() && (dataReader.HasRows)) 
                            {
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------");
                                string str = "";
                                Console.WriteLine(header, "JobID".PadRight(15), "Jobtitle".PadRight(22), "Employer".PadRight(22), "Description".PadRight(26), "EmpFrom".PadRight(27), "EmpTo".PadRight(15));
                                Console.WriteLine();
                                for (int j = 0; j < dataReader.FieldCount; j++)
                                {
                                    Console.Write(dataReader.GetValue(j) + "{0}", str.PadRight(15));
                                }
                                Console.WriteLine();
                                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");
                            }
                        }
                    }
                }
            }
            catch(OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
