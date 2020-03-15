using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainOfResponsibilityPattern : MonoBehaviour
{
//請假2天內 經理批准
//請假5天內 協理批准
//請假7天內 總經理批准
//7天以上   不批准
    void Start()
    {
        Manager a1 = new Manager("阿福"); // 經理
        Director a2 = new Director("技安"); // 協理
        GeneralManager a3 = new GeneralManager("宜靜"); // 總經理
        a1.SetUpManager(a2); // 設定經理的上級為協理
        a2.SetUpManager(a3); // 設定協理的上級為總經理

        // 假單初始化
        LeaveRequest leaveRequest = new LeaveRequest(); // 假單
        leaveRequest.Name = "大雄"; // 員工姓名

        leaveRequest.DayNum = 1; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出1天的假單

        leaveRequest.DayNum = 3; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出3天的假單

        leaveRequest.DayNum = 7; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出7天的假單

        leaveRequest.DayNum = 10; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出10天的假單


    }

    // 管理者處理事假申請的抽象類別
    abstract class ManagerHandler
    {
        protected string name;
        protected ManagerHandler upManager; // 上一級的管理者

        public ManagerHandler(string name)
        {
            this.name = name;
        }

        // 設定上一級的管理者
        public void SetUpManager(ManagerHandler upManager)
        {
            this.upManager = upManager;
        }

        // 事假處理
        abstract public void RequestPersonalLeave(LeaveRequest leaveRequest);
    }

    // 經理
    class Manager : ManagerHandler
    {
        public Manager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 2)
            {
                // 2天以內，經理可以批准
                Debug.LogFormat("經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);

            }
            else
            {
                // 超過2天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 協理
    class Director : ManagerHandler
    {
        public Director(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 5)
            {
                // 5天以內，經理可以批准
                Debug.LogFormat("協理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過5天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 總經理
    class GeneralManager : ManagerHandler
    {
        public GeneralManager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 7)
            {
                // 7天以內，總經理批准
                Debug.LogFormat("總經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過7天以上，再深入了解原因
                Debug.LogFormat("總經理 {0} 要再了解 {1}{2}天的事假原因", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
        }
    }


    // 假單
    class LeaveRequest
    {
        // 姓名
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // 天數
        private int dayNum;
        public int DayNum
        {
            get { return dayNum; }
            set { dayNum = value; }
        }
    }

}
