using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该类可序列化显示（在unity面板上）
//这样TimeManager中的此类变量即使保密也可以在面板上查看
[System.Serializable]
public class GameTimeStamp
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public Season season;

    public enum DayOfTheWeek
    {
        Saturday,
        Sunday,
        Monday,
        TuesDay,
        Wednesday,
        Thursday,
        Friday,    
    }
    public int day;
    public int hour;
    public int minute;

    //构造函数
    public GameTimeStamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    //重载构造函数
    public GameTimeStamp(GameTimeStamp gameTimeStamp)
    {
        this.year = gameTimeStamp.year;
        this.season = gameTimeStamp.season;
        this.day = gameTimeStamp.day;
        this.hour = gameTimeStamp.hour;
        this.minute = gameTimeStamp.minute;
    }

    //更新时间
    public void UpdateClock()
    {
        minute++;

        //60分钟一小时
        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }
        //24小时一天
        if(hour >= 24)
        {
            hour = 0;
            day++;
        }
        //30天一个月
        if(day > 30)
        {
            day = 1;

            //一个月算一个季节，四季节一年（不和现实一样）
            if(season==Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
            
        }
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        //换算过去的时间
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;
        //算今天的天数
        int dayIndex = daysPassed % 7;
        //天数转换成日期
        return (DayOfTheWeek)dayIndex;
    }

    //小时转分钟
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }
    //天数转小时
    public static int DaysToHours(int day)
    { 
        return day * 24; 
    }
    //季转天数
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex=(int)season;
        return seasonIndex*30;
    }
    //年转天
    public static int YearsToDays(int year)
    {
        return year * 4 * 30; 
    }

    //计算两条时间戳的差值
    public static int CompareTimeStamps(GameTimeStamp timeStamp1,GameTimeStamp timeStamp2)
    {
        int timeStamp1Hours = DaysToHours(YearsToDays(timeStamp1.year)) + DaysToHours(SeasonsToDays(timeStamp1.season)) + DaysToHours(timeStamp1.day) + timeStamp1.hour;
        int timeStamp2Hours = DaysToHours(YearsToDays(timeStamp2.year)) + DaysToHours(SeasonsToDays(timeStamp2.season)) + DaysToHours(timeStamp2.day) + timeStamp2.hour;
        int difference = timeStamp2Hours - timeStamp1Hours;
        return Mathf.Abs(difference);
    }
}
