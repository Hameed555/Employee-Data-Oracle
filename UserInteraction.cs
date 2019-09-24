using System;


namespace EmployeeDetails
{
    class UserInteraction
    {
        private readonly OracleServer _oracleServer = new OracleServer();

        // Insertion Operation (Create)
        internal void CreateOperation()
        {
            try
            {
                Console.WriteLine("\nCreate Operation");
                Console.WriteLine("****** *********");
                bool loopContinueTitle = true;
                string jobtitle = "";
                while (loopContinueTitle)
                {
                    Console.Write("\n Enter the JobTitle : ");
                    jobtitle = Console.ReadLine();

                    if (jobtitle == string.Empty)
                        Console.WriteLine("\n Sorry!! Put Valid data...");
                    else
                        loopContinueTitle = false;
                }

                string employer = "";
                bool loopContinueEmployer = true;
                while (loopContinueEmployer)
                {
                    Console.Write("\n Enter the Employer : ");
                    employer = Console.ReadLine();

                    if (employer == string.Empty)
                        Console.WriteLine("\n Employer Cannot Be Null Or Empty");

                    else
                        loopContinueEmployer = false;
                }

                string desc = "";
                bool loopContinueDesc = true;
                while (loopContinueDesc)
                {
                    Console.Write("\n Enter the Description : ");
                    desc = Console.ReadLine();

                    if (desc == string.Empty)
                        Console.WriteLine("\n Description Cannot Be Null Or Empty");

                    else
                        loopContinueDesc = false;
                }

                string fromDate = "";
                bool loopContinueFrom = true;
                DateTime dateFrom;
                while (loopContinueFrom)
                {
                    Console.Write("\n Enter From Period : ");
                    fromDate = Console.ReadLine();

                    if (DateTime.TryParse(fromDate, out dateFrom))
                    {
                        fromDate = string.Format("{0:dd-MM-yyyy}", dateFrom);
                        loopContinueFrom = false;
                    }
                    else
                        Console.WriteLine("\n Sorry!! Please Give An Valid Date...");
                }

                string toDate = "";
                bool loopContinueTo = true;
                DateTime dateTo;
                while (loopContinueTo)
                {
                    Console.Write("\n Enter To Period : ");
                    toDate = Console.ReadLine();

                    if (DateTime.TryParse(toDate, out dateTo))
                    {
                        toDate = string.Format("{0:dd-MM-yyyy}", dateTo);

                        DateTime fromData = Convert.ToDateTime(fromDate);
                        DateTime toData = Convert.ToDateTime(toDate);

                        int dateResult = DateTime.Compare(fromData, toData);

                        if (dateResult == 0)
                            Console.WriteLine("Date Must Not Be Same");
                        else if (dateResult > 0)
                            Console.WriteLine("ToDate Must Be Later Than FromDate");
                        else
                        loopContinueTo = false;
                    }
                    else
                        Console.WriteLine("\n Sorry!! Please Give An Valid Date...");
                }
                int employeehistory_id = _oracleServer.ProcessEmployeeHistory(fromDate, toDate);
                _oracleServer.ProcessEmployeeData(jobtitle, employer, desc,employeehistory_id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in inserting data : {0}", ex.Message);
            }
        }

        //Update Operation
        internal void UpdateOperation()
        {
            Console.WriteLine("\nUpdate Operation");
            Console.WriteLine("****** *********");
            try
            {
                int employeehistoryID = 0;
                int jobID=0;
                bool loopContinueid = true;
                while (loopContinueid)
                {
                    Console.Write("\n Enter The JobID : ");
                    string jobidString = Console.ReadLine();
                    if (int.TryParse(jobidString, out jobID))
                    {
                        employeehistoryID = _oracleServer.JobIDExist(jobID);
                        loopContinueid = false;
                    }
                    else
                        Console.WriteLine("Please Put Valid Data");
                }
                //int empHistoryID = _SqlServer.JobIDExist(jobid);
                if (employeehistoryID != 0)
                {
                    bool loopContinue = true;
                    string jobTitle = "";
                    while (loopContinue)
                    {
                        Console.Write("\n Enter the JobTitle : ");
                        jobTitle = Console.ReadLine();

                        if (jobTitle == string.Empty)
                        {
                            Console.WriteLine("\n JobTitle Cannot Be Null Or Empty");
                            loopContinue = true;
                        }
                        else
                            loopContinue = false;
                    }

                    bool loopContinue1 = true;
                    string employerData = "";
                    while (loopContinue1)
                    {
                        Console.Write("\n Enter the Employer : ");
                        employerData = Console.ReadLine();

                        if (employerData == string.Empty)
                        {
                            Console.WriteLine("\n Employer Cannot Be Null Or Empty");
                            loopContinue1 = true;
                        }
                        else
                            loopContinue1 = false;
                    }

                    bool loopContinue2 = true;
                    string descData = "";
                    while (loopContinue2)
                    {
                        Console.Write("\n Enter the Description : ");
                        descData = Console.ReadLine();

                        if (descData == string.Empty)
                        {
                            Console.WriteLine("\n Description Cannot Be Null Or Empty");
                            loopContinue2 = true;
                        }
                        else
                            loopContinue2 = false;
                    }

                    string fromDate = "";
                    bool loopContinueFrom = true;
                    DateTime dateFrom;
                    while (loopContinueFrom)
                    {
                        Console.Write("\n Enter From Period : ");
                        fromDate = Console.ReadLine();

                        if (DateTime.TryParse(fromDate, out dateFrom))
                        {
                            fromDate = string.Format("{0:dd-MM-yyyy}", dateFrom);
                            loopContinueFrom = false;
                        }

                        else
                            Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                    }

                    string toDate = "";
                    bool loopContinueTo = true;
                    DateTime dateTo;
                    while (loopContinueTo)
                    {
                        Console.Write("\n Enter To Period : ");
                        toDate = Console.ReadLine();

                        if (DateTime.TryParse(toDate, out dateTo))
                        {
                            toDate = string.Format("{0:dd-MM-yyyy}", dateTo);

                            DateTime fromData = Convert.ToDateTime(fromDate);
                            DateTime toData = Convert.ToDateTime(toDate);

                            int dateResult = DateTime.Compare(fromData,toData);

                            if (dateResult == 0)
                                Console.WriteLine("\n Date Must Not Be Same...");
                            else if (dateResult > 0)
                                Console.WriteLine("\n From Date Is Always Earlier...");
                            else
                            loopContinueTo = false;
                        }
                        
                        else
                            Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                    }
                    _oracleServer.UpdateEmploymentHistory(fromDate, toDate, employeehistoryID);
                    _oracleServer.UpdateEmployee_Data(jobID, jobTitle, employerData, descData, employeehistoryID);
                }
                else
                    Console.WriteLine("Data Does Not Exist....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in updating data : {0}", ex.Message);
            }
        }
        // Delete Operation
        internal void DeleteOperation()
        {
            Console.WriteLine("\nDelete Operation");
            Console.WriteLine("****** *********");
            try
            {
                int employeehistoryID = 0;
                bool loopContinue = true;
                int jobID = 0;
                while (loopContinue)
                {
                    Console.Write("\n Enter The JobID :");
                    string jobidString = Console.ReadLine();
                    

                    if (int.TryParse(jobidString, out jobID))
                    {
                        employeehistoryID = _oracleServer.JobIDExist(jobID);
                        loopContinue = false;
                    }
                    else
                        Console.WriteLine("Please Put Valid Data");
                }

                if ((employeehistoryID != 0) && (jobID != 0))
                {
                    _oracleServer.DeleteData(jobID,employeehistoryID);
                }
                else
                    Console.WriteLine("Data Does Not Exist....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Delete Operation : {0}", ex.Message);
            }
        }
        //Display Operation
        internal void DisplayOperation()
        {
            Console.WriteLine("\nDisplay Operation");
            Console.WriteLine("******* *********\n");
            bool loopContinue = true;
            int data = 0;
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("\n 1. Single Record");
            Console.WriteLine("\n 2. Entire Records\n");
            Console.WriteLine("---------------------------------------------------------------");

            while (loopContinue)
            {
                Console.Write("\n Enter Your Choice : ");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out data))
                {
                    switch (data)
                    {
                        case 1:
                            SingleView();
                            loopContinue = false;
                            break;
                        case 2:
                            _oracleServer.MultiView();
                            loopContinue = false;
                            break;
                        default:
                            Console.WriteLine("Sorry!! Please Give An Valid Data");
                            loopContinue = true;
                            break;
                    }
                }
                else
                    Console.WriteLine("Sorry!! Please Give An Valid Data");
            }
        }

        internal void SingleView()
        {
            bool loopContinue = true;
            int value = 0;
            try
            {
                while (loopContinue)
                {
                    Console.Write("\n Enter The JobID : ");
                    string jobID = Console.ReadLine();

                    if (int.TryParse(jobID, out value))
                    {
                        _oracleServer.SingleData(value);
                        loopContinue = false;
                    }

                    else
                        Console.WriteLine("Please Give Valid Data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
