using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiliderPattern : MonoBehaviour
{

    private void Start()
    {
        Builider builider = new VactationBuilider();

        builider.BuilidDay("3/1");
        builider.AddHotel("Hotel");
        builider.AddReservation("NA");
        builider.AddSpecialEvent("NA");
        builider.AddTickets("No Game No Life");
        print(builider.GetVactationPlanner());
    }


    public abstract class Builider
    {

        public abstract void BuilidDay(string s);
        public abstract void AddHotel(string name);
        public abstract void AddReservation(string s);
        public abstract void AddSpecialEvent(string s);
        public abstract void AddTickets(string s);
        public abstract string GetVactationPlanner();
    }

    public class VactationBuilider : Builider
    {
        string vactation;
        public override void AddHotel(string name)
        {
            vactation += "飯店:" + name + "\r\n";
        }

        public override void AddReservation(string s)
        {
            vactation += "預約號碼:" + s + "\r\n";
        }

        public override void AddSpecialEvent(string s)
        {
            vactation += "特殊行程:" + s + "\r\n";
        }

        public override void BuilidDay(string s)
        {
            vactation += "日期:" + s + "\r\n";
        }

        public override string GetVactationPlanner()
        {
            return vactation;
        }

        public override void AddTickets(string s)
        {
            vactation += "電影票:" + s + "\r\n";
        }
    }

}

