using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DatePicker : MonoBehaviour
{
    public TMP_Dropdown date;
    public TMP_Dropdown month;
    public TMP_Dropdown year;

    int currentDate;
    int currentMonth;
    int currentYear;

    void Start()
    {
        AddYears();
        SetCurrentMonthDate();
    }

    public void GetDates()
    {
        AddDates();
    }

    public void SetYear()
    {
        currentYear = System.Int32.Parse(year.options[year.value].text);
    }

    void AddDates()
    {
        if (currentMonth == 0)
            SetCurrentMonthDate();

        int currentDateValue = date.value;

        int days = System.DateTime.DaysInMonth(currentYear, currentMonth);
        date.ClearOptions();

        List<string> dates = new List<string>();

        for (int i = 1; i <= days; i++)
            dates.Add(i.ToString());

        date.AddOptions(dates);
        dates.Clear();
    }

    void SetCurrentMonthDate()
    {
        currentMonth = System.DateTime.Now.Month;
        month.value = currentMonth - 1;
        AddDates();
        currentDate = System.DateTime.Now.Day;
        date.value = currentDate - 1;
    }

    public void SetDate()
    {
        currentDate = System.Int32.Parse(date.options[date.value].text);
    }

    public void SetMonth()
    {
        currentMonth = month.value + 1;
    }

    void AddYears()
    {
        int currYear = System.DateTime.Now.Year;
        int minYear = Mathf.Clamp(currYear - 35, 0, currYear);

        List<string> years = new List<string>();
        for (int i = minYear; i <= currYear; i++)
            years.Add(i.ToString());

        year.AddOptions(years);
        year.value = 35;
        currentYear = currYear;
        years.Clear();
    }

    public string Stringify()
    {
        System.DateTime dateOfBirth = new System.DateTime(currentYear, currentMonth, currentDate);
        return dateOfBirth.ToString("dd/MM/yyyy");
    }

    public System.DateTime GetDate()
    {
        return new System.DateTime(currentYear, currentMonth, currentDate);
    }
}