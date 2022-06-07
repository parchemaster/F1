using FormulaAPI;
using FormulaAPI.Entities;
using PV178_HW02.Export;
using PV178_HW02.Modelling;
using PV178_HW02.ui;

namespace PV178_HW02.data;

public class F1Factory
{
    private List<Result> _results;
    private List<Status> _statuses;
    private List<Driver> _drivers;
    public F1Factory()
    {
        _statuses = F1.GetStatuses(1000);
        _drivers = F1.GetDrivers(1000);
        CreateResults();
        StartProgram();
    }
    public void CreateResults()
    {
        _results = F1.GetResults(1000, 0);
        for (int i = 1; i < 30; i++)
        {
            _results.AddRange(F1.GetResults(1000, i*1000));
        }
    }
    
    public void StartProgram()
    {
        UI.PrintStart();
        int decision = UI.ReadInt("Your choice: ", 2);
        var listIssue = new List<String>();
        //user wants to find issue for specific driver
        if (decision == 1)
        {
            _drivers.ForEach(driver => Console.WriteLine(driver.Id + ": " + driver.Forename + " " + driver.Surname));
            int driverID = UI.ReadInt("Your choice: ", _drivers.Count);
            listIssue = CreateListOfIssueSpecificDriver(driverID);
        }
        //user wants to find issue for specific nationality
        else if (decision == 2)
        {
            listIssue = CreateListOfIssueSpecificNationality();

        }

        //checks if there are some experienced drivers
        if (listIssue.Any())
        {
            CsvExporter.Export("fif1_file", listIssue);
            ModelGenerator.run_cmd();
        }
        else
        {
            Console.Write("There are no experienced drivers");
        }
    }

    public List<String> CreateListOfIssueSpecificNationality()
    {
        //creating list of nationality 
        var nationalityList = new List<String>();
        foreach (var driver in _drivers)
        {
            if (!nationalityList.Contains(driver.Nationality))
            {
                nationalityList.Add(driver.Nationality);
            }
        }
        nationalityList.ForEach(nationality => Console.WriteLine((nationalityList.IndexOf(nationality) + 1) + ": " + nationality));

        int chosenNationalId = UI.ReadInt("Your choice: ", nationalityList.Count);
        
        //creating list of issues for chosen nationality
        List<String> issues = new List<string>();
        foreach (var driver in _drivers)
        {
            if (driver.Nationality.Equals(nationalityList[chosenNationalId-1]) && CheckExperience(driver))
            {
                issues.AddRange(CreateListOfIssueSpecificDriver(driver.Id));
            }
        }

        return issues;
    }


    public List<String> CreateListOfIssueSpecificDriver(int driverID)
    {

        var issueList = new List<String>();
        foreach (var result in _results) 
        {
            if (result.DriverId == driverID)
            {

                if (CheckValidation(result))
                {
                    foreach (var state in _statuses)
                    {
                        if (state.Id == result.StatusId)
                        {
                            var stateIndex = _statuses.IndexOf(state);
                            issueList.Add(result.DriverId + ";" + _statuses[stateIndex].Name);
                            Console.WriteLine(result.DriverId + ";" + _statuses[stateIndex].Name);
                            break;
                        }
                    }
                }
            }
        }

        return issueList;
    }

    //checks if state of issue is technical problem 
    private bool CheckValidation(Result result)
    {
        IList<bool> validConditions = new List<bool>
        {
            result.StatusId != 1 && result.StatusId != 2 && result.StatusId != 31 &&
            result.StatusId != 54 && result.StatusId != 62 && result.StatusId != 81 &&
            result.StatusId != 71 && result.StatusId != 89 && result.StatusId != 100 && 
            result.StatusId != 96 && result.StatusId != 107 && result.StatusId != 139 && 
            (result.StatusId < 11 || result.StatusId > 19) && result.StatusId != 45 && 
            result.StatusId != 50 && result.StatusId != 128 && result.StatusId != 53 &&
            result.StatusId != 55 && result.StatusId != 58 && result.StatusId != 88 &&
            (result.StatusId < 111 || result.StatusId > 120) && (result.StatusId < 122 || result.StatusId > 125) &&
            result.StatusId != 127 && result.StatusId != 133 && result.StatusId != 134
            
        };
        return validConditions[0];
    }

    private bool CheckExperience(Driver driver)
    {
        int experience = 0;
        foreach (var result in _results)
        {
            experience += result.DriverId == driver.Id ? 1 : 0;
        }
        return experience >= 10;
    }
}